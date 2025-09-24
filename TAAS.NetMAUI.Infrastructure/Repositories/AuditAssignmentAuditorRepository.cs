using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Infrastructure.Data;
using TAAS.NetMAUI.Infrastructure.Interfaces;

namespace TAAS.NetMAUI.Infrastructure.Repositories {
    public class AuditAssignmentAuditorRepository : BaseRepository<AuditAssignmentAuditor>, IAuditAssignmentAuditorRepository {

        public AuditAssignmentAuditorRepository( TaasDbContext context ) : base( context ) {

        }
        public async Task<AuditAssignmentAuditor?> GetOneAuditAssignmentAuditorByAuditAssignmentIdAndAuditorId( long auditAssignmentId, long auditorId, bool trackChanges ) =>
            await FindByCondition( b => b.AuditAssignmentId == auditAssignmentId && b.AuditorId == auditorId, trackChanges )
            .SingleOrDefaultAsync();
    }
}
