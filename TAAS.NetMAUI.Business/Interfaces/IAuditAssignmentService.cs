using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Business.Interfaces {
    public interface IAuditAssignmentService {
        Task<List<AuditAssignmentDto>> GetAllAuditAssignments( bool trackChanges );
    }
}
