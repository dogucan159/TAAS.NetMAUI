using AutoMapper;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Infrastructure.Interfaces;

namespace TAAS.NetMAUI.Business.Services {
    public class ChecklistDetailTaasFileService : IChecklistDetailTaasFileService {

        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ChecklistDetailTaasFileService( IRepositoryManager manager, IMapper mapper ) {
            _manager = manager;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task Create( ChecklistDetailTaasFileCreateDto checklistDetailTaasFileDto ) {
            var checklistDetailTaasFile = _mapper.Map<ChecklistDetailTaasFile>( checklistDetailTaasFileDto );
            _manager.ChecklistDetailTaasFile.CreateOneChecklistDetailTaasFile( checklistDetailTaasFile );
            await _manager.SaveAsync();
        }

        public async System.Threading.Tasks.Task CreateList( List<ChecklistDetailTaasFileCreateDto> checklistDetailTaasFileDtos ) {
            var checklistDetailTaasFiles = _mapper.Map<List<ChecklistDetailTaasFile>>( checklistDetailTaasFileDtos );
            foreach ( var checklistDetailTaasFile in checklistDetailTaasFiles ) 
                _manager.ChecklistDetailTaasFile.CreateOneChecklistDetailTaasFile( checklistDetailTaasFile );
            await _manager.SaveAsync();
        }

        public async Task<List<ChecklistDetailTaasFileDto>> GetByChecklistDetailId( long checklistDetailId, bool trackChanges ) {
            var checklistDetailTaasFiles = await _manager.ChecklistDetailTaasFile.GetAllChecklistDetailTaasFilesByChecklistDetailId( checklistDetailId, trackChanges );
            return _mapper.Map<List<ChecklistDetailTaasFileDto>>( checklistDetailTaasFiles );
        }

        public async Task<List<ChecklistDetailTaasFileDto>> GetByTaasFileId( long taasFileId, bool trackChanges ) {
            var checklistDetailTaasFiles = await _manager.ChecklistDetailTaasFile.GetAllChecklistDetailTaasFilesByTaasFileId( taasFileId, trackChanges );
            return _mapper.Map<List<ChecklistDetailTaasFileDto>>( checklistDetailTaasFiles );
        }

        public async System.Threading.Tasks.Task SoftDeleteList( List<ChecklistDetailTaasFileDeleteDto> checklistDetailTaasFileDtos, bool trackChanges ) {
            foreach ( var checklistDetailTaasFileDto in checklistDetailTaasFileDtos ) {
                var checklistDetailTaasFile = await _manager.ChecklistDetailTaasFile.GetOneChecklistDetailTaasFileById( checklistDetailTaasFileDto.Id, trackChanges );
                _mapper.Map( checklistDetailTaasFileDto, checklistDetailTaasFile );
                _manager.ChecklistDetailTaasFile.UpdateOneChecklistDetailTaasFile( checklistDetailTaasFile );
            }
            await _manager.SaveAsync();
        }
    }
}
