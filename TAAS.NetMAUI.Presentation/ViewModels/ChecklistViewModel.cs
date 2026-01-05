using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Presentation.Data;
using TAAS.NetMAUI.Presentation.Models;
using TAAS.NetMAUI.Presentation.Utilities.Dialog;

namespace TAAS.NetMAUI.Presentation.ViewModels {
    public partial class ChecklistViewModel : ObservableObject {
        private readonly IServiceManager _manager;
        private readonly IDialogService _dialogService;

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

        public ChecklistViewModel( IServiceManager manager, IDialogService dialogService ) {
            _manager = manager;
            _dialogService = dialogService;

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

                if ( !checklists.Any() ) {
                    await Shell.Current.DisplayAlert( "Warning", "No checklist found to transfer", "OK" );
                    return;
                }

                bool isConfirmed = await Shell.Current.DisplayAlert(
                "Confirm Deletion",
                "Are you sure you want to transfer checklists to the web service?",
                "Yes", "No" );

                if ( !isConfirmed )
                    return;

                IsBusy = true;


#if DEBUG_TEST || RELEASE_TEST || RELEASE_PROD

                try {

                    try {
                        await _manager.ApiService.SendSmsCode();
                    }
                    catch ( Exception ex ) {
                        await _dialogService.ShowAlertAsync( "Error", $"Failed to send SMS code: {ex.Message}" );
                        return;
                    }
                    finally {
                        IsBusy = false;
                    }

                    var code = await _dialogService.PromptAsync(
                        title: "Enter Verification Code",
                        message: "A verification code has been sent. Please enter it below:",
                        placeholder: "e.g. 123456",
                        accept: "Submit",
                        cancel: "Cancel"
                    );

                    if ( string.IsNullOrWhiteSpace( code ) ) {
                        await _dialogService.ShowAlertAsync( "Cancelled", "Verification was cancelled." );
                        return;
                    }

                    code = code.Trim();

                    IsBusy = true;
                    try {
                        await _manager.ApiService.VerifySmsCode( code );

                        await Transfer();
                        await Shell.Current.DisplayAlert( "Success", "Unsynced files transferred to live!", "OK" );
                        await LoadChecklistsAsync();

                    }
                    catch ( Exception ex ) {
                        await _dialogService.ShowAlertAsync( "Error", $"Failed to verify SMS code or pull data: {ex.Message}" );
                    }
                }
                finally {
                    IsBusy = false;
                }

#else
                try {
                    await Transfer();
                    await Shell.Current.DisplayAlert( "Success", "Unsynced files transferred to live!", "OK" );
                    await LoadChecklistsAsync();
                }
                catch ( Exception ex ) {
                    Debug.WriteLine( $"[TransferToLiveAsync] ERROR: {ex.Message}" );
                    await Shell.Current.DisplayAlert( "Error", "Failed to pull data.", "OK" );
                }
                finally {
                    IsBusy = false;
                }
#endif
            }
        }

        private async System.Threading.Tasks.Task Transfer() {
            try {
                //CODE HERE
                //bool isDeleteAfterTransfer = DeleteAfterTransfer;

                var checklists = await _manager.ChecklistService.GetChecklistsWithDetailsByAuditAssignmentIdAndAuditTypeId( NavigationContext.CurrentAuditAssignment.Id,
                    NavigationContext.CurrentAuditType.Id, true );
                var auditorDto = await _manager.AuditorService.GetByMachineName( false );
                await _manager.ApiService.TransferChecklistsToLive( checklists, auditorDto );
            }
            catch ( Exception ex ) {
                throw new Exception( ex.Message );
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
