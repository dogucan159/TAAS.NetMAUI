using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IChecklistTaasFileRepository {
        Task<ChecklistTaasFile> GetOneChecklistTaasFileById( long id, bool trackChanges );
        Task<ChecklistTaasFile?> GetOneChecklistTaasFileByChecklistIdAndTaasFileId( long checklistId, long taasFileId, bool trackChanges );
        Task<List<ChecklistTaasFile>> GetAllChecklistTaasFilesByChecklistId( long checklistId, bool trackChanges );
        void CreateOneChecklistTaasFile( ChecklistTaasFile checklistTaasFile );

        void UpdateOneChecklistTaasFile( ChecklistTaasFile checklistTaasFile );
    }
}
