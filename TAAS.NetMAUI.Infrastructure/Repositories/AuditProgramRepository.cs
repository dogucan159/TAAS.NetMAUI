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
    public class AuditProgramRepository : BaseRepository<AuditProgram>, IAuditProgramRepository {

        public AuditProgramRepository( TaasDbContext context ) : base( context ) {

        }
        public async Task<AuditProgram?> GetOneAuditProgramById( long id, bool trackChanges ) => 
            await FindByCondition( b => b.Id == id, trackChanges )
            .SingleOrDefaultAsync();
    }
}
