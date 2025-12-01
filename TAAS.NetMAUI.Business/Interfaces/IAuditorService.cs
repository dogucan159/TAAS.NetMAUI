using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Business.Interfaces {
    public interface IAuditorService {
        Task<AuditorDto> GetById( long id, bool trackChanges );
        Task<AuditorDto> GetByIdentificationNumber( String identificationNumber, bool trackChanges );
        Task<AuditorDto> GetByMachineName( bool trackChanges );
    }
}
