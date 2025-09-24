
namespace TAAS.NetMAUI.Core.Entities {
    public class AuditAssignment : BaseEntity {
        public long CoordinatorAuditorId { get; set; }
        public Auditor CoordinatorAuditor { get; set; }

        public long? StrategyPlanPeriodId { get; set; }
        public StrategyPlanPeriod? StrategyPlanPeriod { get; set; }
        public long? AuditPeriodId { get; set; }
        public AuditPeriod? AuditPeriod { get; set; }

        public long MainTaskId { get; set; }
        public MainTask MainTask { get; set; }

        public long TaskTypeId { get; set; }
        public TaskType TaskType { get; set; }

        public long TaskId { get; set; }
        public Task Task { get; set; }

        public ICollection<AuditAssignmentAuditor> AuditAssignmentAuditors { get; set; }
        public ICollection<AuditAssignmentTemporaryAuditor> AuditAssignmentTemporaryAuditors { get; set; }
        public ICollection<AuditAssignmentOperationAuditType> AuditAssignmentOperationAuditTypes { get; set; }
        public ICollection<AuditAssignmentFinancialAuditType> AuditAssignmentFinancialAuditTypes { get; set; }
    }
}
