using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Utilities.ExcelUpload {
    public class ExcelValidationResult {

        public bool IsValid => Violations.Count == 0;
        public List<ExcelViolation> Violations { get; } = new();
        public byte[] Bytes { get; set; } = Array.Empty<byte>();
        public List<NeutralizedCell> Changes { get; } = new();

    }
}
