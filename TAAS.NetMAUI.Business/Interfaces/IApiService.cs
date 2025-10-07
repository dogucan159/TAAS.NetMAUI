using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Business.Interfaces {
    public interface IApiService {
        Task<List<AuditAssignmentDto>?> PullAuditAssignmentsByMainTaskFromAPI( string code, string token );
        Task<List<AuditAssignmentDto>?> PullAuditAssignmentsByTaskTypeFromAPI( string code, string token );
        Task<List<AuditAssignmentDto>?> PullAuditAssignmentsByTaskFromAPI( string code, string token );
        Task<List<ChecklistDto>?> PullChecklistsByAuditAssignmentIdAndAuditTypeIdFromAPI( long auditAssignmentId, long auditTypeId, string token );
        Task<ChecklistDto?> PullChecklistFromAPI( long id, string token );
        System.Threading.Tasks.Task SyncAuditAssignmentAsync( AuditAssignmentDto auditAssignmentDto );
        System.Threading.Tasks.Task SyncChecklistAsync( ChecklistDto checklistDto, string token );
        System.Threading.Tasks.Task TransferChecklistsToLive( List<ChecklistDto> lstChecklistDto, String token );
    }
}
