using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Core.DTOs {
    public class ChecklistDto : BaseDto {
        public long AuditAssignmentId { get; set; }
        public AuditAssignmentDto AuditAssignment { get; set; }
        public long AuditTypeId { get; set; }
        public AuditTypeDto AuditType { get; set; }

        public long? AuditProgramId { get; set; }
        public AuditProgramDto? AuditProgram { get; set; }

        public String? Comment { get; set; }
        public long? SamplingRowNumber { get; set; }

        public long ChecklistTemplateId { get; set; }
        public ChecklistTemplateDto ChecklistTemplate { get; set; }

        public bool? Turkish { get; set; }

        public string? Status { get; set; }

        public long? ReviewedAuditorId { get; set; }
        public AuditorDto? ReviewedAuditor { get; set; }


        public ICollection<ChecklistAuditorDto> ChecklistAuditors { get; set; }
        public ICollection<ChecklistHeaderDto> ChecklistHeaders { get; set; }
        public ICollection<ChecklistDetailDto> ChecklistDetails { get; set; }
        public ICollection<ChecklistTaasFileDto> ChecklistTaasFiles { get; set; }
        public ICollection<TaasFileDto> TaasFiles { get; set; }

    }
}
