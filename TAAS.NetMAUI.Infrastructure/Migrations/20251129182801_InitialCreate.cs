using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAAS.NetMAUI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    IdentificationNumber = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    MachineName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditPeriods",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "text", nullable: false),
                    RegisteredName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyRequirements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    GeneralCode = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyRequirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainTasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecificFunctions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    GeneralCode = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificFunctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StrategyPlanPeriods",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrategyPlanPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaasFile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    ObjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileData = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Synched = table.Column<bool>(type: "INTEGER", nullable: true),
                    ApiId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaasFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    SystemAuditTypeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTypes_AuditTypes_SystemAuditTypeId",
                        column: x => x.SystemAuditTypeId,
                        principalTable: "AuditTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistTemplateDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    CommentTr = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<bool>(type: "INTEGER", nullable: true),
                    Sequence = table.Column<int>(type: "INTEGER", nullable: false),
                    ChecklistTemplateId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistTemplateDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistTemplateDetails_ChecklistTemplates_ChecklistTemplateId",
                        column: x => x.ChecklistTemplateId,
                        principalTable: "ChecklistTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistTemplateHeaders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Sequence = table.Column<int>(type: "INTEGER", nullable: false),
                    ChecklistTemplateId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistTemplateHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistTemplateHeaders_ChecklistTemplates_ChecklistTemplateId",
                        column: x => x.ChecklistTemplateId,
                        principalTable: "ChecklistTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditPrograms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstitutionId = table.Column<long>(type: "INTEGER", nullable: false),
                    KeyRequirementId = table.Column<long>(type: "INTEGER", nullable: true),
                    SpecificFunctionId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditPrograms_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditPrograms_KeyRequirements_KeyRequirementId",
                        column: x => x.KeyRequirementId,
                        principalTable: "KeyRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditPrograms_SpecificFunctions_SpecificFunctionId",
                        column: x => x.SpecificFunctionId,
                        principalTable: "SpecificFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditAssignments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoordinatorAuditorId = table.Column<long>(type: "INTEGER", nullable: false),
                    StrategyPlanPeriodId = table.Column<long>(type: "INTEGER", nullable: true),
                    AuditPeriodId = table.Column<long>(type: "INTEGER", nullable: true),
                    MainTaskId = table.Column<long>(type: "INTEGER", nullable: false),
                    TaskTypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditAssignments_AuditPeriods_AuditPeriodId",
                        column: x => x.AuditPeriodId,
                        principalTable: "AuditPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditAssignments_Auditors_CoordinatorAuditorId",
                        column: x => x.CoordinatorAuditorId,
                        principalTable: "Auditors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditAssignments_MainTasks_MainTaskId",
                        column: x => x.MainTaskId,
                        principalTable: "MainTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditAssignments_StrategyPlanPeriods_StrategyPlanPeriodId",
                        column: x => x.StrategyPlanPeriodId,
                        principalTable: "StrategyPlanPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditAssignments_TaskTypes_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditAssignments_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditAssignmentAuditors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuditAssignmentId = table.Column<long>(type: "INTEGER", nullable: false),
                    AuditorId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditAssignmentAuditors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditAssignmentAuditors_AuditAssignments_AuditAssignmentId",
                        column: x => x.AuditAssignmentId,
                        principalTable: "AuditAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditAssignmentAuditors_Auditors_AuditorId",
                        column: x => x.AuditorId,
                        principalTable: "Auditors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditAssignmentFinancialAuditTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuditAssignmentId = table.Column<long>(type: "INTEGER", nullable: false),
                    AuditTypeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditAssignmentFinancialAuditTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditAssignmentFinancialAuditTypes_AuditAssignments_AuditAssignmentId",
                        column: x => x.AuditAssignmentId,
                        principalTable: "AuditAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditAssignmentFinancialAuditTypes_AuditTypes_AuditTypeId",
                        column: x => x.AuditTypeId,
                        principalTable: "AuditTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditAssignmentOperationAuditTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuditAssignmentId = table.Column<long>(type: "INTEGER", nullable: false),
                    AuditTypeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditAssignmentOperationAuditTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditAssignmentOperationAuditTypes_AuditAssignments_AuditAssignmentId",
                        column: x => x.AuditAssignmentId,
                        principalTable: "AuditAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditAssignmentOperationAuditTypes_AuditTypes_AuditTypeId",
                        column: x => x.AuditTypeId,
                        principalTable: "AuditTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditAssignmentTemporaryAuditors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuditAssignmentId = table.Column<long>(type: "INTEGER", nullable: false),
                    AuditorId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditAssignmentTemporaryAuditors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditAssignmentTemporaryAuditors_AuditAssignments_AuditAssignmentId",
                        column: x => x.AuditAssignmentId,
                        principalTable: "AuditAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditAssignmentTemporaryAuditors_Auditors_AuditorId",
                        column: x => x.AuditorId,
                        principalTable: "Auditors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuditAssignmentId = table.Column<long>(type: "INTEGER", nullable: false),
                    AuditTypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    AuditProgramId = table.Column<long>(type: "INTEGER", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    SamplingRowNumber = table.Column<long>(type: "INTEGER", nullable: true),
                    ChecklistTemplateId = table.Column<long>(type: "INTEGER", nullable: false),
                    Turkish = table.Column<bool>(type: "INTEGER", nullable: true),
                    ReviewedAuditorId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklists_AuditAssignments_AuditAssignmentId",
                        column: x => x.AuditAssignmentId,
                        principalTable: "AuditAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklists_AuditPrograms_AuditProgramId",
                        column: x => x.AuditProgramId,
                        principalTable: "AuditPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklists_AuditTypes_AuditTypeId",
                        column: x => x.AuditTypeId,
                        principalTable: "AuditTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklists_Auditors_ReviewedAuditorId",
                        column: x => x.ReviewedAuditorId,
                        principalTable: "Auditors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklists_ChecklistTemplates_ChecklistTemplateId",
                        column: x => x.ChecklistTemplateId,
                        principalTable: "ChecklistTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistAuditors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChecklistId = table.Column<long>(type: "INTEGER", nullable: false),
                    AuditorId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistAuditors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistAuditors_Auditors_AuditorId",
                        column: x => x.AuditorId,
                        principalTable: "Auditors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChecklistAuditors_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChecklistId = table.Column<long>(type: "INTEGER", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", nullable: true),
                    Explanation = table.Column<string>(type: "TEXT", nullable: true),
                    ExplanationFormatted = table.Column<string>(type: "TEXT", nullable: true),
                    ChecklistTemplateDetailId = table.Column<long>(type: "INTEGER", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistDetails_ChecklistTemplateDetails_ChecklistTemplateDetailId",
                        column: x => x.ChecklistTemplateDetailId,
                        principalTable: "ChecklistTemplateDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChecklistDetails_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistHeaders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChecklistId = table.Column<long>(type: "INTEGER", nullable: false),
                    ChecklistTemplateHeaderId = table.Column<long>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistHeaders_ChecklistTemplateHeaders_ChecklistTemplateHeaderId",
                        column: x => x.ChecklistTemplateHeaderId,
                        principalTable: "ChecklistTemplateHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChecklistHeaders_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistTaasFiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChecklistId = table.Column<long>(type: "INTEGER", nullable: false),
                    TaasFileId = table.Column<long>(type: "INTEGER", nullable: false),
                    Synched = table.Column<bool>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistTaasFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistTaasFiles_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistTaasFiles_TaasFile_TaasFileId",
                        column: x => x.TaasFileId,
                        principalTable: "TaasFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistDetailTaasFiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChecklistDetailId = table.Column<long>(type: "INTEGER", nullable: false),
                    TaasFileId = table.Column<long>(type: "INTEGER", nullable: false),
                    Synched = table.Column<bool>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistDetailTaasFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistDetailTaasFiles_ChecklistDetails_ChecklistDetailId",
                        column: x => x.ChecklistDetailId,
                        principalTable: "ChecklistDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistDetailTaasFiles_TaasFile_TaasFileId",
                        column: x => x.TaasFileId,
                        principalTable: "TaasFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignmentAuditors_AuditAssignmentId",
                table: "AuditAssignmentAuditors",
                column: "AuditAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignmentAuditors_AuditorId",
                table: "AuditAssignmentAuditors",
                column: "AuditorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignmentFinancialAuditTypes_AuditAssignmentId",
                table: "AuditAssignmentFinancialAuditTypes",
                column: "AuditAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignmentFinancialAuditTypes_AuditTypeId",
                table: "AuditAssignmentFinancialAuditTypes",
                column: "AuditTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignmentOperationAuditTypes_AuditAssignmentId",
                table: "AuditAssignmentOperationAuditTypes",
                column: "AuditAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignmentOperationAuditTypes_AuditTypeId",
                table: "AuditAssignmentOperationAuditTypes",
                column: "AuditTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignments_AuditPeriodId",
                table: "AuditAssignments",
                column: "AuditPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignments_CoordinatorAuditorId",
                table: "AuditAssignments",
                column: "CoordinatorAuditorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignments_MainTaskId",
                table: "AuditAssignments",
                column: "MainTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignments_StrategyPlanPeriodId",
                table: "AuditAssignments",
                column: "StrategyPlanPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignments_TaskId",
                table: "AuditAssignments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignments_TaskTypeId",
                table: "AuditAssignments",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignmentTemporaryAuditors_AuditAssignmentId",
                table: "AuditAssignmentTemporaryAuditors",
                column: "AuditAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditAssignmentTemporaryAuditors_AuditorId",
                table: "AuditAssignmentTemporaryAuditors",
                column: "AuditorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditPrograms_InstitutionId",
                table: "AuditPrograms",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditPrograms_KeyRequirementId",
                table: "AuditPrograms",
                column: "KeyRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditPrograms_SpecificFunctionId",
                table: "AuditPrograms",
                column: "SpecificFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistAuditors_AuditorId",
                table: "ChecklistAuditors",
                column: "AuditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistAuditors_ChecklistId",
                table: "ChecklistAuditors",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistDetails_ChecklistId",
                table: "ChecklistDetails",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistDetails_ChecklistTemplateDetailId",
                table: "ChecklistDetails",
                column: "ChecklistTemplateDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistDetailTaasFiles_ChecklistDetailId",
                table: "ChecklistDetailTaasFiles",
                column: "ChecklistDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistDetailTaasFiles_TaasFileId",
                table: "ChecklistDetailTaasFiles",
                column: "TaasFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistHeaders_ChecklistId",
                table: "ChecklistHeaders",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistHeaders_ChecklistTemplateHeaderId",
                table: "ChecklistHeaders",
                column: "ChecklistTemplateHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_AuditAssignmentId",
                table: "Checklists",
                column: "AuditAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_AuditProgramId",
                table: "Checklists",
                column: "AuditProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_AuditTypeId",
                table: "Checklists",
                column: "AuditTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_ChecklistTemplateId",
                table: "Checklists",
                column: "ChecklistTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_ReviewedAuditorId",
                table: "Checklists",
                column: "ReviewedAuditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTaasFiles_ChecklistId",
                table: "ChecklistTaasFiles",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTaasFiles_TaasFileId",
                table: "ChecklistTaasFiles",
                column: "TaasFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTemplateDetails_ChecklistTemplateId",
                table: "ChecklistTemplateDetails",
                column: "ChecklistTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTemplateHeaders_ChecklistTemplateId",
                table: "ChecklistTemplateHeaders",
                column: "ChecklistTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTypes_SystemAuditTypeId",
                table: "TaskTypes",
                column: "SystemAuditTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditAssignmentAuditors");

            migrationBuilder.DropTable(
                name: "AuditAssignmentFinancialAuditTypes");

            migrationBuilder.DropTable(
                name: "AuditAssignmentOperationAuditTypes");

            migrationBuilder.DropTable(
                name: "AuditAssignmentTemporaryAuditors");

            migrationBuilder.DropTable(
                name: "ChecklistAuditors");

            migrationBuilder.DropTable(
                name: "ChecklistDetailTaasFiles");

            migrationBuilder.DropTable(
                name: "ChecklistHeaders");

            migrationBuilder.DropTable(
                name: "ChecklistTaasFiles");

            migrationBuilder.DropTable(
                name: "ChecklistDetails");

            migrationBuilder.DropTable(
                name: "ChecklistTemplateHeaders");

            migrationBuilder.DropTable(
                name: "TaasFile");

            migrationBuilder.DropTable(
                name: "ChecklistTemplateDetails");

            migrationBuilder.DropTable(
                name: "Checklists");

            migrationBuilder.DropTable(
                name: "AuditAssignments");

            migrationBuilder.DropTable(
                name: "AuditPrograms");

            migrationBuilder.DropTable(
                name: "ChecklistTemplates");

            migrationBuilder.DropTable(
                name: "AuditPeriods");

            migrationBuilder.DropTable(
                name: "Auditors");

            migrationBuilder.DropTable(
                name: "MainTasks");

            migrationBuilder.DropTable(
                name: "StrategyPlanPeriods");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "KeyRequirements");

            migrationBuilder.DropTable(
                name: "SpecificFunctions");

            migrationBuilder.DropTable(
                name: "AuditTypes");
        }
    }
}
