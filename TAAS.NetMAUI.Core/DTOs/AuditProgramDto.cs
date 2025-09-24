using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Core.DTOs {
    public class AuditProgramDto : BaseDto {
        public long InstitutionId { get; set; }
        public InstitutionDto Institution { get; set; }

        public long? KeyRequirementId { get; set; }
        public KeyRequirementDto? KeyRequirement { get; set; }

        public long? SpecificFunctionId { get; set; }
        public SpecificFunctionDto? SpecificFunction { get; set; }
    }
}
