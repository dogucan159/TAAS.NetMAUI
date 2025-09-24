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
    public class AuditAssignmentService : IAuditAssignmentService {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public AuditAssignmentService( IRepositoryManager manager, IMapper mapper ) {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<List<AuditAssignmentDto>> GetAllAuditAssignments( bool trackChanges ) {
            var auditAssignments = await _manager.AuditAssignment.GetAllAuditAssignments( trackChanges );
            return _mapper.Map<List<AuditAssignmentDto>>( auditAssignments );
        }
    }
}
