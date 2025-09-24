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
    public class ChecklistService : IChecklistService {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public ChecklistService( IRepositoryManager manager, IMapper mapper ) {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<List<ChecklistDto>> GetChecklistsByAuditAssignmentIdAndAuditTypeId( long auditAssignmentId, long auditTypeId, bool trackChanges ) {
            var checklists = await _manager.Checklist.GetChecklistsByAuditAssignmentIdAndAuditTypeId( auditAssignmentId, auditTypeId, trackChanges );
            return _mapper.Map<List<ChecklistDto>>( checklists );
        }

        public async Task<List<ChecklistDto>> GetChecklistsWithDetailsByAuditAssignmentIdAndAuditTypeId( long auditAssignmentId, long auditTypeId, bool trackChanges ) {
            var checklists = await _manager.Checklist.GetChecklistsWithDetailsByAuditAssignmentIdAndAuditTypeId( auditAssignmentId, auditTypeId, trackChanges );
            return _mapper.Map<List<ChecklistDto>>( checklists );
        }
    }
}
