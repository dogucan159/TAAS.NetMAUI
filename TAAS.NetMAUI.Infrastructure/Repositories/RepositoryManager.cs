using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Infrastructure.Data;
using TAAS.NetMAUI.Infrastructure.Interfaces;

namespace TAAS.NetMAUI.Infrastructure.Repositories {
    public class RepositoryManager : IRepositoryManager {


        private readonly TaasDbContext _context;
        private readonly IAuditorRepository _auditorRepository;
        private readonly IAuditAssignmentRepository _auditAssignmentRepository;
        private readonly IStrategyPlanPeriodRepository _strategyPlanPeriodRepository;
        private readonly IAuditPeriodRepository _auditPeriodRepository;
        private readonly IMainTaskRepository _mainTaskRepository;
        private readonly ITaskTypeRepository _taskTypeRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IAuditTypeRepository _auditTypeRepository;
        private readonly IAuditAssignmentAuditorRepository _auditAssignmentAuditorRepository;
        private readonly IAuditAssignmentTemporaryAuditorRepository _auditAssignmentTemporaryAuditorRepository;
        private readonly IAuditAssignmentOperationAuditTypeRepository _auditAssignmentOperationAuditTypeRepository;
        private readonly IAuditAssignmentFinancialAuditTypeRepository _auditAssignmentFinancialAuditTypeRepository;
        private readonly IChecklistTemplateRepository _checklistTemplateRepository;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IKeyRequirementRepository _keyRequirementRepository;
        private readonly ISpecificFunctionRepository _specificFunctionRepository;
        private readonly IAuditProgramRepository _auditProgramRepository;
        private readonly IChecklistRepository _checklistRepository;
        private readonly IChecklistAuditorRepository _checklistAuditorRepository;
        private readonly IChecklistTemplateDetailRepository _checklistTemplateDetailRepository;
        private readonly IChecklistDetailRepository _checklistDetailRepository;

        private readonly IChecklistTemplateHeaderRepository _checklistTemplateHeaderRepository;
        private readonly IChecklistHeaderRepository _checklistHeaderRepository;
        private readonly IChecklistTaasFileRepository _checklistTaasFileRepository;
        private readonly IChecklistDetailTaasFileRepository _checklistDetailTaasFileRepository;
        private readonly ITaasFileRepository _taasFileRepository;

        public RepositoryManager( TaasDbContext context,
            IAuditorRepository auditorRepository,
            IAuditAssignmentRepository auditAssignmentRepository,
            IStrategyPlanPeriodRepository strategyPlanPeriodRepository,
            IAuditPeriodRepository auditPeriodRepository,
            IMainTaskRepository mainTaskRepository,
            ITaskTypeRepository taskTypeRepository,
            ITaskRepository taskRepository,
            IAuditTypeRepository auditTypeRepository,
            IChecklistTemplateRepository checklistTemplateRepository,
            IAuditAssignmentAuditorRepository auditAssignmentAuditorRepository,
            IAuditAssignmentTemporaryAuditorRepository auditAssignmentTemporaryAuditorRepository,
            IAuditAssignmentOperationAuditTypeRepository auditAssignmentOperationAuditTypeRepository,
            IAuditAssignmentFinancialAuditTypeRepository auditAssignmentFinancialAuditTypeRepository,
            IInstitutionRepository institutionRepository,
            IKeyRequirementRepository keyRequirementRepository,
            ISpecificFunctionRepository specificFunctionRepository,
            IAuditProgramRepository auditProgramRepository,
            IChecklistRepository checklistRepository,
            IChecklistAuditorRepository checklistAuditorRepository,
            IChecklistTemplateDetailRepository checklistTemplateDetailRepository,
            IChecklistDetailRepository checklistDetailRepository,
            IChecklistTemplateHeaderRepository checklistTemplateHeaderRepository,
            IChecklistHeaderRepository checklistHeaderRepository,
            ITaasFileRepository taasFileRepository,
            IChecklistTaasFileRepository checklistTaasFileRepository,
            IChecklistDetailTaasFileRepository checklistDetailTaasFileRepository ) {
            _context = context;
            _auditorRepository = auditorRepository;
            _auditAssignmentRepository = auditAssignmentRepository;
            _strategyPlanPeriodRepository = strategyPlanPeriodRepository;
            _auditPeriodRepository = auditPeriodRepository;
            _mainTaskRepository = mainTaskRepository;
            _taskTypeRepository = taskTypeRepository;
            _taskRepository = taskRepository;
            _auditTypeRepository = auditTypeRepository;
            _auditAssignmentAuditorRepository = auditAssignmentAuditorRepository;
            _auditAssignmentTemporaryAuditorRepository = auditAssignmentTemporaryAuditorRepository;
            _auditAssignmentOperationAuditTypeRepository = auditAssignmentOperationAuditTypeRepository;
            _auditAssignmentFinancialAuditTypeRepository = auditAssignmentFinancialAuditTypeRepository;
            _checklistTemplateRepository = checklistTemplateRepository;
            _institutionRepository = institutionRepository;
            _keyRequirementRepository = keyRequirementRepository;
            _specificFunctionRepository = specificFunctionRepository;
            _auditProgramRepository = auditProgramRepository;
            _checklistRepository = checklistRepository;
            _checklistAuditorRepository = checklistAuditorRepository;
            _checklistTemplateDetailRepository = checklistTemplateDetailRepository;
            _checklistDetailRepository = checklistDetailRepository;
            _checklistTemplateHeaderRepository = checklistTemplateHeaderRepository;
            _checklistHeaderRepository = checklistHeaderRepository;
            _taasFileRepository = taasFileRepository;
            _checklistTaasFileRepository = checklistTaasFileRepository;
            _checklistDetailTaasFileRepository = checklistDetailTaasFileRepository;
        }
        public IAuditorRepository Auditor => _auditorRepository;

        public IAuditAssignmentRepository AuditAssignment => _auditAssignmentRepository;

        public IStrategyPlanPeriodRepository StrategyPlanPeriod => _strategyPlanPeriodRepository;

        public IAuditPeriodRepository AuditPeriod => _auditPeriodRepository;

        public IMainTaskRepository MainTask => _mainTaskRepository;

        public ITaskTypeRepository TaskType => _taskTypeRepository;

        public ITaskRepository Task => _taskRepository;

        public IAuditTypeRepository AuditType => _auditTypeRepository;

        public IChecklistTemplateRepository ChecklistTemplate => _checklistTemplateRepository;

        public IAuditAssignmentAuditorRepository AuditAssignmentAuditor => _auditAssignmentAuditorRepository;

        public IAuditAssignmentTemporaryAuditorRepository AuditAssignmentTemporaryAuditor => _auditAssignmentTemporaryAuditorRepository;

        public IAuditAssignmentOperationAuditTypeRepository AuditAssignmentOperationAuditType => _auditAssignmentOperationAuditTypeRepository;

        public IAuditAssignmentFinancialAuditTypeRepository AuditAssignmentFinancialAuditType => _auditAssignmentFinancialAuditTypeRepository;

        public IInstitutionRepository Institution => _institutionRepository;

        public IKeyRequirementRepository KeyRequirement => _keyRequirementRepository;

        public ISpecificFunctionRepository SpecificFunction => _specificFunctionRepository;

        public IAuditProgramRepository AuditProgram => _auditProgramRepository;

        public IChecklistRepository Checklist => _checklistRepository;

        public IChecklistAuditorRepository ChecklistAuditor => _checklistAuditorRepository;

        public IChecklistTemplateDetailRepository ChecklistTemplateDetail => _checklistTemplateDetailRepository;

        public IChecklistDetailRepository ChecklistDetail => _checklistDetailRepository;

        public IChecklistTemplateHeaderRepository ChecklistTemplateHeader => _checklistTemplateHeaderRepository;

        public IChecklistHeaderRepository ChecklistHeader => _checklistHeaderRepository;

        public ITaasFileRepository TaasFile => _taasFileRepository;

        public IChecklistTaasFileRepository ChecklistTaasFile => _checklistTaasFileRepository;

        public IChecklistDetailTaasFileRepository ChecklistDetailTaasFile => _checklistDetailTaasFileRepository;

        public async Task SaveAsync() {
            await _context.SaveChangesAsync();
        }
    }
}
