using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Infrastructure;
using TAAS.NetMAUI.Infrastructure.Interfaces;

namespace TAAS.NetMAUI.Business.Services {
    public class AuditorService : IAuditorService {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public AuditorService( IRepositoryManager manager, IMapper mapper ) {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<AuditorDto> GetById( long id, bool trackChanges ) {
            var auditor = await _manager.Auditor.GetOneAuditorById( id, trackChanges );
            return _mapper.Map<AuditorDto>( auditor );
        }

        public async Task<AuditorDto> GetByIdentificationNumber( string identificationNumber, bool trackChanges ) {
            var auditor = await _manager.Auditor.GetOneAuditorByIdentificationNumber( identificationNumber, trackChanges );
            return _mapper.Map<AuditorDto>( auditor );
        }
    }
}
