using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.DTOs {
    public class TaasFileCreateDto : BaseDto {
        public String Name { get; set; }
        public int Size { get; set; }
        public byte[] FileData { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
