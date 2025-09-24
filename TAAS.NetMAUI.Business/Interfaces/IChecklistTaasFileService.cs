using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Business.Interfaces {
    public interface IChecklistTaasFileService {
        Task<ChecklistTaasFileDto> GetById( long id, bool trackChanges );
        Task<List<ChecklistTaasFileDto>> GetByChecklistId( long checklistId, bool trackChanges );
        Task Create( ChecklistTaasFileCreateDto checklistTaasFileDto );
        Task SoftDelete( ChecklistTaasFileDeleteDto checklistTaasFileDto, bool trackChanges );
    }
}
