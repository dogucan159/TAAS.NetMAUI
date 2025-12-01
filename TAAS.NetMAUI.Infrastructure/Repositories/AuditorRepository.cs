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
    public class AuditorRepository : BaseRepository<Auditor>, IAuditorRepository {

        public AuditorRepository( TaasDbContext context ) : base( context ) {

        }

        public void CreateOneAuditor( Auditor auditor ) => Create( auditor );

        public async Task<Auditor?> GetOneAuditorById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
            .SingleOrDefaultAsync();

        public async Task<Auditor?> GetOneAuditorByIdentificationNumber( string identificationNumber, bool trackChanges ) =>
            await FindByCondition( b => b.IdentificationNumber == identificationNumber, trackChanges )
            .SingleOrDefaultAsync();

        public async Task<Auditor?> GetOneAuditorByMachineName( bool trackChanges ) =>
            await FindByCondition( b => b.MachineName == System.Environment.MachineName, trackChanges )
            .SingleOrDefaultAsync();
    }
}
