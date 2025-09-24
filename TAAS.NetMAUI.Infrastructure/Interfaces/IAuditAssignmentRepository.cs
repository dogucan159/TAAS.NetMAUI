using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IAuditAssignmentRepository {
        Task<AuditAssignment?> GetOneAuditAssignmentById( long id, bool trackChanges );
        Task<List<AuditAssignment>> GetAllAuditAssignments( bool trackChanges );
        void CreateOneAuditAssignment( AuditAssignment auditAssignment );
    }
}
