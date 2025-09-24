using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class AuditAssignmentTemporaryAuditor : BaseEntity {
        public long AuditAssignmentId { get; set; }
        public AuditAssignment AuditAssignment { get; set; }
        public long AuditorId { get; set; }
        public Auditor Auditor { get; set; }
    }
}
