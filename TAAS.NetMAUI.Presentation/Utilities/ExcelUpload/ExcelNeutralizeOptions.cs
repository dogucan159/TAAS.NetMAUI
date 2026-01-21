using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Utilities.ExcelUpload {
    public class ExcelNeutralizeOptions {

        public HashSet<string>? AllowedSheets { get; set; }
        public HashSet<int>? AllowedColumns { get; set; }
        public int MaxRowsPerSheet { get; set; } = 50000;
        public int MaxColumnsPerSheet { get; set; } = 200;
        public bool SkipHiddenSheets { get; set; } = false; // Scan hidden by default
        public bool NeutralizeDdeText { get; set; } = true;  // DDE when stored as plain text
        public bool NeutralizeFormulaLikeText { get; set; } = true; // = + - @

    }
}
