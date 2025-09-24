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
    public class ChecklistTemplateRepository : BaseRepository<ChecklistTemplate>, IChecklistTemplateRepository {
        public ChecklistTemplateRepository( TaasDbContext context ) : base( context ) {

        }

        public void CreateOneChecklistTemplate( ChecklistTemplate checklistTemplate ) => Create( checklistTemplate );

        public void DeleteOneChecklistTemplate( ChecklistTemplate checklistTemplate ) => Delete( checklistTemplate );

        public async Task<ChecklistTemplate?> GetOneChecklistTemplateById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
            .SingleOrDefaultAsync();
    }
}
