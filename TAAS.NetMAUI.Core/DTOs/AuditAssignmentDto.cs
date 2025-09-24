using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.DTOs {
    public class AuditAssignmentDto : BaseDto {
        public long CoordinatorAuditorId { get; set; }
        public AuditorDto CoordinatorAuditor { get; set; }

        public long? StrategyPlanPeriodId { get; set; }
        public StrategyPlanPeriodDto? StrategyPlanPeriod { get; set; }
        public long? AuditPeriodId { get; set; }
        public AuditPeriodDto? AuditPeriod { get; set; }

        public long MainTaskId { get; set; }
        public MainTaskDto MainTask { get; set; }

        public long TaskTypeId { get; set; }
        public TaskTypeDto TaskType { get; set; }

        public long TaskId { get; set; }
        public TaskDto Task { get; set; }

        public ICollection<AuditAssignmentAuditorDto> AuditAssignmentAuditors { get; set; }
        public ICollection<AuditAssignmentTemporaryAuditorDto> AuditAssignmentTemporaryAuditors { get; set; }
        public ICollection<AuditAssignmentOperationAuditTypeDto> AuditAssignmentOperationAuditTypes { get; set; }
        public ICollection<AuditAssignmentFinancialAuditTypeDto> AuditAssignmentFinancialAuditTypes { get; set; }
    }
}
