using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;

using TAAS.NetMAUI.Core.Entities;
namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IChecklistTemplateRepository {
        Task<ChecklistTemplate?> GetOneChecklistTemplateById( long id, bool trackChanges );
        void CreateOneChecklistTemplate( ChecklistTemplate checklistTemplate );
        void DeleteOneChecklistTemplate( ChecklistTemplate checklistTemplate );
    }
}
