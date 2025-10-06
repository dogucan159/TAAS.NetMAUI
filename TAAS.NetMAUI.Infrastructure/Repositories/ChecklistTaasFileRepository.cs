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
    public class ChecklistTaasFileRepository : BaseRepository<ChecklistTaasFile>, IChecklistTaasFileRepository {

        public ChecklistTaasFileRepository( TaasDbContext context ) : base( context ) {

        }

        public async Task<ChecklistTaasFile?> GetOneChecklistTaasFileByChecklistIdAndTaasFileId( long checklistId, long taasFileId, bool trackChanges ) =>
            await FindByCondition( b => b.ChecklistId == checklistId && b.TaasFileId == taasFileId, trackChanges )
            .SingleOrDefaultAsync();


        public void CreateOneChecklistTaasFile( ChecklistTaasFile checklistTaasFile ) => Create( checklistTaasFile );
        public void UpdateOneChecklistTaasFile( ChecklistTaasFile checklistTaasFile ) => Update( checklistTaasFile );

        public async Task<ChecklistTaasFile> GetOneChecklistTaasFileById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
            .Include( b => b.TaasFile )
            .FirstAsync();

        public async Task<List<ChecklistTaasFile>> GetAllChecklistTaasFilesByChecklistId( long checklistId, bool trackChanges ) =>
            await FindByCondition( b => b.ChecklistId == checklistId && ( !b.Deleted.HasValue || ( b.Deleted.HasValue && !b.Deleted.Value ) ), trackChanges )
            .Include( b => b.TaasFile )
            .ToListAsync();
    }
}
