using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Infrastructure.Data;
using TAAS.NetMAUI.Infrastructure.Interfaces;

namespace TAAS.NetMAUI.Infrastructure.Repositories {
    public class TaasFileRepository : BaseRepository<TaasFile>, ITaasFileRepository {

        public TaasFileRepository( TaasDbContext context ) : base( context ) {

        }

        public void CreateOneTaasFile( TaasFile taasFile ) => Create( taasFile );

        public async Task<List<TaasFile>> GetAllTaasFilesByChecklistId( long checklistId, bool trackChanges ) =>
            await FindByCondition( b => b.ChecklistTaasFiles.Any( i => i.ChecklistId == checklistId && ( !i.Deleted.HasValue || ( i.Deleted.HasValue && !i.Deleted.Value ) ) ), trackChanges ).ToListAsync();
        public async Task<TaasFile?> GetOneTaasFileByApiId( long apiId, bool trackChanges ) =>
            await FindByCondition( b => b.ApiId == apiId, trackChanges )
            .SingleOrDefaultAsync();

        public async Task<TaasFile?> GetOneTaasFileById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
            .SingleOrDefaultAsync();

        public void UpdateOneTaasFile( TaasFile taasFile ) => Update( taasFile );
    }
}
