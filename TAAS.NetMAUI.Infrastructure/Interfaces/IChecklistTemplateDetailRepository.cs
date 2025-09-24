using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IChecklistTemplateDetailRepository {
        Task<ChecklistTemplateDetail?> GetOneChecklistTemplateDetailById( long id, bool trackChanges );
    }
}
