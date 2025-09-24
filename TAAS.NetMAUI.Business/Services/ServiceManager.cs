using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Business.Interfaces;

namespace TAAS.NetMAUI.Business.Services {
    public class ServiceManager : IServiceManager {
        private readonly IAuditorService _auditorService;
        private readonly IAuditAssignmentService _auditAssignmentService;
        private readonly IChecklistService _checklistService;
        private readonly IChecklistHeaderService _checklistHeaderService;
        private readonly IChecklistDetailService _checklistDetailService;
        private readonly IChecklistTaasFileService _checklistTaasFileService;
        private readonly IChecklistDetailTaasFileService _checklistDetailTaasFileService;
        private readonly IApiService _apiService;

        public ServiceManager( IAuditorService auditorService, IAuditAssignmentService auditAssignmentService, IApiService apiService, IChecklistService checklistService, IChecklistHeaderService checklistHeaderService, IChecklistDetailService checklistDetailService, IChecklistTaasFileService checklistTaasFileService, IChecklistDetailTaasFileService checklistDetailTaasFileService ) {
            _auditorService = auditorService;
            _auditAssignmentService = auditAssignmentService;
            _apiService = apiService;
            _checklistService = checklistService;
            _checklistHeaderService = checklistHeaderService;
            _checklistDetailService = checklistDetailService;
            _checklistTaasFileService = checklistTaasFileService;
            _checklistDetailTaasFileService = checklistDetailTaasFileService;
        }

        public IAuditorService AuditorService => _auditorService;

        public IAuditAssignmentService AuditAssignmentService => _auditAssignmentService;

        public IApiService ApiService => _apiService;

        public IChecklistService ChecklistService => _checklistService;

        public IChecklistHeaderService ChecklistHeaderService => _checklistHeaderService;

        public IChecklistDetailService ChecklistDetailService => _checklistDetailService;

        public IChecklistTaasFileService ChecklistTaasFileService => _checklistTaasFileService;

        public IChecklistDetailTaasFileService ChecklistDetailTaasFileService => _checklistDetailTaasFileService;
    }
}
