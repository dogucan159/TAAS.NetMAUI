using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Presentation.Data;
using TAAS.NetMAUI.Presentation.Models;

namespace TAAS.NetMAUI.Presentation.ViewModels {
    public partial class ChecklistViewModel : ObservableObject {
        private readonly IServiceManager _manager;

        [ObservableProperty]
        private ObservableCollection<ChecklistItem> checklists = new ObservableCollection<ChecklistItem>();

        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private bool deleteAfterTransfer = true;

        [ObservableProperty]
        private string breadcrumbText = "";

        [ObservableProperty]
        private bool isNotSystemAudit = false;

        public ChecklistViewModel( IServiceManager manager ) {
            _manager = manager;

            IsNotSystemAudit = NavigationContext.CurrentAuditAssignment?.TaskType.SystemAuditTypeId != NavigationContext.CurrentAuditType?.Id;

            if ( IsNotSystemAudit )
                BreadcrumbText = NavigationContext.CurrentAuditAssignment?.AuditAssignmentOperationAuditTypes?.Any( at => at.AuditTypeId == NavigationContext.CurrentAuditType?.Id ) == true ?
                    "Operation Audit Types" :
                    NavigationContext.CurrentAuditAssignment?.AuditAssignmentFinancialAuditTypes?.Any( at => at.AuditTypeId == NavigationContext.CurrentAuditType?.Id ) == true ?
                    "Financial Audit Types" : "";
            
        }

        public async System.Threading.Tasks.Task LoadChecklistsAsync() {
            var result = await _manager.ChecklistService.GetChecklistsByAuditAssignmentIdAndAuditTypeId( NavigationContext.CurrentAuditAssignment.Id, NavigationContext.CurrentAuditType.Id, false );
            Checklists = new ObservableCollection<ChecklistItem>( ChecklistDtoToChecklistItemConverter.Convert( result ) );
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task ShowChecklistDetail( ChecklistItem checklistItem ) {
            NavigationContext.CurrentChecklist = checklistItem;
            await Shell.Current.GoToAsync( nameof( ChecklistDetailPage ) );
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task PullDataAsync() {
            await Shell.Current.GoToAsync( nameof( ChecklistSelectionPage ) );
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task TransferToLiveAsync() {
            if ( IsBusy )
                return;
            else {
                try {

                    bool isConfirmed = await Shell.Current.DisplayAlert(
                        "Confirm Deletion",
                        "Are you sure you want to transfer checklists to the web service?",
                        "Yes", "No" );

                    if ( !isConfirmed )
                        return;

                    IsBusy = true;
                    //CODE HERE
                    bool isDeleteAfterTransfer = DeleteAfterTransfer;

                    var checklists = await _manager.ChecklistService.GetChecklistsWithDetailsByAuditAssignmentIdAndAuditTypeId( NavigationContext.CurrentAuditAssignment.Id,
                        NavigationContext.CurrentAuditType.Id, true );

                    if ( checklists.Any() ) {
                        var auditorDto = await _manager.AuditorService.GetByMachineName( false );
                        await _manager.ApiService.TransferChecklistsToLive( checklists, auditorDto );
                        await Shell.Current.DisplayAlert( "Success", "Unsynced files transferred to live!", "OK" );

                        await LoadChecklistsAsync();
                    }
                    else
                        await Shell.Current.DisplayAlert( "Warning", "No checklist found to transfer", "OK" );

                    //
                }
                catch ( Exception ex ) {
                    Debug.WriteLine( $"[TransferToLive] ERROR: {ex.Message}" );
                    await Shell.Current.DisplayAlert( "Error", "An error occurred during transfer.", "OK" );
                }
                finally {
                    IsBusy = false;
                }
            }
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task NavigateToMainPage() {
            await Shell.Current.GoToAsync( "//MainPage" );
        }
        [RelayCommand]
        private async System.Threading.Tasks.Task NavigateToAuditPage() {

            if ( NavigationContext.CurrentAuditAssignment?.AuditAssignmentOperationAuditTypes?.Any( at => at.AuditTypeId == NavigationContext.CurrentAuditType?.Id ) == true )
                await Shell.Current.GoToAsync( nameof( OperationAuditPage ) );
            else if ( NavigationContext.CurrentAuditAssignment?.AuditAssignmentFinancialAuditTypes?.Any( at => at.AuditTypeId == NavigationContext.CurrentAuditType?.Id ) == true ) 
                await Shell.Current.GoToAsync( nameof( FinancialAuditPage ) );
            

        }


    }
}
