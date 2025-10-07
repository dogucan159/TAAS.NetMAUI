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

namespace TAAS.NetMAUI.Presentation.ViewModels {
    public partial class AuditAssignmentSelectionViewModel : ObservableObject {

        private readonly IServiceManager _manager;
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

        public AuditAssignmentSelectionViewModel( IServiceManager manager ) {
            _manager = manager;
        }

        [RelayCommand]
        public async System.Threading.Tasks.Task GetAuditAssignmentsByMainTaskAsync() {
            if ( IsBusy )
                return;

            try {
                if ( !String.IsNullOrWhiteSpace( MainTaskEntry ) ) {
                    IsBusy = true;
                    AuditAssignments.Clear();
                    var sessionUserId = Preferences.Get( "SessionUserId", -1L );
                    var sessionUser = await _manager.AuditorService.GetById( sessionUserId, false );

                    List<AuditAssignmentDto>? apiAuditAssignments = await _manager.ApiService.PullAuditAssignmentsByMainTaskFromAPI( MainTaskEntry, sessionUser.AccessToken );
                    if ( apiAuditAssignments != null && apiAuditAssignments.Count > 0 ) {
                        var dbAuditAssignments = await _manager.AuditAssignmentService.GetAllAuditAssignments( false );
                        if ( dbAuditAssignments != null && dbAuditAssignments.Count > 0 )
                            AuditAssignments = new ObservableCollection<AuditAssignmentDto>( apiAuditAssignments.Where( a => !dbAuditAssignments.Any( d => d.Id == a.Id ) ) );
                        else
                            AuditAssignments = new ObservableCollection<AuditAssignmentDto>( apiAuditAssignments );
                    }
                    await Shell.Current.DisplayAlert( "Success", "Data pulled successfully!", "OK" );
                }
                else
                    await Shell.Current.DisplayAlert( "Error", "Please enter a main task code.", "OK" );
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[GetAuditAssignmentsByMainTaskAsync] ERROR: {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to pull data.", "OK" );
            }
            finally {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async System.Threading.Tasks.Task GetAuditAssignmentsByTaskTypeAsync() {
            if ( IsBusy )
                return;

            try {
                if ( !String.IsNullOrWhiteSpace( TaskTypeEntry ) ) {
                    IsBusy = true;
                    AuditAssignments.Clear();
                    var sessionUserId = Preferences.Get( "SessionUserId", -1L );
                    var sessionUser = await _manager.AuditorService.GetById( sessionUserId, false );

                    List<AuditAssignmentDto>? apiAuditAssignments = await _manager.ApiService.PullAuditAssignmentsByTaskTypeFromAPI( TaskTypeEntry, sessionUser.AccessToken );
                    if ( apiAuditAssignments != null && apiAuditAssignments.Count > 0 ) {
                        var dbAuditAssignments = await _manager.AuditAssignmentService.GetAllAuditAssignments( false );
                        if ( dbAuditAssignments != null && dbAuditAssignments.Count > 0 )
                            AuditAssignments = new ObservableCollection<AuditAssignmentDto>( apiAuditAssignments.Where( a => !dbAuditAssignments.Any( d => d.Id == a.Id ) ) );
                        else
                            AuditAssignments = new ObservableCollection<AuditAssignmentDto>( apiAuditAssignments );
                    }
                    await Shell.Current.DisplayAlert( "Success", "Data pulled successfully!", "OK" );
                }
                else
                    await Shell.Current.DisplayAlert( "Error", "Please enter a task type code.", "OK" );
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[GetAuditAssignmentsByTaskTypeAsync] ERROR: {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to pull data.", "OK" );
            }
            finally {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async System.Threading.Tasks.Task GetAuditAssignmentsByTaskAsync() {
            if ( IsBusy )
                return;

            try {
                if ( !String.IsNullOrWhiteSpace( TaskEntry ) ) {
                    IsBusy = true;
                    AuditAssignments.Clear();
                    var sessionUserId = Preferences.Get( "SessionUserId", -1L );
                    var sessionUser = await _manager.AuditorService.GetById( sessionUserId, false );

                    List<AuditAssignmentDto>? apiAuditAssignments = await _manager.ApiService.PullAuditAssignmentsByTaskFromAPI( TaskEntry, sessionUser.AccessToken );
                    if ( apiAuditAssignments != null && apiAuditAssignments.Count > 0 ) {
                        var dbAuditAssignments = await _manager.AuditAssignmentService.GetAllAuditAssignments( false );
                        if ( dbAuditAssignments != null && dbAuditAssignments.Count > 0 )
                            AuditAssignments = new ObservableCollection<AuditAssignmentDto>( apiAuditAssignments.Where( a => !dbAuditAssignments.Any( d => d.Id == a.Id ) ) );
                        else
                            AuditAssignments = new ObservableCollection<AuditAssignmentDto>( apiAuditAssignments );
                    }
                    await Shell.Current.DisplayAlert( "Success", "Data pulled successfully!", "OK" );
                }
                else
                    await Shell.Current.DisplayAlert( "Error", "Please enter a task code.", "OK" );
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[GetAuditAssignmentsByTaskAsync] ERROR: {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to pull data.", "OK" );
            }
            finally {
                IsBusy = false;
            }
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
