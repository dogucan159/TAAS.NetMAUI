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
            return new Window( new AppShell() );


        }
    }
}