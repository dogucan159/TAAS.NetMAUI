using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TAAS.NetMAUI.Business;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Business.Services;
using TAAS.NetMAUI.Infrastructure;
using TAAS.NetMAUI.Infrastructure.Data;
using TAAS.NetMAUI.Infrastructure.Interfaces;
using TAAS.NetMAUI.Infrastructure.Repositories;
using TAAS.NetMAUI.Presentation.Utilities.Dialog;
using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts( fonts => {
                    fonts.AddFont( "OpenSans-Regular.ttf", "OpenSansRegular" );
                    fonts.AddFont( "OpenSans-Semibold.ttf", "OpenSansSemibold" );
                } );

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddDbContext<TaasDbContext>( options => {
                var dbPath = Path.Combine( FileSystem.AppDataDirectory, "taas_offline.db" );
                options.UseSqlite( $"Data Source={dbPath}" );
            } );

            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper( typeof( MauiProgram ) );

            builder.Services.AddScoped<IAuditorRepository, AuditorRepository>();
            builder.Services.AddScoped<IAuditAssignmentRepository, AuditAssignmentRepository>();
            builder.Services.AddScoped<IStrategyPlanPeriodRepository, StrategyPlanPeriodRepository>();
            builder.Services.AddScoped<IAuditPeriodRepository, AuditPeriodRepository>();
            builder.Services.AddScoped<IMainTaskRepository, MainTaskRepository>();
            builder.Services.AddScoped<ITaskTypeRepository, TaskTypeRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IAuditTypeRepository, AuditTypeRepository>();
            builder.Services.AddScoped<IAuditAssignmentAuditorRepository, AuditAssignmentAuditorRepository>();
            builder.Services.AddScoped<IAuditAssignmentTemporaryAuditorRepository, AuditAssignmentTemporaryAuditorRepository>();
            builder.Services.AddScoped<IAuditAssignmentOperationAuditTypeRepository, AuditAssignmentOperationAuditTypeRepository>();
            builder.Services.AddScoped<IAuditAssignmentFinancialAuditTypeRepository, AuditAssignmentFinancialAuditTypeRepository>();
            builder.Services.AddScoped<IChecklistTemplateRepository, ChecklistTemplateRepository>();
            builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
            builder.Services.AddScoped<IKeyRequirementRepository, KeyRequirementRepository>();
            builder.Services.AddScoped<ISpecificFunctionRepository, SpecificFunctionRepository>();
            builder.Services.AddScoped<IAuditProgramRepository, AuditProgramRepository>();
            builder.Services.AddScoped<IChecklistRepository, ChecklistRepository>();
            builder.Services.AddScoped<IChecklistAuditorRepository, ChecklistAuditorRepository>();
            builder.Services.AddScoped<IChecklistDetailRepository, ChecklistDetailRepository>();
            builder.Services.AddScoped<IChecklistTemplateDetailRepository, ChecklistTemplateDetailRepository>();
            builder.Services.AddScoped<IChecklistHeaderRepository, ChecklistHeaderRepository>();
            builder.Services.AddScoped<IChecklistTemplateHeaderRepository, ChecklistTemplateHeaderRepository>();
            builder.Services.AddScoped<IChecklistTaasFileRepository, ChecklistTaasFileRepository>();
            builder.Services.AddScoped<IChecklistDetailTaasFileRepository, ChecklistDetailTaasFileRepository>();
            builder.Services.AddScoped<ITaasFileRepository, TaasFileRepository>();


            builder.Services.AddScoped<IApiService, ApiService>();
            builder.Services.AddScoped<IAuditorService, AuditorService>();
            builder.Services.AddScoped<IAuditAssignmentService, AuditAssignmentService>();
            builder.Services.AddScoped<IChecklistService, ChecklistService>();
            builder.Services.AddScoped<IChecklistHeaderService, ChecklistHeaderService>();
            builder.Services.AddScoped<IChecklistDetailService, ChecklistDetailService>();
            builder.Services.AddScoped<IChecklistTaasFileService, ChecklistTaasFileService>();
            builder.Services.AddScoped<IChecklistDetailTaasFileService, ChecklistDetailTaasFileService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();


            builder.Services.AddScoped<LoginViewModel>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<AuditAssignmentSelectionViewModel>();
            builder.Services.AddTransient<OperationAuditViewModel>();
            builder.Services.AddTransient<FinancialAuditViewModel>();
            builder.Services.AddTransient<ChecklistViewModel>();
            builder.Services.AddTransient<ChecklistSelectionViewModel>();
            builder.Services.AddTransient<ChecklistDetailViewModel>();
            builder.Services.AddTransient<QuestionDetailPage>();

            return builder.Build();
        }
    }
}
