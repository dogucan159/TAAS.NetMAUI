using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface ISettingRepository {
        Task<List<Setting>> GetAllSettings( bool trackChanges );
        Task<Setting?> GetOneSettingByKey( String key, bool trackChanges );
        void CreateOneSetting( Setting setting );
        void UpdateOneSetting( Setting setting );
        void DeleteOneSetting( Setting setting );
        Task<Setting?> GetOneSettingById( long id, bool trackChanges );

    }
}
