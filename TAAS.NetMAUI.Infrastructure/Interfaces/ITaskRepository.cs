using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface ITaskRepository {
        Task<Core.Entities.Task?> GetOneTaskById( long id, bool trackChanges );
    }
}
