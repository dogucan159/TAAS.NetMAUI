using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Utilities.ExcelUpload {

    public static class ExcelUploadValidator {
        private static readonly char[] FormulaTriggers = new[] { '=', '+', '-', '@' };

        // DDE indicators like =cmd|..., =microsoft|..., =excel|...
        private static readonly Regex DdeRegex =
            new Regex( @"^=\s*(?:cmd|microsoft|excel)\s*\|", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        // Conservative HTML/script indicators
        private static readonly Regex ScriptTagRegex =
            new Regex( @"<\s*/?\s*script\b", RegexOptions.IgnoreCase | RegexOptions.Compiled );
        private static readonly Regex JsUrlRegex =
            new Regex( @"javascript\s*:", RegexOptions.IgnoreCase | RegexOptions.Compiled );
        private static readonly Regex HtmlEventHandlerRegex =
            new Regex( @"\bon\w+\s*=", RegexOptions.IgnoreCase | RegexOptions.Compiled );
        private static readonly Regex HtmlTagRegex =
            new Regex( @"<[^>]+>", RegexOptions.IgnoreCase | RegexOptions.Compiled );
        private static readonly Regex UrlRegex =
            new Regex( @"https?://", RegexOptions.IgnoreCase | RegexOptions.Compiled );


        public static async Task<ExcelValidationResult> ValidateAsync( Stream excelStream, ExcelValidationOptions? options = null ) {
            options ??= new ExcelValidationOptions();

            // Copy to a seekable buffer
            using var mem = new MemoryStream();
            await excelStream.CopyToAsync( mem ).ConfigureAwait( false );
            mem.Position = 0;

            var result = new ExcelValidationResult();

            using var workbook = new XLWorkbook( mem );

            foreach ( var ws in workbook.Worksheets ) {
                // Sheet whitelist
                if ( options.AllowedSheets != null && !options.AllowedSheets.Contains( ws.Name ) ) {
                    result.Violations.Add( new ExcelViolation {
                        Sheet = ws.Name,
                        Address = "(sheet)",
                        Reason = "Sheet not allowed by whitelist."
                    } );
                    // Keep scanning so user gets full report
                }

                // Hidden sheet
                if ( options.ForbidHiddenSheets && ws.Visibility != XLWorksheetVisibility.Visible ) {
                    result.Violations.Add( new ExcelViolation {
                        Sheet = ws.Name,
                        Address = "(sheet)",
                        Reason = "Hidden sheet detected."
                    } );
                    // Keep scanning the hidden sheet as well to surface all issues
                }

                // Use exact bounds of used area
                var firstUsed = ws.FirstCellUsed();
                var lastUsed = ws.LastCellUsed();
                if ( firstUsed == null || lastUsed == null )
                    continue; // empty sheet

                int startRow = firstUsed.Address.RowNumber;
                int startCol = firstUsed.Address.ColumnNumber;
                int endRow = lastUsed.Address.RowNumber;
                int endCol = lastUsed.Address.ColumnNumber;

                // Column whitelist: report once per disallowed column in bounds
                if ( options.AllowedColumns != null && options.AllowedColumns.Count > 0 ) {
                    for ( int c = startCol; c <= endCol; c++ ) {
                        if ( !options.AllowedColumns.Contains( c ) ) {
                            result.Violations.Add( new ExcelViolation {
                                Sheet = ws.Name,
                                Address = $"Column {c}",
                                Reason = "Column not allowed by whitelist."
                            } );
                        }
                    }
                }

                // Comments (sheet-wide)
                if ( options.ForbidComments ) {
                    foreach ( var c in ws.CellsUsed().Where( c => c.HasComment ) ) {
                        result.Violations.Add( new ExcelViolation {
                            Sheet = ws.Name,
                            Address = c.Address.ToStringFixed(),
                            Reason = "Cell comment detected."
                        } );
                        if ( options.StopOnFirstCritical ) return result;
                    }
                }

                // Cell scanning
                for ( int r = startRow; r <= endRow; r++ ) {
                    for ( int c = startCol; c <= endCol; c++ ) {
                        if ( options.AllowedColumns != null &&
                            options.AllowedColumns.Count > 0 &&
                            !options.AllowedColumns.Contains( c ) ) {
                            continue; // already reported column-level violation
                        }

                        var cell = ws.Cell( r, c );
                        var addr = cell.Address.ToStringFixed();

                        // (1) Real formulas
                        var hasFormula = options.FlagFormula && ( cell.HasFormula || !string.IsNullOrEmpty( cell.FormulaA1 ) );
                        if ( hasFormula ) {
                            var f = cell.FormulaA1 ?? string.Empty;
                            if ( options.AddFormulaToViolation ) {
                                result.Violations.Add( new ExcelViolation {
                                    Sheet = ws.Name,
                                    Address = addr,
                                    Reason = "Cell contains a formula.",
                                    Sample = f.Length > 128 ? f[..128] + "..." : f
                                } );
                                if ( options.StopOnFirstCritical ) return result;
                            }
                            else {

                                // Make the formula inert & visible to user
                                cell.Clear( XLClearOptions.Contents );
                                cell.Value = XLCellValue.FromObject( "'" + f );

                                result.Changes.Add( new NeutralizedCell( ws.Name, addr, "Formula escaped with leading apostrophe", GetSample( f ) ) );
                                continue;
                            }

                            // DDE pattern inside formula
                            if ( DdeRegex.IsMatch( f ) ) {
                                result.Violations.Add( new ExcelViolation {
                                    Sheet = ws.Name,
                                    Address = addr,
                                    Reason = "DDE formula pattern detected.",
                                    Sample = f.Length > 128 ? f[..128] + "..." : f
                                } );
                                if ( options.StopOnFirstCritical ) return result;
                            }

                            continue; // no need to inspect text for this cell
                        }

                        // (2) Raw text for non-formula cells (not formatted display)
                        string raw;
                        if ( cell.DataType == XLDataType.Text )
                            raw = cell.GetString();
                        else
                            raw = cell.Value.ToString( CultureInfo.InvariantCulture );

                        if ( cell.Value.IsBlank || string.IsNullOrWhiteSpace( raw ) )
                            continue;

                        var trimmed = raw.TrimStart();

                        // Formula-like text (including DDE typed as plain text)
                        if ( options.FlagFormulaLikeText &&
                            trimmed.Length > 0 &&
                            ( trimmed[0] == '=' || trimmed[0] == '+' || trimmed[0] == '-' || trimmed[0] == '@' ) ) {
                            if ( options.AddFormulaLikeTextToViolation ) {
                                result.Violations.Add( new ExcelViolation {
                                    Sheet = ws.Name,
                                    Address = addr,
                                    Reason = "Text value looks like a formula (starts with =, +, -, @).",
                                    Sample = raw.Length > 128 ? raw[..128] + "..." : raw
                                } );
                                if ( options.StopOnFirstCritical ) return result;
                            }
                            else {
                                bool startsWithTrigger = trimmed.Length > 0 && FormulaTriggers.Contains( trimmed[0] );
                                bool looksLikeDde = DdeRegex.IsMatch( trimmed );

                                if ( startsWithTrigger || looksLikeDde ) {
                                    // Avoid double-escaping: if it already *visibly* starts with an apostrophe in the string we got,
                                    // ClosedXML typically returns the value without the visual prefix. We still enforce text and prefix.
                                    cell.Value = XLCellValue.FromObject( "'" + raw );
                                    cell.Value = "'" + raw;

                                    var reason = looksLikeDde
                                        ? "DDE-like text escaped with leading apostrophe"
                                        : "Formula-like text escaped with leading apostrophe";

                                    result.Changes.Add( new NeutralizedCell( ws.Name, addr, reason, GetSample( raw ) ) );
                                }
                            }

                            /*if ( DdeRegex.IsMatch( trimmed ) ) {
                                result.Violations.Add( new ExcelViolation {
                                    Sheet = ws.Name,
                                    Address = addr,
                                    Reason = "DDE formula-like text detected.",
                                    Sample = raw.Length > 128 ? raw[..128] + "..." : raw
                                } );
                                if ( options.StopOnFirstCritical ) return result;
                            }*/
                        }

                        // Optional XSS/HTML checks on raw text
                        if ( options.FlagHtmlOrScript &&
                            ( ScriptTagRegex.IsMatch( raw ) ||
                             JsUrlRegex.IsMatch( raw ) ||
                             HtmlEventHandlerRegex.IsMatch( raw ) ||
                             HtmlTagRegex.IsMatch( raw ) ) ) {
                            result.Violations.Add( new ExcelViolation {
                                Sheet = ws.Name,
                                Address = addr,
                                Reason = "Potential HTML/script content detected.",
                                Sample = raw.Length > 128 ? raw[..128] + "..." : raw
                            } );
                            if ( options.StopOnFirstCritical ) return result;
                        }

                        if ( options.ForbidUrl && UrlRegex.IsMatch( raw ) ) {
                            result.Violations.Add( new ExcelViolation {
                                Sheet = ws.Name,
                                Address = addr,
                                Reason = "URL detected (http/https URLs are not allowed).",
                                Sample = raw.Length > 128 ? raw[..128] + "..." : raw
                            } );

                            if ( options.StopOnFirstCritical )
                                return result;
                        }

                    } // end for
                }
            }
            using var outMs = new MemoryStream();
            workbook.SaveAs( outMs );
            result.Bytes = outMs.ToArray();

            return result;
        }

        private static string GetSample( string s )
            => s.Length > 128 ? s[..128] + "..." : s;

    }
}