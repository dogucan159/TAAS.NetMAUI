using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TAAS.NetMAUI.Business;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Presentation.Utilities.Dialog;

namespace TAAS.NetMAUI.Presentation.ViewModels {
    public partial class AuditAssignmentSelectionViewModel : ObservableObject {

        private readonly IServiceManager _manager;
        private readonly IDialogService _dialogService;
        [ObservableProperty]
        private string mainTaskEntry = "";
        [ObservableProperty]
        private string taskTypeEntry = "";
        [ObservableProperty]
        private string taskEntry = "";
        [ObservableProperty]
        private bool isBusy = false;
        [ObservableProperty]
        private ObservableCollection<AuditAssignmentDto> auditAssignments = new ObservableCollection<AuditAssignmentDto>();
        [ObservableProperty]
        private AuditAssignmentDto selectedAuditAssignment;
        public bool HasSelected => SelectedAuditAssignment != null;

        public AuditAssignmentSelectionViewModel( IServiceManager manager, IDialogService dialogService ) {
            _manager = manager;
            _dialogService = dialogService;
        }

        [RelayCommand]
        public async System.Threading.Tasks.Task GetAuditAssignments() {
            if ( IsBusy )
                return;

            if ( String.IsNullOrEmpty( MainTaskEntry ) && String.IsNullOrEmpty( TaskTypeEntry ) && String.IsNullOrWhiteSpace( TaskEntry ) ) {
                await Shell.Current.DisplayAlert( "Error", "Please enter at least one of the main task, task type and task code.", "OK" );
                return;
            }

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
                    await Pull();
                    await _dialogService.ShowAlertAsync( "Success", "Data pulled successfully!" );
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
                await Pull();
                await Shell.Current.DisplayAlert( "Success", "Data pulled successfully!", "OK" );
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[GetAuditAssignments] ERROR: {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to pull data.", "OK" );
            }
            finally {
                IsBusy = false;
            }
#endif

        }

        private async System.Threading.Tasks.Task Pull() {
            try {
                List<AuditAssignmentDto>? apiAuditAssignments = await _manager.ApiService.PullAuditAssignments( MainTaskEntry, TaskTypeEntry, TaskEntry );
                if ( apiAuditAssignments != null && apiAuditAssignments.Count > 0 ) {
                    var dbAuditAssignments = await _manager.AuditAssignmentService.GetAllAuditAssignments( false );
                    if ( dbAuditAssignments != null && dbAuditAssignments.Count > 0 )
                        AuditAssignments = new ObservableCollection<AuditAssignmentDto>( apiAuditAssignments.Where( a => !dbAuditAssignments.Any( d => d.Id == a.Id ) ) );
                    else
                        AuditAssignments = new ObservableCollection<AuditAssignmentDto>( apiAuditAssignments );
                }
            }
            catch ( Exception ex ) {
                throw new Exception( ex.Message );
            }
        }

        [RelayCommand]
        public async System.Threading.Tasks.Task Clear() {
            if ( IsBusy )
                return;
            MainTaskEntry = "";
            TaskTypeEntry = "";
            TaskEntry = "";
        }

        public ICommand SelectAuditAssignmentCommand => new Command<AuditAssignmentDto>( auditAssignment => {
            SelectedAuditAssignment = auditAssignment;
            OnPropertyChanged( nameof( HasSelected ) );
        } );

        [RelayCommand]
        private async System.Threading.Tasks.Task SyncSelectedAsync() {
            if ( IsBusy )
                return;
            try {
                IsBusy = true;

                //Sync
                await _manager.ApiService.SyncAuditAssignmentAsync( SelectedAuditAssignment );

                await Shell.Current.GoToAsync( "//MainPage" );
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[SyncDataAsync] ERROR: {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to sync data.", "OK" );
            }
            finally {
                IsBusy = false;
            }

        }

    }
}
