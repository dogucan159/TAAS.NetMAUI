using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class ChecklistHeader : BaseEntity {
        public long ChecklistId { get; set; }
        public Checklist Checklist { get; set; }

        public long ChecklistTemplateHeaderId { get; set; }
        public ChecklistTemplateHeader ChecklistTemplateHeader { get; set; }

        public String? Value { get; set; }
        public int Version { get; set; }
    }
}
