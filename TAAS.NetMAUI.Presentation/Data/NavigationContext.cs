using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Presentation.Models;

namespace TAAS.NetMAUI.Presentation.Data {
    public static class NavigationContext {
        public static AuditAssignmentDto? CurrentAuditAssignment { get; set; }
        public static AuditTypeDto? CurrentAuditType { get; set; }
        public static ChecklistItem? CurrentChecklist { get; set; }
        public static long? ChecklistDetailId { get; set; }
    }
}
