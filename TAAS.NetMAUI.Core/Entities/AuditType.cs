using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class AuditType : BaseEntity {
        public String Code { get; set; }
        public String Description { get; set; }
        public ICollection<AuditAssignmentOperationAuditType> AuditAssignmentOperationAuditTypes { get; set; }
        public ICollection<AuditAssignmentFinancialAuditType> AuditAssignmentFinancialAuditTypes { get; set; }
    }
}
