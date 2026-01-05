using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Utilities.Dialog {
    public class DialogService : IDialogService {
        public Task<string?> PromptAsync( string title, string message, string placeholder = "", string accept = "OK", string cancel = "Cancel" ) {
            return Application.Current.MainPage.DisplayPromptAsync( title, message, accept, cancel, placeholder );
        }

        public Task ShowAlertAsync( string title, string message, string cancel = "OK" ) {
            return Application.Current.MainPage.DisplayAlert( title, message, cancel );
        }
    }
}
