using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class ChecklistAuditor : BaseEntity {
        public long ChecklistId { get; set; }
        public Checklist Checklist { get; set; }

        public long AuditorId { get; set; }
        public Auditor Auditor { get; set; }


    }
}
