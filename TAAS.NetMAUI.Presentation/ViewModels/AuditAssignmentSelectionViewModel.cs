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
        public async System.Threading.Tasks.Task GetAuditAssignments() {
            if ( IsBusy )
                return;

            if ( String.IsNullOrEmpty( MainTaskEntry ) && String.IsNullOrEmpty( TaskTypeEntry ) && String.IsNullOrWhiteSpace( TaskEntry ) ) {
                await Shell.Current.DisplayAlert( "Error", "Please enter at least one of the main task, task type and task code.", "OK" );
                return;
            }

            try {
                IsBusy = true;
                AuditAssignments.Clear();

                List<AuditAssignmentDto>? apiAuditAssignments = await _manager.ApiService.PullAuditAssignments( MainTaskEntry, TaskTypeEntry, TaskEntry );
                if ( apiAuditAssignments != null && apiAuditAssignments.Count > 0 ) {
                    var dbAuditAssignments = await _manager.AuditAssignmentService.GetAllAuditAssignments( false );
                    if ( dbAuditAssignments != null && dbAuditAssignments.Count > 0 )
                        AuditAssignments = new ObservableCollection<AuditAssignmentDto>( apiAuditAssignments.Where( a => !dbAuditAssignments.Any( d => d.Id == a.Id ) ) );
                    else
                        AuditAssignments = new ObservableCollection<AuditAssignmentDto>( apiAuditAssignments );
                }
                await Shell.Current.DisplayAlert( "Success", "Data pulled successfully!", "OK" );
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[GetAuditAssignments] ERROR: {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to pull data.", "OK" );
            }
            finally {
                IsBusy = false;
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
