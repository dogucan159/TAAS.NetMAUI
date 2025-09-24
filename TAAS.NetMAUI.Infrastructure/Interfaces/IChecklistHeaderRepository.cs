using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IChecklistHeaderRepository {
        Task<List<ChecklistHeader>> GetAllChecklistHeaderByChecklistId( long checklistId, bool trackChanges );
        Task<ChecklistHeader?> GetOneChecklistHeaderById( long id, bool trackChanges );

        void UpdateOneChecklistHeader( ChecklistHeader checklistHeader );
    }
}
