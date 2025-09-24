using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class AuditProgram : BaseEntity {
        public long InstitutionId { get; set; }
        public Institution Institution { get; set; }

        public long? KeyRequirementId { get; set; }
        public KeyRequirement? KeyRequirement { get; set; }

        public long? SpecificFunctionId { get; set; }
        public SpecificFunction? SpecificFunction { get; set; }
    }
}
