using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IAuditorRepository {
        Task<Auditor?> GetOneAuditorById( long id, bool trackChanges );
        Task<Auditor?> GetOneAuditorByIdentificationNumber( String identificationNumber, bool trackChanges );
        void CreateOneAuditor( Auditor auditor );
    }
}
