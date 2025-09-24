using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IAuditAssignmentFinancialAuditTypeRepository {
        Task<AuditAssignmentFinancialAuditType?> GetOneAuditAssignmentFinancialAuditTypeByAuditAssignmentIdAndAuditTypeId( long auditAssignmentId, long auditTypeId, bool trackChanges );

    }
}
