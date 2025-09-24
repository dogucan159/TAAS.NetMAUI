using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Business;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Shared;

namespace TAAS.NetMAUI.Presentation.ViewModels {
    public partial class LoginViewModel : ObservableObject {
        private readonly IServiceManager _manager;

        public String IdentificationNumber { get; set; } = String.Empty;
        public String Password { get; set; } = String.Empty;
        public String ErrorMessage { get; set; } = String.Empty;
        public bool HasError => !string.IsNullOrEmpty( ErrorMessage );


        public LoginViewModel( IServiceManager manager ) {
            _manager = manager;
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task Login() {
            var auditorDto = await _manager.AuditorService.GetByIdentificationNumber( IdentificationNumber, false );
            if ( auditorDto != null && auditorDto.Password == PasswordHelper.Hash( Password ) ) {
                Preferences.Set( "SessionUserId", auditorDto.Id );
                if ( Application.Current != null )
                    Application.Current.Windows[0].Page = new AppShell();
            }
            else {
                ErrorMessage = "Invalid credentials. Please try again.";
                OnPropertyChanged( nameof( ErrorMessage ) );
                OnPropertyChanged( nameof( HasError ) );

            }
        }
    }
}
