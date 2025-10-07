using TAAS.NetMAUI.Infrastructure.Data;
using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation {
    public partial class App : Application {

        private readonly IServiceProvider _serviceProvider;
        public App( IServiceProvider serviceProvider ) {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            TaasDbInitializer.Initialize( serviceProvider );
            UserAppTheme = AppTheme.Dark;

        }

        protected override Window CreateWindow( IActivationState? activationState ) {
            var sessionUserId = Preferences.Get( "SessionUserId", -1L );

            Page startupPage;

            if ( sessionUserId > 0 ) {
                startupPage = new AppShell();
            }
            else {
                var loginViewModel = _serviceProvider.GetRequiredService<LoginViewModel>();
                startupPage = new LoginPage( loginViewModel );
            }

            return new Window( startupPage );


        }
    }
}