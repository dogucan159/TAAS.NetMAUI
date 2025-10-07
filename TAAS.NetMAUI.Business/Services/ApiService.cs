using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Infrastructure;
using TAAS.NetMAUI.Infrastructure.Interfaces;
using TAAS.NetMAUI.Shared;

namespace TAAS.NetMAUI.Business.Services {

    public class RootObject {
        public List<AuditAssignmentDto> Content { get; set; }
    }
    public class ApiService : IApiService {

        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ApiService( IRepositoryManager manager, IMapper mapper ) {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<List<AuditAssignmentDto>?> PullAuditAssignmentsByMainTaskFromAPI( string code, string token ) {

            try {
                String key = SettingsKeyConst.API_ADDRESS;
                var setting = await _manager.Setting.GetOneSettingByKey( key, false );

                if ( setting == null )
                    throw new Exception( $"The setting with key : {key} could not found." );


                //https://dev-taas.hmb.gov.tr/backend/
                HttpClient client = new() {
                    BaseAddress = new Uri( setting.Value )
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", token );

                var payload = new {
                    ilike = new { mainTaskCode = code },
                    page = 0,
                    size = 20
                };

                var json = System.Text.Json.JsonSerializer.Serialize( payload );
                var content = new StringContent( json, Encoding.UTF8, "application/json" );
                List<AuditAssignmentDto>? result = null;
                try {
                    var response = await client.PostAsync( "audit-assignment/queryInfo", content );
                    response.EnsureSuccessStatusCode();
                    var auditAssignmentsJsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<RootObject>( auditAssignmentsJsonString )?.Content;
                }
                catch ( HttpRequestException ex ) {
                    throw new Exception( ex.Message );
                }

                return result;
            }
            catch ( Exception ex ) {
                throw new Exception( ex.Message );
            }


        }

        public async Task<List<AuditAssignmentDto>?> PullAuditAssignmentsByTaskTypeFromAPI( string code, string token ) {

            try {
                String key = SettingsKeyConst.API_ADDRESS;
                var setting = await _manager.Setting.GetOneSettingByKey( key, false );

                if ( setting == null )
                    throw new Exception( $"The setting with key : {key} could not found." );


                //https://dev-taas.hmb.gov.tr/backend/
                HttpClient client = new() {
                    BaseAddress = new Uri( setting.Value )
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", token );

                var payload = new {
                    ilike = new { taskTypeCode = code },
                    page = 0,
                    size = 20
                };

                var json = System.Text.Json.JsonSerializer.Serialize( payload );
                var content = new StringContent( json, Encoding.UTF8, "application/json" );
                List<AuditAssignmentDto>? result = null;
                try {
                    var response = await client.PostAsync( "audit-assignment/queryInfo", content );
                    response.EnsureSuccessStatusCode();
                    var auditAssignmentsJsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<RootObject>( auditAssignmentsJsonString )?.Content;
                }
                catch ( HttpRequestException ex ) {
                    throw new Exception( ex.Message );
                }

                return result;
            }
            catch ( Exception ex ) {
                throw new Exception( ex.Message );
            }


        }

        public async Task<List<AuditAssignmentDto>?> PullAuditAssignmentsByTaskFromAPI( string code, string token ) {

            try {
                String key = SettingsKeyConst.API_ADDRESS;
                var setting = await _manager.Setting.GetOneSettingByKey( key, false );

                if ( setting == null )
                    throw new Exception( $"The setting with key : {key} could not found." );


                //https://dev-taas.hmb.gov.tr/backend/
                HttpClient client = new() {
                    BaseAddress = new Uri( setting.Value )
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", token );

                var payload = new {
                    ilike = new { taskCode = code },
                    page = 0,
                    size = 20
                };

                var json = System.Text.Json.JsonSerializer.Serialize( payload );
                var content = new StringContent( json, Encoding.UTF8, "application/json" );
                List<AuditAssignmentDto>? result = null;
                try {
                    var response = await client.PostAsync( "audit-assignment/queryInfo", content );
                    response.EnsureSuccessStatusCode();
                    var auditAssignmentsJsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<RootObject>( auditAssignmentsJsonString )?.Content;
                }
                catch ( HttpRequestException ex ) {
                    throw new Exception( ex.Message );
                }

                return result;
            }
            catch ( Exception ex ) {
                throw new Exception( ex.Message );
            }


        }

        public async Task<List<ChecklistDto>?> PullChecklistsByAuditAssignmentIdAndAuditTypeIdFromAPI( long auditAssignmentId, long auditTypeId, string token ) {
            String key = SettingsKeyConst.API_ADDRESS;
            var setting = await _manager.Setting.GetOneSettingByKey( key, false );

            if ( setting == null )
                throw new Exception( $"The setting with key : {key} could not found." );


            HttpClient client = new() {
                BaseAddress = new Uri( setting.Value )
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", token );


            var payload = new {
                auditAssignmentId,
                auditTypeId
            };

            var json = System.Text.Json.JsonSerializer.Serialize( payload );
            var content = new StringContent( json, Encoding.UTF8, "application/json" );
            List<ChecklistDto>? result = null;
            try {
                var response = await client.PostAsync( "checklist/getByAuditAssignmentIdAndAuditTypeId", content );
                response.EnsureSuccessStatusCode();
                var checklistsJsonString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ChecklistDto>>( checklistsJsonString );
            }
            catch ( HttpRequestException ex ) {

                throw new Exception( ex.Message );
            }

            return result;
        }

        public async Task<ChecklistDto?> PullChecklistFromAPI( long id, string token ) {

            try {
                String key = SettingsKeyConst.API_ADDRESS;
                var setting = await _manager.Setting.GetOneSettingByKey( key, false );

                if ( setting == null )
                    throw new Exception( $"The setting with key : {key} could not found." );


                //https://dev-taas.hmb.gov.tr/backend/
                HttpClient client = new() {
                    BaseAddress = new Uri( setting.Value )
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", token );
                ChecklistDto? result = null;
                try {
                    var response = await client.GetAsync( $"checklist?id={id}" );
                    response.EnsureSuccessStatusCode();
                    var checklistJsonString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ChecklistDto>( checklistJsonString );
                }
                catch ( HttpRequestException ex ) {
                    throw new Exception( ex.Message );
                }

                return result;
            }
            catch ( Exception ex ) {
                throw new Exception( ex.Message );
            }


        }

        public async System.Threading.Tasks.Task SyncAuditAssignmentAsync( AuditAssignmentDto auditAssignmentDto ) {
            var auditAssignment = new AuditAssignment() {
                Id = auditAssignmentDto.Id,
                AuditAssignmentAuditors = new List<AuditAssignmentAuditor>(),
                AuditAssignmentTemporaryAuditors = new List<AuditAssignmentTemporaryAuditor>(),
                AuditAssignmentOperationAuditTypes = new List<AuditAssignmentOperationAuditType>(),
                AuditAssignmentFinancialAuditTypes = new List<AuditAssignmentFinancialAuditType>(),
            };

            //Auditor
            var dbCoordinatorAuditor = await _manager.Auditor.GetOneAuditorById( auditAssignmentDto.CoordinatorAuditorId, true );
            if ( dbCoordinatorAuditor != null )
                auditAssignment.CoordinatorAuditor = dbCoordinatorAuditor;
            else {
                var auditor = _mapper.Map<Auditor>( auditAssignmentDto.CoordinatorAuditor );
                auditor.Password = PasswordHelper.Hash( "FC_2025.Taas" );
                auditAssignment.CoordinatorAuditor = auditor;
            }

            //MainTask
            var dbMainTask = await _manager.MainTask.GetOneMainTaskById( auditAssignmentDto.MainTaskId, true );
            if ( dbMainTask != null )
                auditAssignment.MainTask = dbMainTask;
            else {
                var mainTask = _mapper.Map<MainTask>( auditAssignmentDto.MainTask );
                auditAssignment.MainTask = mainTask;
            }

            //TaskType
            var dbTaskType = await _manager.TaskType.GetOneTaskTypeById( auditAssignmentDto.TaskTypeId, true );
            if ( dbTaskType != null )
                auditAssignment.TaskType = dbTaskType;
            else {
                var taskType = _mapper.Map<TaskType>( auditAssignmentDto.TaskType );
                auditAssignment.TaskType = taskType;
                var dbAuditType = await _manager.AuditType.GetOneAuditTypeById( auditAssignmentDto.TaskType.SystemAuditTypeId, true );
                if ( dbAuditType != null )
                    auditAssignment.TaskType.SystemAuditType = dbAuditType;
                else {
                    var auditType = _mapper.Map<AuditType>( auditAssignmentDto.TaskType.SystemAuditType );
                    auditAssignment.TaskType.SystemAuditType = auditType;
                }
            }

            //Task
            var dbTask = await _manager.Task.GetOneTaskById( auditAssignmentDto.TaskId, true );
            if ( dbTask != null )
                auditAssignment.Task = dbTask;
            else {
                var task = _mapper.Map<Core.Entities.Task>( auditAssignmentDto.Task );
                auditAssignment.Task = task;
            }

            //StrategyPlanPeriod
            if ( auditAssignmentDto.StrategyPlanPeriodId.HasValue ) {
                var dbStrategyPlanPeriod = await _manager.StrategyPlanPeriod.GetOneStrategyPlanPeriodById( auditAssignmentDto.StrategyPlanPeriodId.Value, true );
                if ( dbStrategyPlanPeriod != null )
                    auditAssignment.StrategyPlanPeriod = dbStrategyPlanPeriod;
                else {
                    var strategyPlanPeriod = _mapper.Map<StrategyPlanPeriod>( auditAssignmentDto.StrategyPlanPeriod );
                    auditAssignment.StrategyPlanPeriod = strategyPlanPeriod;
                }
            }

            //AuditPeriod
            if ( auditAssignmentDto.AuditPeriodId.HasValue ) {
                var dbAuditPeriod = await _manager.AuditPeriod.GetOneAuditPeriodById( auditAssignmentDto.AuditPeriodId.Value, true );
                if ( dbAuditPeriod != null )
                    auditAssignment.AuditPeriod = dbAuditPeriod;
                else {
                    var auditPeriod = _mapper.Map<AuditPeriod>( auditAssignmentDto.AuditPeriod );
                    auditAssignment.AuditPeriod = auditPeriod;
                }
            }

            //AuditAssignmentAuditors
            if ( auditAssignmentDto.AuditAssignmentAuditors.Any() ) {
                foreach ( var auditAssignmentAuditor in auditAssignmentDto.AuditAssignmentAuditors ) {
                    var dbAuditAssignmentAuditor = await _manager.AuditAssignmentAuditor.GetOneAuditAssignmentAuditorByAuditAssignmentIdAndAuditorId( auditAssignment.Id, auditAssignmentAuditor.AuditorId, true );
                    if ( dbAuditAssignmentAuditor != null )
                        auditAssignment.AuditAssignmentAuditors.Add( dbAuditAssignmentAuditor );
                    else {
                        AuditAssignmentAuditor newAuditAssignmentAuditor = new AuditAssignmentAuditor() { AuditAssignment = auditAssignment };
                        var dbAuditor = await _manager.Auditor.GetOneAuditorById( auditAssignmentAuditor.AuditorId, true );
                        if ( dbAuditor != null )
                            newAuditAssignmentAuditor.Auditor = dbAuditor;
                        else {
                            var auditor = _mapper.Map<Auditor>( auditAssignmentAuditor.Auditor );
                            auditor.Password = PasswordHelper.Hash( "FC_2025.Taas" );
                            newAuditAssignmentAuditor.Auditor = auditor;
                        }
                        auditAssignment.AuditAssignmentAuditors.Add( newAuditAssignmentAuditor );
                    }
                }
            }
            //AuditAssignmentTemporaryAuditors
            if ( auditAssignmentDto.AuditAssignmentTemporaryAuditors.Any() ) {
                foreach ( var auditAssignmentTemporaryAuditor in auditAssignmentDto.AuditAssignmentTemporaryAuditors ) {
                    var dbAuditAssignmentTemporaryAuditor = await _manager.AuditAssignmentTemporaryAuditor.GetOneAuditAssignmentTemporaryAuditorByAuditAssignmentIdAndAuditorId( auditAssignment.Id, auditAssignmentTemporaryAuditor.AuditorId, true );
                    if ( dbAuditAssignmentTemporaryAuditor != null )
                        auditAssignment.AuditAssignmentTemporaryAuditors.Add( dbAuditAssignmentTemporaryAuditor );
                    else {
                        AuditAssignmentTemporaryAuditor newAuditAssignmentTemporaryAuditor = new AuditAssignmentTemporaryAuditor() { AuditAssignment = auditAssignment };
                        var dbAuditor = await _manager.Auditor.GetOneAuditorById( auditAssignmentTemporaryAuditor.AuditorId, true );
                        if ( dbAuditor != null )
                            newAuditAssignmentTemporaryAuditor.Auditor = dbAuditor;
                        else {
                            var auditor = _mapper.Map<Auditor>( auditAssignmentTemporaryAuditor.Auditor );
                            auditor.Password = PasswordHelper.Hash( "FC_2025.Taas" );
                            newAuditAssignmentTemporaryAuditor.Auditor = auditor;
                        }
                        auditAssignment.AuditAssignmentTemporaryAuditors.Add( newAuditAssignmentTemporaryAuditor );
                    }
                }
            }

            //AuditAssignmentOperationAuditTypes
            if ( auditAssignmentDto.AuditAssignmentOperationAuditTypes.Any() ) {
                foreach ( var auditAssignmentOperationAuditType in auditAssignmentDto.AuditAssignmentOperationAuditTypes ) {
                    var dbAuditAssignmentOperationAuditType = await _manager.AuditAssignmentOperationAuditType.GetOneAuditAssignmentOperationAuditTypeByAuditAssignmentIdAndAuditTypeId( auditAssignment.Id, auditAssignmentOperationAuditType.AuditTypeId, true );
                    if ( dbAuditAssignmentOperationAuditType != null )
                        auditAssignment.AuditAssignmentOperationAuditTypes.Add( dbAuditAssignmentOperationAuditType );
                    else {
                        AuditAssignmentOperationAuditType newAuditAssignmentOperationAuditType = new AuditAssignmentOperationAuditType() { AuditAssignment = auditAssignment };
                        var dbAuditType = await _manager.AuditType.GetOneAuditTypeById( auditAssignmentOperationAuditType.AuditTypeId, true );
                        if ( dbAuditType != null )
                            newAuditAssignmentOperationAuditType.AuditType = dbAuditType;
                        else {
                            var auditType = _mapper.Map<AuditType>( auditAssignmentOperationAuditType.AuditType );
                            newAuditAssignmentOperationAuditType.AuditType = auditType;
                        }
                        auditAssignment.AuditAssignmentOperationAuditTypes.Add( newAuditAssignmentOperationAuditType );
                    }
                }
            }
            //AuditAssignmentFinancialAuditTypes
            if ( auditAssignmentDto.AuditAssignmentFinancialAuditTypes.Any() ) {
                foreach ( var auditAssignmentFinancialAuditType in auditAssignmentDto.AuditAssignmentFinancialAuditTypes ) {
                    var dbAuditAssignmentFinancialAuditType = await _manager.AuditAssignmentFinancialAuditType.GetOneAuditAssignmentFinancialAuditTypeByAuditAssignmentIdAndAuditTypeId( auditAssignment.Id, auditAssignmentFinancialAuditType.AuditTypeId, true );
                    if ( dbAuditAssignmentFinancialAuditType != null )
                        auditAssignment.AuditAssignmentFinancialAuditTypes.Add( dbAuditAssignmentFinancialAuditType );
                    else {
                        AuditAssignmentFinancialAuditType newAuditAssignmentFinancialAuditType = new AuditAssignmentFinancialAuditType() { AuditAssignment = auditAssignment };
                        var dbAuditType = await _manager.AuditType.GetOneAuditTypeById( auditAssignmentFinancialAuditType.AuditTypeId, true );
                        if ( dbAuditType != null )
                            newAuditAssignmentFinancialAuditType.AuditType = dbAuditType;
                        else {
                            var auditType = _mapper.Map<AuditType>( auditAssignmentFinancialAuditType.AuditType );
                            newAuditAssignmentFinancialAuditType.AuditType = auditType;
                        }
                        auditAssignment.AuditAssignmentFinancialAuditTypes.Add( newAuditAssignmentFinancialAuditType );
                    }
                }
            }

            //AuditAssignment
            _manager.AuditAssignment.CreateOneAuditAssignment( auditAssignment );
            await _manager.SaveAsync();
        }

        public async System.Threading.Tasks.Task SyncChecklistAsync( ChecklistDto checklistDto, string token ) {
            try {


                String key = SettingsKeyConst.API_ADDRESS;
                var setting = await _manager.Setting.GetOneSettingByKey( key, false );

                if ( setting == null )
                    throw new Exception( $"The setting with key : {key} could not found." );

                HttpClient client = new() {
                    BaseAddress = new Uri( setting.Value )
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", token );

                Checklist checklist = new Checklist() {
                    Id = checklistDto.Id,
                    ChecklistAuditors = new List<ChecklistAuditor>(),
                    ChecklistTaasFiles = new List<ChecklistTaasFile>(),
                };
                //AuditAssignment
                var dbAuditAssignment = await _manager.AuditAssignment.GetOneAuditAssignmentById( checklistDto.AuditAssignmentId, true );
                checklist.AuditAssignment = dbAuditAssignment;
                //AuditType
                var dbAuditType = await _manager.AuditType.GetOneAuditTypeById( checklistDto.AuditTypeId, true );
                checklist.AuditType = dbAuditType;

                //AuditProgram
                if ( checklistDto.AuditProgramId.HasValue ) {
                    var dbAuditProgram = await _manager.AuditProgram.GetOneAuditProgramById( checklistDto.AuditProgramId.Value, true );

                    if ( dbAuditProgram != null )
                        checklist.AuditProgram = dbAuditProgram;
                    else {
                        AuditProgram auditProgram = _mapper.Map<AuditProgram>( checklistDto.AuditProgram );
                        //InstitutionUnit (Bodies)
                        var dbInstitution = await _manager.Institution.GetOneInstitutionById( checklistDto.AuditProgram.InstitutionId, true );
                        auditProgram.Institution = dbInstitution ?? _mapper.Map<Institution>( checklistDto.AuditProgram.Institution );
                        //KeyRequirement
                        if ( checklistDto.AuditProgram.KeyRequirementId.HasValue ) {
                            var dbKeyRequirement = await _manager.KeyRequirement.GetOneKeyRequirementById( checklistDto.AuditProgram.KeyRequirementId.Value, true );
                            auditProgram.KeyRequirement = dbKeyRequirement ?? _mapper.Map<KeyRequirement>( checklistDto.AuditProgram.KeyRequirement );
                        }
                        //SpecificFunction
                        if ( checklistDto.AuditProgram.SpecificFunctionId.HasValue ) {
                            var dbSpecificFunction = await _manager.SpecificFunction.GetOneSpecificFunctionById( checklistDto.AuditProgram.SpecificFunctionId.Value, true );
                            auditProgram.SpecificFunction = dbSpecificFunction ?? _mapper.Map<SpecificFunction>( checklistDto.AuditProgram.SpecificFunction );
                        }
                        checklist.AuditProgram = auditProgram;
                    }
                } // end AuditProgram

                //ChecklistTemplate
                var dbChecklistTemplate = await _manager.ChecklistTemplate.GetOneChecklistTemplateById( checklistDto.ChecklistTemplateId, true );
                checklist.ChecklistTemplate = dbChecklistTemplate ?? _mapper.Map<ChecklistTemplate>( checklistDto.ChecklistTemplate );

                //ReviewedAuditor
                if ( checklistDto.ReviewedAuditorId.HasValue ) {
                    var dbReviewedAuditor = await _manager.Auditor.GetOneAuditorById( checklistDto.ReviewedAuditorId.Value, true );
                    if ( dbReviewedAuditor != null )
                        checklist.ReviewedAuditor = dbReviewedAuditor;
                    else {
                        var auditor = _mapper.Map<Auditor>( checklistDto.ReviewedAuditor );
                        auditor.Password = PasswordHelper.Hash( "FC_2025.Taas" );
                        checklist.ReviewedAuditor = auditor;
                    }
                } //end reviewed auditor

                //ChecklistTaasFiles
                if ( checklistDto.TaasFiles.Any() ) {
                    foreach ( var taasFileDto in checklistDto.TaasFiles ) {

                        ChecklistTaasFile newChecklistTaasFile = new ChecklistTaasFile() {
                            Checklist = checklist,
                            Synched = true,
                        };
                        var dbTaasFile = await _manager.TaasFile.GetOneTaasFileByApiId( taasFileDto.Id, true );
                        if ( dbTaasFile != null )
                            newChecklistTaasFile.TaasFile = dbTaasFile;
                        else {
                            //taas-file/getWithFileData
                            try {
                                var response = await client.GetAsync( $"taas-file/getWithFileData?id={taasFileDto.Id}" );
                                response.EnsureSuccessStatusCode();
                                var taasFileJsonString = await response.Content.ReadAsStringAsync();
                                TaasFileDto? taasFileResult = JsonConvert.DeserializeObject<TaasFileDto>( taasFileJsonString );

                                newChecklistTaasFile.TaasFile = _mapper.Map<TaasFile>( taasFileResult );
                                newChecklistTaasFile.TaasFile.ApiId = taasFileResult.Id;
                                newChecklistTaasFile.TaasFile.Synched = true;
                            }
                            catch ( HttpRequestException ex ) {
                                //throw new Exception( ex.Message );
                            }
                        }

                        checklist.ChecklistTaasFiles.Add( newChecklistTaasFile );
                    }
                } // end if checklist taas files

                //ChecklistAuditors
                if ( checklistDto.ChecklistAuditors.Any() ) {
                    foreach ( var checklistAuditor in checklistDto.ChecklistAuditors ) {
                        var dbChecklistAuditor = await _manager.ChecklistAuditor.GetOneChecklistAuditorByChecklistIdAndAuditorId( checklistDto.Id, checklistAuditor.AuditorId, true );
                        if ( dbChecklistAuditor != null )
                            checklist.ChecklistAuditors.Add( dbChecklistAuditor );
                        else {
                            ChecklistAuditor newChecklistAuditor = new ChecklistAuditor() { Checklist = checklist };
                            var dbAuditor = await _manager.Auditor.GetOneAuditorById( checklistAuditor.AuditorId, true );
                            if ( dbAuditor != null )
                                newChecklistAuditor.Auditor = dbAuditor;
                            else {
                                var auditor = _mapper.Map<Auditor>( checklistAuditor.Auditor );
                                auditor.Password = PasswordHelper.Hash( "FC_2025.Taas" );
                                newChecklistAuditor.Auditor = auditor;
                            }
                            checklist.ChecklistAuditors.Add( newChecklistAuditor );
                        }
                    }
                }
                //ChecklistDetails
                checklist.ChecklistDetails = _mapper.Map<List<ChecklistDetail>>( checklistDto.ChecklistDetails );
                if ( checklist.ChecklistDetails.Any() ) {
                    foreach ( var checklistDetail in checklist.ChecklistDetails ) {
                        checklistDetail.ChecklistDetailTaasFiles = new List<ChecklistDetailTaasFile>();
                        var checklistDetailDto = checklistDto.ChecklistDetails.First( i => i.Id == checklistDetail.Id );
                        if ( checklistDetailDto.TaasFiles.Any() ) {
                            foreach ( var taasFileDto in checklistDetailDto.TaasFiles )
                                checklistDetail.ChecklistDetailTaasFiles.Add( new ChecklistDetailTaasFile() {
                                    ChecklistDetail = checklistDetail,
                                    TaasFile = checklist.ChecklistTaasFiles.First( i => i.TaasFile.ApiId == taasFileDto.Id ).TaasFile,
                                    Synched = true,
                                } );
                        }
                    }
                } // end if checklist detail

                //ChecklistHeaders
                checklist.ChecklistHeaders = _mapper.Map<List<ChecklistHeader>>( checklistDto.ChecklistHeaders );


                //Checklist
                _manager.Checklist.CreateOneChecklist( checklist );
                await _manager.SaveAsync();

                //CODE HERE: API'de checklist offline ozelligini true yapan metod cagrilacak.
                var checklistPayload = new {
                    offline = true
                };
                var checklistJson = System.Text.Json.JsonSerializer.Serialize( checklistPayload );
                var checklistContent = new StringContent( checklistJson, Encoding.UTF8, "application/json" );
                try {
                    var response = await client.PutAsync( $"checklist/{checklist.Id}/updateOffline", checklistContent );
                    response.EnsureSuccessStatusCode();
                }
                catch ( HttpRequestException ex ) {
                    throw new Exception( ex.Message );
                }
            }
            catch ( Exception ex ) {

                throw new Exception( ex.Message );
            }
        }

        public async System.Threading.Tasks.Task TransferChecklistsToLive( List<ChecklistDto> lstChecklistDto, String token ) {
            try {

                String key = SettingsKeyConst.API_ADDRESS;
                var setting = await _manager.Setting.GetOneSettingByKey( key, false );

                if ( setting == null )
                    throw new Exception( $"The setting with key : {key} could not found." );


                //https://dev-taas.hmb.gov.tr/backend/
                HttpClient client = new() {
                    BaseAddress = new Uri( setting.Value )
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", token );

                foreach ( var checklistDto in lstChecklistDto ) {

                    //ChecklistTaasFiles
                    //var checklistTaasFiles = _mapper.Map<List<ChecklistTaasFile>>( checklistDto.ChecklistTaasFiles );

                    if ( checklistDto.ChecklistTaasFiles?.Any() == true ) {
                        var createdChecklistTaasFiles = checklistDto.ChecklistTaasFiles.Where( i => !i.Deleted.HasValue || ( i.Deleted.HasValue && !i.Deleted.Value ) );

                        if ( createdChecklistTaasFiles.Any() ) {
                            foreach ( var createdChecklistTaasFile in createdChecklistTaasFiles ) {


                                var taasFilePayload = new {
                                    fileName = createdChecklistTaasFile.TaasFile.Name,
                                    fileData = createdChecklistTaasFile.TaasFile.FileData,
                                    directory = String.Empty
                                };
                                var taasFileJson = System.Text.Json.JsonSerializer.Serialize( taasFilePayload );
                                var taasFileContent = new StringContent( taasFileJson, Encoding.UTF8, "application/json" );
                                TaasFileDto? result = null;
                                try {
                                    var response = await client.PostAsync( "taas-file/createWithFileData", taasFileContent );
                                    response.EnsureSuccessStatusCode();
                                    var taasFileJsonString = await response.Content.ReadAsStringAsync();
                                    result = JsonConvert.DeserializeObject<TaasFileDto>( taasFileJsonString );

                                    //bu degerleri veritabaninda guncelle
                                    var dbTaasFile = await _manager.TaasFile.GetOneTaasFileById( createdChecklistTaasFile.TaasFileId, true );
                                    dbTaasFile.ApiId = result.Id;
                                    dbTaasFile.ObjectId = result.ObjectId;
                                    dbTaasFile.Synched = true;
                                    _manager.TaasFile.UpdateOneTaasFile( dbTaasFile );
                                    await _manager.SaveAsync();
                                    //Api'ye kaydedilecek degerler
                                    createdChecklistTaasFile.TaasFileId = result.Id;
                                    createdChecklistTaasFile.TaasFile.ObjectId = result.ObjectId;

                                }
                                catch ( HttpRequestException ex ) {
                                    throw new Exception( ex.Message );
                                }
                            } //end if
                            var checklistTaasFilePayload = new {
                                taasFiles = createdChecklistTaasFiles.Select( i => new { id = i.TaasFileId } ),
                                explanationFormatted = String.Empty

                            };
                            var checklistTaasFileJson = System.Text.Json.JsonSerializer.Serialize( checklistTaasFilePayload );
                            var checklistTaasFileContent = new StringContent( checklistTaasFileJson, Encoding.UTF8, "application/json" );
                            try {
                                var response = await client.PutAsync( $"checklist/update-taas-file/{checklistDto.Id}", checklistTaasFileContent );
                                response.EnsureSuccessStatusCode();

                                foreach ( var createdChecklistTaasFile in createdChecklistTaasFiles ) {
                                    var dbChecklistTaasFile = await _manager.ChecklistTaasFile.GetOneChecklistTaasFileById( createdChecklistTaasFile.Id, true );
                                    dbChecklistTaasFile.Synched = true;
                                    _manager.ChecklistTaasFile.UpdateOneChecklistTaasFile( dbChecklistTaasFile );
                                    await _manager.SaveAsync();
                                }

                            }
                            catch ( HttpRequestException ex ) {
                                throw new Exception( ex.Message );
                            }
                        }

                        var deletedChecklistTaasFiles = checklistDto.ChecklistTaasFiles
                            .Where( i => i.Synched.HasValue && i.Synched.Value && i.Deleted.HasValue && i.Deleted.Value );

                        if ( deletedChecklistTaasFiles.Any() ) {
                            foreach ( var deletedChecklistTaasFile in deletedChecklistTaasFiles ) {
                                var dbDeletedTaasFile = await _manager.TaasFile.GetOneTaasFileById( deletedChecklistTaasFile.TaasFileId, true );
                                var checklistTaasFilePayload = new {
                                    checklistId = checklistDto.Id,
                                    taasFileId = dbDeletedTaasFile.ApiId
                                };
                                var checklistTaasFileJson = System.Text.Json.JsonSerializer.Serialize( checklistTaasFilePayload );
                                var checklistTaasFileContent = new StringContent( checklistTaasFileJson, Encoding.UTF8, "application/json" );
                                try {
                                    var response = await client.PostAsync( $"checklist/deleteTaasFile", checklistTaasFileContent );
                                    response.EnsureSuccessStatusCode();
                                }
                                catch ( HttpRequestException ex ) {
                                    throw new Exception( ex.Message );
                                }
                            }
                        }
                    }

                    //ChecklistDetails
                    if ( checklistDto.ChecklistDetails?.Any() == true ) {
                        foreach ( var checklistDetail in checklistDto.ChecklistDetails ) {
                            var checklistDetailPayload = new {
                                checklistTemplateDetailId = checklistDetail.ChecklistTemplateDetailId,
                                checklistId = checklistDto.Id,
                                explanationFormatted = checklistDetail.ExplanationFormatted,
                                explanation = checklistDetail.Explanation,
                                answer = checklistDetail.Answer,
                                version = checklistDetail.Version,
                            };
                            var checklistDetailJson = System.Text.Json.JsonSerializer.Serialize( checklistDetailPayload );
                            var checklistDetailContent = new StringContent( checklistDetailJson, Encoding.UTF8, "application/json" );
                            try {
                                var response = await client.PutAsync( $"checklist-detail/{checklistDetail.Id}", checklistDetailContent );
                                response.EnsureSuccessStatusCode();
                            }
                            catch ( HttpRequestException ex ) {
                                throw new Exception( ex.Message );
                            }

                            //Files
                            if ( checklistDetail.ChecklistDetailTaasFiles?.Any() == true ) {
                                var createdChecklistDetailTaasFiles = checklistDetail.ChecklistDetailTaasFiles.Where( i => ( !i.Synched.HasValue || ( i.Synched.HasValue && !i.Synched.Value ) )
                            && ( !i.Deleted.HasValue || ( i.Deleted.HasValue && !i.Deleted.Value ) ) );

                                if ( createdChecklistDetailTaasFiles.Any() ) {
                                    List<long> lstTaasFileToCreate = new List<long>();
                                    var taasFiles = await System.Threading.Tasks.Task.WhenAll(
                                        createdChecklistDetailTaasFiles.Select( async file => {
                                            var dbFile = await _manager.TaasFile.GetOneTaasFileById( file.TaasFileId, true );
                                            return new { id = dbFile.ApiId };
                                        } )
                                    );

                                    var checklistDetailTaasFilePayload = new {
                                        taasFiles,
                                        explanationFormatted = checklistDetail.ExplanationFormatted
                                    };

                                    var checklistDetailTaasFileJson = System.Text.Json.JsonSerializer.Serialize( checklistDetailTaasFilePayload );
                                    var checklistDetailTaasFileContent = new StringContent( checklistDetailTaasFileJson, Encoding.UTF8, "application/json" );
                                    try {
                                        var response = await client.PutAsync( $"checklist-detail/update-detailed-taas-file/{checklistDetail.Id}", checklistDetailTaasFileContent );
                                        response.EnsureSuccessStatusCode();

                                        //Synched
                                        foreach ( var createdChecklistDetailTaasFile in createdChecklistDetailTaasFiles ) {
                                            var dbChecklistDetailTaasFile = await _manager.ChecklistDetailTaasFile.GetOneChecklistDetailTaasFileById( createdChecklistDetailTaasFile.Id, true );
                                            dbChecklistDetailTaasFile.Synched = true;
                                            _manager.ChecklistDetailTaasFile.UpdateOneChecklistDetailTaasFile( dbChecklistDetailTaasFile );
                                            await _manager.SaveAsync();
                                        }
                                    }
                                    catch ( HttpRequestException ex ) {
                                        throw new Exception( ex.Message );
                                    }
                                }

                                var deletedChecklistDetailTaasFiles = checklistDetail.ChecklistDetailTaasFiles
                                    .Where( i => i.Synched.HasValue && i.Synched.Value && i.Deleted.HasValue && i.Deleted.Value );

                                if ( deletedChecklistDetailTaasFiles.Any() ) {
                                    foreach ( var deletedChecklistDetailTaasFile in deletedChecklistDetailTaasFiles ) {
                                        var checklistDetailTaasFilePayload = new {
                                            checklistDetailId = checklistDetail.Id,
                                            taasFileId = deletedChecklistDetailTaasFile.TaasFileId
                                        };
                                        var checklistDetailTaasFileJson = System.Text.Json.JsonSerializer.Serialize( checklistDetailTaasFilePayload );
                                        var checklistDetailTaasFileContent = new StringContent( checklistDetailTaasFileJson, Encoding.UTF8, "application/json" );
                                        try {
                                            var response = await client.PostAsync( $"checklist-detail/deleteTaasFile", checklistDetailTaasFileContent );
                                            response.EnsureSuccessStatusCode();
                                        }
                                        catch ( HttpRequestException ex ) {
                                            throw new Exception( ex.Message );
                                        }
                                    }
                                }
                            }

                        }
                    }

                    //Headers
                    if ( checklistDto.ChecklistHeaders?.Any() == true ) {
                        foreach ( var checklistHeader in checklistDto.ChecklistHeaders ) {
                            var checklistHeaderPayload = new {
                                value = checklistHeader.Value,
                                checklistId = checklistHeader.ChecklistId,
                                checklistTemplateHeaderId = checklistHeader.ChecklistTemplateHeaderId,
                                version = checklistHeader.Version,
                            };
                            var checklistHeaderJson = System.Text.Json.JsonSerializer.Serialize( checklistHeaderPayload );
                            var checklistHeaderContent = new StringContent( checklistHeaderJson, Encoding.UTF8, "application/json" );
                            try {
                                var response = await client.PutAsync( $"checklist-header/{checklistHeader.Id}", checklistHeaderContent );
                                response.EnsureSuccessStatusCode();
                            }
                            catch ( HttpRequestException ex ) {
                                throw new Exception( ex.Message );
                            }
                        }
                    }

                    //Delete Checklist
                    var dbChecklist = await _manager.Checklist.GetOneChecklistById( checklistDto.Id, true );
                    _manager.Checklist.DeleteOneChecklist( dbChecklist );
                    _manager.ChecklistTemplate.DeleteOneChecklistTemplate( dbChecklist.ChecklistTemplate );
                    await _manager.SaveAsync();

                    //CODE HERE: API'de checklist offline ozelligini false yapan metod cagrilacak.
                    var checklistPayload = new {
                        offline = false
                    };
                    var checklistJson = System.Text.Json.JsonSerializer.Serialize( checklistPayload );
                    var checklistContent = new StringContent( checklistJson, Encoding.UTF8, "application/json" );
                    try {
                        var response = await client.PutAsync( $"checklist/{checklistDto.Id}/updateOffline", checklistContent );
                        response.EnsureSuccessStatusCode();
                    }
                    catch ( HttpRequestException ex ) {
                        throw new Exception( ex.Message );
                    }

                } //end foreach
            }
            catch ( Exception ex ) {

                throw new Exception( ex.Message );
            }
        }
    }
}
