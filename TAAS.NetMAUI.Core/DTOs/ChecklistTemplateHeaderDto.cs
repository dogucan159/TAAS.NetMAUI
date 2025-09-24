using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Core.DTOs {
    public class ChecklistTemplateHeaderDto : BaseDto {
        public String Name { get; set; }
        public int Sequence { get; set; }

        public long ChecklistTemplateId { get; set; }
        public ChecklistTemplateDto ChecklistTemplate { get; set; }
    }
}
