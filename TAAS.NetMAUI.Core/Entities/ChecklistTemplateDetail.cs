using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class ChecklistTemplateDetail : BaseEntity {
        public String Comment { get; set; }
        public String? CommentTr { get; set; }
        public bool? Title { get; set; }
        public int Sequence { get; set; }

        public long ChecklistTemplateId { get; set; }
        public ChecklistTemplate ChecklistTemplate { get; set; }
    }
}
