namespace TAAS.NetMAUI.Business.Interfaces {
    public interface IServiceManager {

        IAuditorService AuditorService { get; }
        IAuditAssignmentService AuditAssignmentService { get; }
        IChecklistService ChecklistService { get; }
        IChecklistHeaderService ChecklistHeaderService { get; }
        IChecklistDetailService ChecklistDetailService { get; }
        IChecklistTaasFileService ChecklistTaasFileService { get; }
        IChecklistDetailTaasFileService ChecklistDetailTaasFileService { get; }
        IApiService ApiService { get; }
    }
}
