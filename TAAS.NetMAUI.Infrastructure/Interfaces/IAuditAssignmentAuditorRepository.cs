using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IAuditAssignmentAuditorRepository {
        Task<AuditAssignmentAuditor?> GetOneAuditAssignmentAuditorByAuditAssignmentIdAndAuditorId( long auditAssignmentId, long auditorId, bool trackChanges );
    }
}
