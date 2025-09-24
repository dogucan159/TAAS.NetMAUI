using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class AuditAssignmentOperationAuditType : BaseEntity {
        public long AuditAssignmentId { get; set; }
        public AuditAssignment AuditAssignment { get; set; }
        public long AuditTypeId { get; set; }
        public AuditType AuditType { get; set; }
    }
}
