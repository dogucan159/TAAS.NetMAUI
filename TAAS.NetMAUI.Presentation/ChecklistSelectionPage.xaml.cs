using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation;

public partial class ChecklistSelectionPage : ContentPage
{
	public ChecklistSelectionPage(ChecklistSelectionViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}

    protected async override void OnAppearing() {
        base.OnAppearing();

		if ( BindingContext is ChecklistSelectionViewModel model )
			await model.GetChecklistsAsync();		
    }
}