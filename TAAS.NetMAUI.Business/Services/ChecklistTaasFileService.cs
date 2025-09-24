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

namespace TAAS.NetMAUI.Business.Services {
    public class ChecklistTaasFileService : IChecklistTaasFileService {

        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ChecklistTaasFileService( IRepositoryManager manager, IMapper mapper ) {
            _manager = manager;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task Create( ChecklistTaasFileCreateDto checklistTaasFileDto ) {
            var checklistTaasFile = _mapper.Map<ChecklistTaasFile>( checklistTaasFileDto );
            _manager.ChecklistTaasFile.CreateOneChecklistTaasFile( checklistTaasFile );
            await _manager.SaveAsync();
        }

        public async Task<List<ChecklistTaasFileDto>> GetByChecklistId( long checklistId, bool trackChanges ) {
            var checklistTaasFiles = await _manager.ChecklistTaasFile.GetAllChecklistTaasFilesByChecklistId( checklistId, trackChanges );
            return _mapper.Map<List<ChecklistTaasFileDto>>( checklistTaasFiles );
        }

        public async Task<ChecklistTaasFileDto> GetById( long id, bool trackChanges ) {
            var checklistTaasFile = await _manager.ChecklistTaasFile.GetOneChecklistTaasFileById( id, trackChanges );
            return _mapper.Map<ChecklistTaasFileDto>( checklistTaasFile );
        }

        public async System.Threading.Tasks.Task SoftDelete( ChecklistTaasFileDeleteDto checklistTaasFileDto, bool trackChanges ) {
            var checklistTaasFile = await _manager.ChecklistTaasFile.GetOneChecklistTaasFileById( checklistTaasFileDto.Id, trackChanges );
            _mapper.Map( checklistTaasFileDto, checklistTaasFile );
            _manager.ChecklistTaasFile.UpdateOneChecklistTaasFile( checklistTaasFile );
            await _manager.SaveAsync();
        }
    }
}
