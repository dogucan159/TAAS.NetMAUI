using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Infrastructure.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TAAS.NetMAUI.Business.Services {
    public class ChecklistHeaderService : IChecklistHeaderService {

        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ChecklistHeaderService( IRepositoryManager manager, IMapper mapper ) {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<List<ChecklistHeaderDto>> GetByChecklistId( long checklistId, bool trackChanges ) {
            var result = await _manager.ChecklistHeader.GetAllChecklistHeaderByChecklistId( checklistId, trackChanges );
            return _mapper.Map<List<ChecklistHeaderDto>>( result );
        }

        public async Task Update( long id, ChecklistHeaderUpdateDto checklistHeaderDto, bool trackChanges ) {
            var dbChecklistHeader = await _manager.ChecklistHeader.GetOneChecklistHeaderById( id, trackChanges );
            _mapper.Map( checklistHeaderDto, dbChecklistHeader );
            _manager.ChecklistHeader.UpdateOneChecklistHeader( dbChecklistHeader );
            await _manager.SaveAsync();
        }
    }
}
