using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.DTOs {
    public class ChecklistDetailTaasFileCreateDto : BaseDto {
        public long ChecklistDetailId { get; set; }
        public long TaasFileId { get; set; }
    }
}
