using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Utilities.ExcelUpload {

    public static class ExcelFormulaEscaper {
        private static readonly char[] FormulaTriggers = new[] { '=', '+', '-', '@' };

        // DDE pattern examples: =cmd|..., =microsoft|..., =excel|...
        private static readonly Regex DdeRegex =
            new Regex( @"^=\s*(?:cmd|microsoft|excel)\s*\|", RegexOptions.IgnoreCase | RegexOptions.Compiled );

        /// <summary>
        /// Loads an Excel workbook, escapes formulas and formula-like text by prefixing a leading apostrophe ('),
        /// and returns a sanitized workbook as bytes plus a list of cells that were changed.
        /// </summary>
        public static async Task<ExcelNeutralizeResult> EscapeFormulasInPlaceAsync( Stream excelStream, ExcelNeutralizeOptions? options = null ) {
            options ??= new ExcelNeutralizeOptions();

            using var mem = new MemoryStream();
            await excelStream.CopyToAsync( mem ).ConfigureAwait( false );
            mem.Position = 0;

            var result = new ExcelNeutralizeResult();

            using var wb = new XLWorkbook( mem );

            foreach ( var ws in wb.Worksheets.ToList() ) {
                if ( options.SkipHiddenSheets && ws.Visibility != XLWorksheetVisibility.Visible )
                    continue;

                if ( options.AllowedSheets != null && !options.AllowedSheets.Contains( ws.Name ) )
                    continue;

                var first = ws.FirstCellUsed();
                var last = ws.LastCellUsed();
                if ( first == null || last == null )
                    continue;

                int startRow = first.Address.RowNumber;
                int startCol = first.Address.ColumnNumber;
                int endRow = Math.Min( last.Address.RowNumber, options.MaxRowsPerSheet );
                int endCol = Math.Min( last.Address.ColumnNumber, options.MaxColumnsPerSheet );

                for ( int r = startRow; r <= endRow; r++ ) {
                    for ( int c = startCol; c <= endCol; c++ ) {
                        if ( options.AllowedColumns != null &&
                            options.AllowedColumns.Count > 0 &&
                            !options.AllowedColumns.Contains( c ) ) {
                            continue;
                        }

                        var cell = ws.Cell( r, c );
                        var addr = cell.Address.ToStringFixed();

                        // (1) Real formulas -> escape by storing the *literal* formula string with a leading apostrophe.
                        if ( cell.HasFormula || !string.IsNullOrEmpty( cell.FormulaA1 ) ) {
                            var formula = cell.FormulaA1 ?? string.Empty;

                            // Make the formula inert & visible to user
                            cell.Clear( XLClearOptions.Contents );
                            cell.Value = XLCellValue.FromObject( "'" + formula );

                            result.Changes.Add( new NeutralizedCell( ws.Name, addr, "Formula escaped with leading apostrophe", GetSample( formula ) ) );
                            continue;
                        }

                        // (2) Non-formula cells: check raw textual value for formula-like patterns or DDE-like strings
                        string raw;
                        if ( cell.DataType == XLDataType.Text )
                            raw = cell.GetString();
                        else
                            raw = cell.Value.ToString( CultureInfo.InvariantCulture );

                        if ( string.IsNullOrEmpty( raw ) )
                            continue;

                        var trimmed = raw.TrimStart();
                        bool startsWithTrigger = trimmed.Length > 0 && FormulaTriggers.Contains( trimmed[0] );
                        bool looksLikeDde = options.NeutralizeDdeText && DdeRegex.IsMatch( trimmed );

                        if ( options.NeutralizeFormulaLikeText && ( startsWithTrigger || looksLikeDde ) ) {
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
                }
            }

            using var outMs = new MemoryStream();
            wb.SaveAs( outMs );
            result.Bytes = outMs.ToArray();
            return result;
        }

        private static string GetSample( string s )
            => s.Length > 128 ? s[..128] + "..." : s;

    }
}
