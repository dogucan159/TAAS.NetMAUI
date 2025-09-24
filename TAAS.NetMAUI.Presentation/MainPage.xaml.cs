using System.Threading.Tasks;
using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation {
    public partial class MainPage : ContentPage {


        public MainPage( MainPageViewModel model ) {
            InitializeComponent();
            BindingContext = model;
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            if ( BindingContext is MainPageViewModel vm ) 
                await vm.LoadAuditAssignmentsAsync();
        }
    }
}
