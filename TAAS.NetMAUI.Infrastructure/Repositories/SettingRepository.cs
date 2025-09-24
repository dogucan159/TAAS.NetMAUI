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
    public class SettingRepository : BaseRepository<Setting>, ISettingRepository {

        public SettingRepository( TaasDbContext context ) : base( context ) {

        }
        public void CreateOneSetting( Setting setting ) => Create( setting );

        public void DeleteOneSetting( Setting setting ) => Delete( setting );

        public void UpdateOneSetting( Setting setting ) => Update( setting );
        public async Task<List<Setting>> GetAllSettings( bool trackChanges ) => await FindAll( trackChanges ).ToListAsync();

        public async Task<Setting?> GetOneSettingByKey( string key, bool trackChanges ) =>
            await FindByCondition( b => b.Key == key, trackChanges )
            .SingleOrDefaultAsync();

        public async Task<Setting?> GetOneSettingById( long id, bool trackChanges ) =>
            await FindByCondition( b => b.Id == id, trackChanges )
            .SingleOrDefaultAsync();
    }
}
