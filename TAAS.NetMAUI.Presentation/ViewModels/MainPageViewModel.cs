using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Business;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Presentation.Data;

namespace TAAS.NetMAUI.Presentation.ViewModels {
    public partial class MainPageViewModel : ObservableObject {
        private readonly IServiceManager _manager;

        [ObservableProperty]
        private ObservableCollection<AuditAssignmentDto> auditAssignments = new ObservableCollection<AuditAssignmentDto>();

        [ObservableProperty]
        private String welcomeMessage = "Welcome Guest";

        public MainPageViewModel( IServiceManager manager ) {
            _manager = manager;
        }

        public async System.Threading.Tasks.Task LoadAuditAssignmentsAsync() {
            var result = await _manager.AuditAssignmentService.GetAllAuditAssignments( false );
            AuditAssignments = new ObservableCollection<AuditAssignmentDto>( result );

            var sessionUserId = Preferences.Get( "SessionUserId", -1L );
            var sessionUser = await _manager.AuditorService.GetById( sessionUserId, false );
            WelcomeMessage = $"Welcome Dear {sessionUser.FirstName} {sessionUser.LastName}";
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task ShowSystemAuditChecklistsAsync( AuditAssignmentDto auditAssignment ) {
            NavigationContext.CurrentAuditAssignment = auditAssignment;
            NavigationContext.CurrentAuditType = auditAssignment.TaskType.SystemAuditType;
            await Shell.Current.GoToAsync( nameof( ChecklistPage ) );
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task PullDataAsync() {
            await Shell.Current.GoToAsync( nameof( AuditAssignmentSelectionPage ) );
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task ShowOperationAuditChecklistsAsync( AuditAssignmentDto assignment ) {
            NavigationContext.CurrentAuditAssignment = assignment;
            await Shell.Current.GoToAsync( nameof( OperationAuditPage ) );
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task ShowFinancialAuditChecklistsAsync( AuditAssignmentDto assignment ) {
            NavigationContext.CurrentAuditAssignment = assignment;
            await Shell.Current.GoToAsync( nameof( FinancialAuditPage ) );
        }
    }
}
