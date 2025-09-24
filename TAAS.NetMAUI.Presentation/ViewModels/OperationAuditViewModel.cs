using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Presentation.Data;

namespace TAAS.NetMAUI.Presentation.ViewModels {
    public partial class OperationAuditViewModel : ObservableObject {
        [ObservableProperty]
        private ObservableCollection<AuditTypeDto> auditTypeList;

        public OperationAuditViewModel() {
            List<AuditTypeDto> lstAuditType = new List<AuditTypeDto>();


            var auditAssignmentOperationAuditTypes = NavigationContext.CurrentAuditAssignment?.AuditAssignmentOperationAuditTypes;
            if ( auditAssignmentOperationAuditTypes != null )
                lstAuditType.AddRange( auditAssignmentOperationAuditTypes.Select( x => x.AuditType ) );


            this.AuditTypeList = new ObservableCollection<AuditTypeDto>( lstAuditType );
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task ShowChecklistsAsync( AuditTypeDto auditType ) {
            NavigationContext.CurrentAuditType = auditType;
            await Shell.Current.GoToAsync( nameof( ChecklistPage ) );
        }

    }
}
