using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Presentation.Data;

namespace TAAS.NetMAUI.Presentation.ViewModels {
    public partial class SelectedAuditAssignmentViewViewModel : ObservableObject {

        [ObservableProperty]
        private AuditAssignmentDto currentAuditAssignment;

        public SelectedAuditAssignmentViewViewModel() {
            CurrentAuditAssignment = NavigationContext.CurrentAuditAssignment;
        }
    }

}
