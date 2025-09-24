using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Core.DTOs {
    public class ChecklistHeaderDto : BaseDto {

        public long ChecklistId { get; set; }
        public ChecklistDto Checklist { get; set; }

        public long ChecklistTemplateHeaderId { get; set; }
        public ChecklistTemplateHeaderDto ChecklistTemplateHeader { get; set; }

        public String? Value { get; set; }
        public int Version { get; set; }
    }
}
