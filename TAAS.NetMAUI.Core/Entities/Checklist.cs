using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class Checklist : BaseEntity {
        public long AuditAssignmentId { get; set; }
        public AuditAssignment AuditAssignment { get; set; }
        public long AuditTypeId { get; set; }
        public AuditType AuditType { get; set; }

        public long? AuditProgramId { get; set; }
        public AuditProgram? AuditProgram { get; set; }

        public String? Comment { get; set; }
        public long? SamplingRowNumber { get; set; }

        public long ChecklistTemplateId { get; set; }
        public ChecklistTemplate ChecklistTemplate { get; set; }

        public bool? Turkish { get; set; }
        public string? Status { get; set; }

        public long? ReviewedAuditorId { get; set; }
        public Auditor? ReviewedAuditor { get; set; }

        public ICollection<ChecklistAuditor> ChecklistAuditors { get; set; }
        public ICollection<ChecklistHeader> ChecklistHeaders { get; set; }
        public ICollection<ChecklistDetail> ChecklistDetails { get; set; }
        public ICollection<ChecklistTaasFile> ChecklistTaasFiles { get; set; }

    }
}
