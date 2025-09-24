using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Models {
    public static class FileSizeConverter {
        public static string Convert( long kilobytes ) {
            const double MB = 1024.0;
            const double GB = MB * 1024.0;

            if ( kilobytes < MB )
                return $"{kilobytes} KB";
            else if ( kilobytes >= GB )
                return $"{kilobytes / GB:F2} GB";
            else
                return $"{kilobytes / MB:F2} MB";
        }
    }
}
