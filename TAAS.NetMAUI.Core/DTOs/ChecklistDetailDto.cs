using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Core.DTOs {
    public class ChecklistDetailDto : BaseDto {
        public long ChecklistId { get; set; }
        public ChecklistDto Checklist { get; set; }
        public string? Answer { get; set; }
        public string? Explanation { get; set; }
        public string? ExplanationFormatted { get; set; }
        public long ChecklistTemplateDetailId { get; set; }
        public int Version { get; set; }
        public ChecklistTemplateDetailDto ChecklistTemplateDetail { get; set; }
        public ICollection<ChecklistDetailTaasFileDto> ChecklistDetailTaasFiles { get; set; }
        public ICollection<TaasFileDto> TaasFiles { get; set; }
    }
}
