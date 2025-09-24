using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation;

public partial class AuditAssignmentSelectionPage : ContentPage {
    public AuditAssignmentSelectionPage( AuditAssignmentSelectionViewModel model ) {
        InitializeComponent();
        BindingContext = model;
    }
}