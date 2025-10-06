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
    public class ChecklistDetailRepository : BaseRepository<ChecklistDetail>, IChecklistDetailRepository {

        public ChecklistDetailRepository( TaasDbContext context ) : base( context ) {

        }

        public async Task<List<ChecklistDetail>> GetAllChecklistDetailByChecklistId( long checklistId, bool trackChanges ) =>
            await FindByCondition( b => b.ChecklistId == checklistId, trackChanges )
            .Include( b => b.ChecklistTemplateDetail )
            .Include( b => b.ChecklistDetailTaasFiles )
            .ThenInclude( b => b.TaasFile )
            .ToListAsync();

        public async Task<ChecklistDetail?> GetOneChecklistDetailById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
            .SingleOrDefaultAsync();

        public void UpdateOneChecklistDetail( ChecklistDetail checklistDetail ) => Update( checklistDetail );
    }
}
