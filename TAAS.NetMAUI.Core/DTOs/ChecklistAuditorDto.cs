using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Core.DTOs {
    public class ChecklistAuditorDto : BaseDto {
        public long ChecklistId { get; set; }
        public ChecklistDto Checklist { get; set; }

        public long AuditorId { get; set; }
        public AuditorDto Auditor { get; set; }
    }
}
