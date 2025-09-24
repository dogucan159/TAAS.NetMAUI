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
    public class TaskTypeRepository : BaseRepository<TaskType>, ITaskTypeRepository {

        public TaskTypeRepository( TaasDbContext context ) : base( context ) {

        }

        public async Task<TaskType?> GetOneTaskTypeById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
            .SingleOrDefaultAsync();
    }
}
