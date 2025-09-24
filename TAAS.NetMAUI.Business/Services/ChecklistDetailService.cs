using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Infrastructure.Interfaces;

namespace TAAS.NetMAUI.Business.Services {
    public class ChecklistDetailService : IChecklistDetailService {

        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ChecklistDetailService( IRepositoryManager manager, IMapper mapper ) {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<List<ChecklistDetailDto>> GetByChecklistId( long checklistId, bool trackChanges ) {
            var result = await _manager.ChecklistDetail.GetAllChecklistDetailByChecklistId( checklistId, trackChanges );
            return _mapper.Map<List<ChecklistDetailDto>>( result );
        }

        public async Task<ChecklistDetailDto> GetById( long id, bool trackChanges ) {
            var result = await _manager.ChecklistDetail.GetOneChecklistDetailById( id, false );
            return _mapper.Map<ChecklistDetailDto>( result );
        }

        public async Task Update( long id, ChecklistDetailAnswerUpdateDto checklistDetailDto, bool trackChanges ) {
            var dbChecklistDetail = await _manager.ChecklistDetail.GetOneChecklistDetailById( id, trackChanges );

            _mapper.Map( checklistDetailDto, dbChecklistDetail );
            _manager.ChecklistDetail.UpdateOneChecklistDetail( dbChecklistDetail );
            await _manager.SaveAsync();

        }

        public async Task Update( long id, ChecklistDetailExplanationUpdateDto checklistDetailDto, bool trackChanges ) {
            var dbChecklistDetail = await _manager.ChecklistDetail.GetOneChecklistDetailById( id, trackChanges );
            _mapper.Map( checklistDetailDto, dbChecklistDetail );
            _manager.ChecklistDetail.UpdateOneChecklistDetail( dbChecklistDetail );
            await _manager.SaveAsync();
        }

        public async Task Update( long id, ChecklistDetailExplanationFormattedUpdateDto checklistDetailDto, bool trackChanges ) {
            var dbChecklistDetail = await _manager.ChecklistDetail.GetOneChecklistDetailById( id, trackChanges );
            _mapper.Map( checklistDetailDto, dbChecklistDetail );
            _manager.ChecklistDetail.UpdateOneChecklistDetail( dbChecklistDetail );
            await _manager.SaveAsync();
        }
    }
}
