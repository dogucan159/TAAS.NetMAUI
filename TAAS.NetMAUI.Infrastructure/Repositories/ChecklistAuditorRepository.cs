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
    public class ChecklistAuditorRepository : BaseRepository<ChecklistAuditor>, IChecklistAuditorRepository {

        public ChecklistAuditorRepository( TaasDbContext context ) : base( context ) {

        }

        public async Task<ChecklistAuditor?> GetOneChecklistAuditorByChecklistIdAndAuditorId( long checklistId, long auditorId, bool trackChanges ) =>
            await FindByCondition( b => b.ChecklistId == checklistId && b.AuditorId == auditorId, trackChanges )
            .SingleOrDefaultAsync();

        public async Task<ChecklistAuditor?> GetOneChecklistAuditorById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
                .SingleOrDefaultAsync();
    }
}
