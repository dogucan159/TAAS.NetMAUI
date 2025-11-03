using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Infrastructure.Interfaces;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface IRepositoryManager {
        IAuditorRepository Auditor { get; }
        IAuditAssignmentRepository AuditAssignment { get; }
        IStrategyPlanPeriodRepository StrategyPlanPeriod { get; }
        IAuditPeriodRepository AuditPeriod { get; }
        IMainTaskRepository MainTask { get; }
        ITaskTypeRepository TaskType { get; }
        ITaskRepository Task { get; }
        IAuditTypeRepository AuditType { get; }
        IChecklistTemplateRepository ChecklistTemplate { get; }
        IAuditAssignmentAuditorRepository AuditAssignmentAuditor { get; }
        IAuditAssignmentTemporaryAuditorRepository AuditAssignmentTemporaryAuditor { get; } 
        IAuditAssignmentOperationAuditTypeRepository AuditAssignmentOperationAuditType { get; }
        IAuditAssignmentFinancialAuditTypeRepository AuditAssignmentFinancialAuditType { get; }
        IInstitutionRepository Institution { get; }
        IKeyRequirementRepository KeyRequirement { get; }
        ISpecificFunctionRepository SpecificFunction { get; }
        IAuditProgramRepository AuditProgram { get; }
        IChecklistRepository Checklist { get; }
        IChecklistAuditorRepository ChecklistAuditor { get; }
        IChecklistTemplateDetailRepository ChecklistTemplateDetail { get; }
        IChecklistDetailRepository ChecklistDetail { get; }
        IChecklistTemplateHeaderRepository ChecklistTemplateHeader { get; }
        IChecklistHeaderRepository ChecklistHeader { get; }
        IChecklistTaasFileRepository ChecklistTaasFile { get; }
        IChecklistDetailTaasFileRepository ChecklistDetailTaasFile { get; }
        ITaasFileRepository TaasFile { get; }
        Task SaveAsync();
    }
}
