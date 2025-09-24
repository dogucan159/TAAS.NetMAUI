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
    public class AuditPeriodRepository : BaseRepository<AuditPeriod>, IAuditPeriodRepository {

        public AuditPeriodRepository( TaasDbContext context ) : base( context ) {

        }

        public async Task<AuditPeriod?> GetOneAuditPeriodById( long id, bool trackChanges ) => 
            await FindByCondition( b => b.Id == id, trackChanges )
            .SingleOrDefaultAsync();
    }
}
