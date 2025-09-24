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
    public class ChecklistDetailTaasFileRepository : BaseRepository<ChecklistDetailTaasFile>, IChecklistDetailTaasFileRepository {

        public ChecklistDetailTaasFileRepository( TaasDbContext context ) : base( context ) {

        }

        public void CreateOneChecklistDetailTaasFile( ChecklistDetailTaasFile checklistDetailTaasFile ) => Create( checklistDetailTaasFile );

        public async Task<List<ChecklistDetailTaasFile>> GetAllChecklistDetailTaasFilesByChecklistDetailId( long checklistDetailId, bool trackChanges ) =>
            await FindByCondition( b => b.ChecklistDetailId == checklistDetailId && ( !b.Deleted.HasValue || ( b.Deleted.HasValue && !b.Deleted.Value ) ), trackChanges )
            .Include( b => b.TaasFile )
            .ToListAsync();

        public async Task<List<ChecklistDetailTaasFile>> GetAllChecklistDetailTaasFilesByTaasFileId( long taasFileId, bool trackChanges ) =>
            await FindByCondition( b => b.TaasFileId == taasFileId && ( !b.Deleted.HasValue || ( b.Deleted.HasValue && !b.Deleted.Value ) ), trackChanges )
            .ToListAsync();

        public async Task<ChecklistDetailTaasFile?> GetOneChecklistDetailTaasFileByChecklistDetailIdAndTaasFileId( long checklistDetailId, long taasFileId, bool trackChanges ) =>
            await FindByCondition( b => b.ChecklistDetailId == checklistDetailId && b.TaasFileId == taasFileId, trackChanges )
            .SingleOrDefaultAsync();

        public async Task<ChecklistDetailTaasFile> GetOneChecklistDetailTaasFileById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
            .FirstAsync();

        public void UpdateOneChecklistDetailTaasFile( ChecklistDetailTaasFile checklistDetailTaasFile ) => Update( checklistDetailTaasFile );
    }
}
