using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation;

public partial class OperationAuditPage : ContentPage {
    public OperationAuditPage( OperationAuditViewModel model ) {
        InitializeComponent();
        BindingContext = model;
    }
}