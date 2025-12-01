using TAAS.NetMAUI.Presentation.Data;

namespace TAAS.NetMAUI.Presentation {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();
            Routing.RegisterRoute( nameof( LoginPage ), typeof( LoginPage ) );
            Routing.RegisterRoute( nameof( AuditAssignmentSelectionPage ), typeof( AuditAssignmentSelectionPage ) );
            Routing.RegisterRoute( nameof( OperationAuditPage ), typeof( OperationAuditPage ) );
            Routing.RegisterRoute( nameof( FinancialAuditPage ), typeof( FinancialAuditPage ) );
            Routing.RegisterRoute( nameof( ChecklistPage ), typeof( ChecklistPage ) );
            Routing.RegisterRoute( nameof( ChecklistSelectionPage ), typeof( ChecklistSelectionPage ) );
            Routing.RegisterRoute( nameof( ChecklistDetailPage ), typeof( ChecklistDetailPage ) );
            Routing.RegisterRoute( nameof( QuestionDetailPage ), typeof( QuestionDetailPage ) );
        }

        //private async void OnSignOutClicked( object sender, EventArgs e ) {
        //    Preferences.Remove( "SessionUserId" );
        //    FlyoutBehavior = FlyoutBehavior.Disabled;
        //    await GoToAsync( nameof( LoginPage ) );
        //    Shell.SetBackButtonBehavior( Shell.Current.CurrentPage, new BackButtonBehavior {
        //        IsVisible = false
        //    } );
        //}
    }
}
