using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Models {
    public class UploadedFileItem {
        public long Id { get; set; }
        public required string FileName { get; set; }
        public required string Extension { get; set; }
        public String Size { get; set; }
    }
}
