using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Utilities.ExcelUpload {
    public class ExcelValidationOptions {
        public HashSet<string>? AllowedSheets { get; set; }
        public HashSet<int>? AllowedColumns { get; set; }
        //public int MaxRowsPerSheet { get; set; } = 50000;
        //public int MaxColumnsPerSheet { get; set; } = 100;
        public bool ForbidHiddenSheets { get; set; } = true;
        public bool ForbidComments { get; set; } = false;
        public bool FlagFormula { get; set; } = true;
        public bool AddFormulaToViolation { get; set; } = false;
        public bool FlagFormulaLikeText { get; set; } = true;
        public bool AddFormulaLikeTextToViolation { get; set; } = false;
        public bool FlagHtmlOrScript { get; set; } = true;
        public bool ForbidUrl { get; set; } = true;
        // Optional: stop early on critical findings (formulas/DDE)
        public bool StopOnFirstCritical { get; set; } = false;


    }
}
