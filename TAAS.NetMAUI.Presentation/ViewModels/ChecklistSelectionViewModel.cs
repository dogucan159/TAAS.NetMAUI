using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Presentation.Data;
using TAAS.NetMAUI.Presentation.Models;

namespace TAAS.NetMAUI.Presentation.ViewModels {

    public partial class ChecklistSelectionViewModel : ObservableObject {

        private readonly IServiceManager _manager;

        [ObservableProperty]
        private ObservableCollection<ChecklistItem> checklists = new ObservableCollection<ChecklistItem>();

        [ObservableProperty]
        private bool isBusy = false;
        public bool HasSelected => Checklists != null && Checklists.Any( c => c.IsSelected );

        public ChecklistSelectionViewModel( IServiceManager manager ) {
            _manager = manager;
        }

        public ICommand ToggleChecklistSelectionCommand => new Command<ChecklistItem>( item => {
            item.IsSelected = !item.IsSelected;

            OnPropertyChanged( nameof( HasSelected ) );
        } );

        [RelayCommand]
        public async System.Threading.Tasks.Task GetChecklistsAsync() {
            if ( this.IsBusy )
                return;

            try {
                IsBusy = true;

                var apiChecklists = await _manager.ApiService.PullChecklistsByAuditAssignmentIdAndAuditTypeIdFromAPI( NavigationContext.CurrentAuditAssignment.Id, NavigationContext.CurrentAuditType.Id );

                if ( apiChecklists != null && apiChecklists.Count > 0 ) {
                    var dbChecklists = await _manager.ChecklistService.GetChecklistsByAuditAssignmentIdAndAuditTypeId( NavigationContext.CurrentAuditAssignment.Id, NavigationContext.CurrentAuditType.Id, false );
                    if ( dbChecklists != null && dbChecklists.Count > 0 )
                        this.Checklists = new ObservableCollection<ChecklistItem>( ChecklistDtoToChecklistItemConverter.Convert( apiChecklists.Where( c => !dbChecklists.Any( d => d.Id == c.Id ) ) ) );
                    else
                        this.Checklists = new ObservableCollection<ChecklistItem>( ChecklistDtoToChecklistItemConverter.Convert( apiChecklists ) );
                }
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[GetChecklistsAsync] ERROR: {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to pull data.", "OK" );
            }
            finally {
                IsBusy = false;
            }

        }

        [RelayCommand]
        private async System.Threading.Tasks.Task SyncSelectedAsync() {
            if ( IsBusy )
                return;
            try {
                IsBusy = true;

                //Sync

                var selectedChecklists = Checklists.Where( x => x.IsSelected ).ToList();

                foreach ( var selectedChecklist in selectedChecklists ) {
                    var checklistDto = await _manager.ApiService.PullChecklistFromAPI( selectedChecklist.Id );

                    await _manager.ApiService.SyncChecklistAsync( checklistDto );
                }

                await Shell.Current.GoToAsync( nameof( ChecklistPage ) );
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
