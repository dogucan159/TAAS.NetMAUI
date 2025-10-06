using TAAS.NetMAUI.Presentation.Data;

namespace TAAS.NetMAUI.Presentation {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();
            //Routing.RegisterRoute( nameof( MainPage ), typeof( MainPage ) );
            Routing.RegisterRoute( nameof( LoginPage ), typeof( LoginPage ) );
            Routing.RegisterRoute( nameof( AuditAssignmentSelectionPage ), typeof( AuditAssignmentSelectionPage ) );
            Routing.RegisterRoute( nameof( OperationAuditPage ), typeof( OperationAuditPage ) );
            Routing.RegisterRoute( nameof( FinancialAuditPage ), typeof( FinancialAuditPage ) );
            Routing.RegisterRoute( nameof( ChecklistPage ), typeof( ChecklistPage ) );
            Routing.RegisterRoute( nameof( ChecklistSelectionPage ), typeof( ChecklistSelectionPage ) );
            Routing.RegisterRoute( nameof( ChecklistDetailPage ), typeof( ChecklistDetailPage ) );
            Routing.RegisterRoute( nameof( QuestionDetailPage ), typeof( QuestionDetailPage ) );

            //InitializeNavigation();

            //var sessionUserId = Preferences.Get( "SessionUserId", -1L );
            //FlyoutBehavior = sessionUserId > 0 ? FlyoutBehavior.Flyout : FlyoutBehavior.Disabled;
        }

        private async void OnSignOutClicked( object sender, EventArgs e ) {
            Preferences.Remove( "SessionUserId" );
            //FlyoutBehavior = FlyoutBehavior.Disabled;
            await GoToAsync( nameof( LoginPage ) );
            //Shell.SetBackButtonBehavior( Shell.Current.CurrentPage, new BackButtonBehavior {
            //    IsVisible = false
            //} );
        }

        private async void InitializeNavigation() {
            try {
                var sessionUserId = Preferences.Get( "SessionUserId", -1L );
                if ( sessionUserId > 0 )
                    await GoToAsync( "//MainPage" );
                else
                    await GoToAsync( "//LoginPage" );
            }
            catch ( Exception ex ) {
                Console.WriteLine( "[Navigation Error] " + ex.Message );
                await GoToAsync( "//LoginPage" );
            }
        }
    }
}
