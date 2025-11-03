using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Shared;

namespace TAAS.NetMAUI.Infrastructure.Data {
    public class TaasDbContext : DbContext {

        public TaasDbContext( DbContextOptions<TaasDbContext> options ) : base( options ) {

        }
        public DbSet<Auditor> Auditors { get; set; }
        public DbSet<AuditAssignment> AuditAssignments { get; set; }
        public DbSet<MainTask> MainTasks { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<Core.Entities.Task> Tasks { get; set; }
        public DbSet<AuditType> AuditTypes { get; set; }
        public DbSet<AuditAssignmentAuditor> AuditAssignmentAuditors { get; set; }
        public DbSet<AuditAssignmentTemporaryAuditor> AuditAssignmentTemporaryAuditors { get; set; }
        public DbSet<AuditAssignmentOperationAuditType> AuditAssignmentOperationAuditTypes { get; set; }
        public DbSet<AuditAssignmentFinancialAuditType> AuditAssignmentFinancialAuditTypes { get; set; }
        public DbSet<StrategyPlanPeriod> StrategyPlanPeriods { get; set; }
        public DbSet<AuditPeriod> AuditPeriods { get; set; }
        public DbSet<ChecklistTemplate> ChecklistTemplates { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<KeyRequirement> KeyRequirements { get; set; }
        public DbSet<SpecificFunction> SpecificFunctions { get; set; }
        public DbSet<AuditProgram> AuditPrograms { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ChecklistAuditor> ChecklistAuditors { get; set; }
        public DbSet<ChecklistDetail> ChecklistDetails { get; set; }
        public DbSet<ChecklistTemplateDetail> ChecklistTemplateDetails { get; set; }
        public DbSet<ChecklistHeader> ChecklistHeaders { get; set; }
        public DbSet<ChecklistTemplateHeader> ChecklistTemplateHeaders { get; set; }
        public DbSet<ChecklistTaasFile> ChecklistTaasFiles { get; set; }
        public DbSet<ChecklistDetailTaasFile> ChecklistDetailTaasFiles { get; set; }
        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            base.OnModelCreating( modelBuilder );
            modelBuilder.ApplyConfigurationsFromAssembly( Assembly.GetExecutingAssembly() );

        }
    }
}
