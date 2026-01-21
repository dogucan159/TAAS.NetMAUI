using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Utilities.ExcelUpload {
    public class ExcelViolation {
        public string Sheet { get; set; } = "";
        public string Address { get; set; } = "";
        public string Reason { get; set; } = "";
        public string? Sample { get; set; }

    }
}
