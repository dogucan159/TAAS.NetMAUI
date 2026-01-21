using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Utilities.ExcelUpload {


    public record NeutralizedCell( string Sheet, string Address, string Reason, string OriginalSample );

    public class ExcelNeutralizeResult {

        public byte[] Bytes { get; set; } = Array.Empty<byte>();
        public List<NeutralizedCell> Changes { get; } = new();

    }
}
