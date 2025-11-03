using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Presentation {
    public class MappingProfile : Profile {

        public MappingProfile() {
            CreateMap<AuditorDto, Auditor>().ReverseMap();
            CreateMap<AuditAssignmentDto, AuditAssignment>().ReverseMap();
            CreateMap<StrategyPlanPeriodDto, StrategyPlanPeriod>().ReverseMap();
            CreateMap<AuditPeriodDto, AuditPeriod>().ReverseMap();
            CreateMap<MainTaskDto, MainTask>().ReverseMap();
            CreateMap<TaskTypeDto, TaskType>().ReverseMap();
            CreateMap<TaskDto, Core.Entities.Task>().ReverseMap();
            CreateMap<AuditTypeDto, AuditType>().ReverseMap();
            CreateMap<AuditAssignmentAuditorDto, AuditAssignmentAuditor>().ReverseMap();
            CreateMap<AuditAssignmentTemporaryAuditorDto, AuditAssignmentTemporaryAuditor>().ReverseMap();
            CreateMap<AuditAssignmentOperationAuditTypeDto, AuditAssignmentOperationAuditType>().ReverseMap();
            CreateMap<AuditAssignmentFinancialAuditTypeDto, AuditAssignmentFinancialAuditType>().ReverseMap();
            CreateMap<ChecklistTemplateDto, ChecklistTemplate>().ReverseMap();
            CreateMap<InstitutionDto, Institution>().ReverseMap();
            CreateMap<KeyRequirementDto, KeyRequirement>().ReverseMap();
            CreateMap<SpecificFunctionDto, SpecificFunction>().ReverseMap();
            CreateMap<AuditProgramDto, AuditProgram>().ReverseMap();
            CreateMap<ChecklistDto, Checklist>().ReverseMap();
            CreateMap<ChecklistAuditorDto, ChecklistAuditor>().ReverseMap();
            CreateMap<ChecklistDetailDto, ChecklistDetail>().ReverseMap();
            CreateMap<ChecklistDetailAnswerUpdateDto, ChecklistDetail>();
            CreateMap<ChecklistDetailExplanationUpdateDto, ChecklistDetail>();
            CreateMap<ChecklistDetailExplanationFormattedUpdateDto, ChecklistDetail>();
            CreateMap<ChecklistTemplateDetailDto, ChecklistTemplateDetail>().ReverseMap();
            CreateMap<ChecklistHeaderDto, ChecklistHeader>().ReverseMap();
            CreateMap<ChecklistHeaderUpdateDto, ChecklistHeader>();
            CreateMap<ChecklistTemplateHeaderDto, ChecklistTemplateHeader>().ReverseMap();
            CreateMap<TaasFileDto, TaasFile>().ReverseMap();
            CreateMap<TaasFileCreateDto, TaasFile>();
            CreateMap<ChecklistTaasFileDto, ChecklistTaasFile>().ReverseMap();
            CreateMap<ChecklistTaasFileCreateDto, ChecklistTaasFile>();
            CreateMap<ChecklistTaasFileDeleteDto, ChecklistTaasFile>();

            CreateMap<ChecklistDetailTaasFileDto, ChecklistDetailTaasFile>().ReverseMap();
            CreateMap<ChecklistDetailTaasFileCreateDto, ChecklistDetailTaasFile>();
            CreateMap<ChecklistDetailTaasFileDeleteDto, ChecklistDetailTaasFile>();
        }

    }
}
