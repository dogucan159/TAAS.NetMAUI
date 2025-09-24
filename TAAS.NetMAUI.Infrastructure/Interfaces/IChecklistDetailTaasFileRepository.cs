using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IChecklistDetailTaasFileRepository {
        Task<ChecklistDetailTaasFile> GetOneChecklistDetailTaasFileById( long id, bool trackChanges );
        Task<List<ChecklistDetailTaasFile>> GetAllChecklistDetailTaasFilesByChecklistDetailId( long checklistDetailId, bool trackChanges );
        Task<List<ChecklistDetailTaasFile>> GetAllChecklistDetailTaasFilesByTaasFileId( long taasFileId, bool trackChanges );
        Task<ChecklistDetailTaasFile?> GetOneChecklistDetailTaasFileByChecklistDetailIdAndTaasFileId( long checklistDetailId, long taasFileId, bool trackChanges );
        void CreateOneChecklistDetailTaasFile( ChecklistDetailTaasFile checklistDetailTaasFile );

        void UpdateOneChecklistDetailTaasFile( ChecklistDetailTaasFile checklistDetailTaasFile );
    }
}
