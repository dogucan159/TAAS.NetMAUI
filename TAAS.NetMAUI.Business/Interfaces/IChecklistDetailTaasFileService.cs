using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Business.Interfaces {
    public interface IChecklistDetailTaasFileService {
        Task<List<ChecklistDetailTaasFileDto>> GetByChecklistDetailId( long checklistDetailId, bool trackChanges );
        Task<List<ChecklistDetailTaasFileDto>> GetByTaasFileId( long taasFileId, bool trackChanges );
        Task Create( ChecklistDetailTaasFileCreateDto checklistDetailTaasFileDto );
        Task CreateList( List<ChecklistDetailTaasFileCreateDto> checklistDetailTaasFileDtos );
        Task SoftDeleteList( List<ChecklistDetailTaasFileDeleteDto> checklistDetailTaasFileDtos, bool trackChanges );
    }
}
