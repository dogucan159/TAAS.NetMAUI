using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class ChecklistDetailTaasFile : BaseEntity {
        public long ChecklistDetailId { get; set; }
        public ChecklistDetail ChecklistDetail { get; set; }
        public long TaasFileId { get; set; }
        public TaasFile TaasFile { get; set; }
        public bool? Synched { get; set; }
        public bool? Deleted { get; set; }

    }
}
