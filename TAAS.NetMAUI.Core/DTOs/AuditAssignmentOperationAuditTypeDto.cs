using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.DTOs {
    public class AuditAssignmentOperationAuditTypeDto : BaseDto {
        public long AuditAssignmentId { get; set; }
        public AuditAssignmentDto AuditAssignment { get; set; }
        public long AuditTypeId { get; set; }
        public AuditTypeDto AuditType { get; set; }
    }
}
