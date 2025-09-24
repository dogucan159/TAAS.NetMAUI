using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.DTOs {
    public class TaskTypeDto : BaseDto {
        public String Code { get; set; }
        public String Description { get; set; }
        public long SystemAuditTypeId { get; set; }
        public AuditTypeDto SystemAuditType { get; set; }
    }
}
