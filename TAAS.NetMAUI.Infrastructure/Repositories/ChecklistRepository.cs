using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Infrastructure.Data;
using TAAS.NetMAUI.Infrastructure.Interfaces;

namespace TAAS.NetMAUI.Infrastructure.Repositories {
    public class ChecklistRepository : BaseRepository<Checklist>, IChecklistRepository {

        public ChecklistRepository( TaasDbContext context ) : base( context ) {

        }

        public void CreateOneChecklist( Checklist checklist ) => Create( checklist );

        public void DeleteOneChecklist( Checklist checklist ) => Delete( checklist );

        public async Task<List<Checklist>> GetAllChecklists( bool trackChanges ) =>
            await FindAll( trackChanges )
                .ToListAsync();

        public async Task<List<Checklist>> GetChecklistsByAuditAssignmentIdAndAuditTypeId( long auditAssignmentId, long audiTypeId, bool trackChanges ) =>
            await FindByCondition( b => b.AuditAssignmentId == auditAssignmentId && b.AuditTypeId == audiTypeId, trackChanges )
                .Include( b => b.ReviewedAuditor )
                .Include( b => b.ChecklistTemplate )
                .Include( b => b.AuditProgram )
                .ThenInclude( b => b.Institution )
                .Include( b => b.AuditProgram )
                .ThenInclude( b => b.KeyRequirement )
                .Include( b => b.AuditProgram )
                .ThenInclude( b => b.SpecificFunction )
                .Include( b => b.ChecklistAuditors )
                .ThenInclude( b => b.Auditor )
                .ToListAsync();

        public async Task<List<Checklist>> GetChecklistsWithDetailsByAuditAssignmentIdAndAuditTypeId( long auditAssignmentId, long audiTypeId, bool trackChanges ) =>
            await FindByCondition( b => b.AuditAssignmentId == auditAssignmentId && b.AuditTypeId == audiTypeId, trackChanges )
                .Include( b => b.ChecklistTaasFiles )
                .ThenInclude( b => b.TaasFile )
                .Include( b => b.ChecklistHeaders )
                .Include( b => b.ChecklistDetails )
                .ThenInclude( b => b.ChecklistDetailTaasFiles )
                .ThenInclude( b => b.TaasFile )
                .ToListAsync();

        public async Task<Checklist?> GetOneChecklistById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
                .Include( b => b.ChecklistTemplate )
                .SingleOrDefaultAsync();
    }
}
