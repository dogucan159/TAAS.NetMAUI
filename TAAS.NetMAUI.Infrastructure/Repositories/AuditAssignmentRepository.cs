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
    public class AuditAssignmentRepository : BaseRepository<AuditAssignment>, IAuditAssignmentRepository {

        public AuditAssignmentRepository( TaasDbContext context ) : base( context ) {
        }

        public void CreateOneAuditAssignment( AuditAssignment auditAssignment ) => Create( auditAssignment );

        public async Task<List<AuditAssignment>> GetAllAuditAssignments( bool trackChanges ) =>
            await FindAll( trackChanges )
                .Include( b => b.CoordinatorAuditor )
                .Include( b => b.MainTask )
                .Include( b => b.TaskType )
                .ThenInclude( b => b.SystemAuditType )
                .Include( b => b.Task )
                .Include( b => b.StrategyPlanPeriod )
                .Include( b => b.AuditPeriod )
                .Include( x => x.AuditAssignmentAuditors )
                .ThenInclude( aa => aa.Auditor )
                .Include( x => x.AuditAssignmentTemporaryAuditors )
                .ThenInclude( aa => aa.Auditor )
                .Include( x => x.AuditAssignmentOperationAuditTypes )
                .ThenInclude( x => x.AuditType )
                .Include( x => x.AuditAssignmentFinancialAuditTypes )
                .ThenInclude( x => x.AuditType )
                .ToListAsync();

        public async Task<AuditAssignment?> GetOneAuditAssignmentById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
            .SingleOrDefaultAsync();
    }
}
