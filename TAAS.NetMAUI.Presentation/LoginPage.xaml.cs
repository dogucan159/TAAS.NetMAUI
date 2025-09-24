using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation;

public partial class LoginPage : ContentPage {
    public LoginPage( LoginViewModel model ) {
        InitializeComponent();
        BindingContext = model;
    }
}