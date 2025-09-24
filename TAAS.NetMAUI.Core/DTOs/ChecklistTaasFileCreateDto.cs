using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.DTOs {
    public class ChecklistTaasFileCreateDto : BaseDto {
        public long ChecklistId { get; set; }
        public long TaasFileId { get; set; }
        public TaasFileCreateDto TaasFile { get; set; }
    }
}
