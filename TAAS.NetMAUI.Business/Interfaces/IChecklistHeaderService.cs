using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Business.Interfaces {
    public interface IChecklistHeaderService {

        Task<List<ChecklistHeaderDto>> GetByChecklistId( long checklistId, bool trackChanges );
        Task Update( long id, ChecklistHeaderUpdateDto checklistHeaderDto, bool trackChanges );   
    }
}
