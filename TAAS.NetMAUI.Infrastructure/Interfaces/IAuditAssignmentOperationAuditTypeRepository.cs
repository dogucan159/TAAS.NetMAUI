using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;

using TAAS.NetMAUI.Core.Entities;
namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IAuditAssignmentOperationAuditTypeRepository {
        Task<AuditAssignmentOperationAuditType?> GetOneAuditAssignmentOperationAuditTypeByAuditAssignmentIdAndAuditTypeId( long auditAssignmentId, long auditTypeId, bool trackChanges );

    }
}
