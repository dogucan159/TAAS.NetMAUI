using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IChecklistDetailRepository {
        Task<List<ChecklistDetail>> GetAllChecklistDetailByChecklistId( long checklistId, bool trackChanges );
        Task<ChecklistDetail?> GetOneChecklistDetailById( long id, bool trackChanges );
        void UpdateOneChecklistDetail( ChecklistDetail checklistDetail );
    }
}
