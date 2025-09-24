using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Core.DTOs {
    public class ChecklistTaasFileDto : BaseDto {
        public long ChecklistId { get; set; }
        public ChecklistDto Checklist { get; set; }
        public long TaasFileId { get; set; }
        public TaasFileDto TaasFile { get; set; }
        public bool? Synched { get; set; }
        public bool? Deleted { get; set; }
    }
}
