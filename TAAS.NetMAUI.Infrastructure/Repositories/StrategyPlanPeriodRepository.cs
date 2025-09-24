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
using TAAS.NetMAUI.Infrastructure.Repositories;

namespace TAAS.NetMAUI.Infrastructure {
    public class StrategyPlanPeriodRepository : BaseRepository<StrategyPlanPeriod>, IStrategyPlanPeriodRepository {

        public StrategyPlanPeriodRepository( TaasDbContext context ) : base( context ) {

        }

        public async Task<StrategyPlanPeriod?> GetOneStrategyPlanPeriodById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
            .SingleOrDefaultAsync();
    }
}
