using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IChecklistRepository {
        Task<Checklist?> GetOneChecklistById( long id, bool trackChanges );
        Task<List<Checklist>> GetChecklistsByAuditAssignmentIdAndAuditTypeId( long auditAssignmentId, long audiTypeId, bool trackChanges );
        Task<List<Checklist>> GetChecklistsWithDetailsByAuditAssignmentIdAndAuditTypeId( long auditAssignmentId, long audiTypeId, bool trackChanges );
        void CreateOneChecklist( Checklist checklist );
        void DeleteOneChecklist( Checklist checklist );
    }
}
