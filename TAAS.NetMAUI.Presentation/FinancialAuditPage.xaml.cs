using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation;

public partial class FinancialAuditPage : ContentPage {
    public FinancialAuditPage( FinancialAuditViewModel model ) {
        InitializeComponent();
        BindingContext = model;
    }
}