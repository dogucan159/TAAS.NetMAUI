using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation;

public partial class SelectedAuditAssignmentView : ContentView
{
	public SelectedAuditAssignmentView()
	{
		InitializeComponent();
		BindingContext = new SelectedAuditAssignmentViewViewModel();
    }
}