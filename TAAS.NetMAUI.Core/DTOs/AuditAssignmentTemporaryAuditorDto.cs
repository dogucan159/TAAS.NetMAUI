using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.DTOs {
    public class AuditAssignmentTemporaryAuditorDto : BaseDto {
        public long AuditAssignmentId { get; set; }
        public AuditAssignmentDto AuditAssignment { get; set; }
        public long AuditorId { get; set; }
        public AuditorDto Auditor { get; set; }
    }
}
