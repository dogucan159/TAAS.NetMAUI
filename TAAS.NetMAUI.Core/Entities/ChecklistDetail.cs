using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class ChecklistDetail : BaseEntity {
        public long ChecklistId { get; set; }
        public Checklist Checklist { get; set; }      
        public string? Answer { get; set; }
        public string? Explanation { get; set; }
        public string? ExplanationFormatted { get; set; }

        public long ChecklistTemplateDetailId { get; set; }
        public int Version { get; set; }
        public ChecklistTemplateDetail ChecklistTemplateDetail { get; set; }
        public ICollection<ChecklistDetailTaasFile> ChecklistDetailTaasFiles { get; set; }
    }
}
