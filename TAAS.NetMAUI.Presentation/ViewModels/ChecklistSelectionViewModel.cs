using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Presentation.Data;
using TAAS.NetMAUI.Presentation.Models;
using TAAS.NetMAUI.Presentation.Utilities;

namespace TAAS.NetMAUI.Presentation.ViewModels {

    public partial class ChecklistSelectionViewModel : ObservableObject {

        private readonly IServiceManager _manager;
        private readonly ITokenUtility _tokenUtility;

        [ObservableProperty]
        private ObservableCollection<ChecklistItem> checklists = new ObservableCollection<ChecklistItem>();

        [ObservableProperty]
        private bool isBusy = false;
        public bool HasSelected => Checklists != null && Checklists.Any( c => c.IsSelected );

        public ChecklistSelectionViewModel( IServiceManager manager, ITokenUtility tokenUtility ) {
            _manager = manager;
            _tokenUtility = tokenUtility;
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

                string token = await _tokenUtility.GetToken();

                var apiChecklists = await _manager.ApiService.PullChecklistsByAuditAssignmentIdAndAuditTypeIdFromAPI( NavigationContext.CurrentAuditAssignment.Id, NavigationContext.CurrentAuditType.Id, token );

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

                string token = await _tokenUtility.GetToken();

                var selectedChecklists = Checklists.Where( x => x.IsSelected ).ToList();

                foreach ( var selectedChecklist in selectedChecklists ) {
                    var checklistDto = await _manager.ApiService.PullChecklistFromAPI( selectedChecklist.Id, token );

                    await _manager.ApiService.SyncChecklistAsync( checklistDto, token );
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
