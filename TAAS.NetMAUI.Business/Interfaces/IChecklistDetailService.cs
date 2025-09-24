using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Business.Interfaces {
    public interface IChecklistDetailService {

        Task<ChecklistDetailDto> GetById( long id, bool trackChanges );
        Task<List<ChecklistDetailDto>> GetByChecklistId( long checklistId, bool trackChanges );
        Task Update( long id, ChecklistDetailAnswerUpdateDto checklistDetailDto, bool trackChanges );
        Task Update( long id, ChecklistDetailExplanationUpdateDto checklistDetailDto, bool trackChanges );
        Task Update( long id, ChecklistDetailExplanationFormattedUpdateDto checklistDetailDto, bool trackChanges );
    }
}
