using System.Threading.Tasks;
using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation;

public partial class ChecklistPage : ContentPage {
    public ChecklistPage( ChecklistViewModel model ) {
        InitializeComponent();
        BindingContext = model;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();

        if ( BindingContext is ChecklistViewModel vm )
            await vm.LoadChecklistsAsync();

    }
}