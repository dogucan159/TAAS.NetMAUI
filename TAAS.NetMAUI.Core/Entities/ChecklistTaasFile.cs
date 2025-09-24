using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class ChecklistTaasFile : BaseEntity {
        public long ChecklistId { get; set; }
        public Checklist Checklist { get; set; }
        public long TaasFileId { get; set; }
        public TaasFile TaasFile { get; set; }
        public bool? Synched { get; set; }
        public bool? Deleted { get; set; }
    }
}
