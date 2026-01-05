using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Utilities.Dialog {
    public interface IDialogService {

        Task<string?> PromptAsync( string title, string message, string placeholder = "", string accept = "OK", string cancel = "Cancel" );
        Task ShowAlertAsync( string title, string message, string cancel = "OK" );

    }
}
