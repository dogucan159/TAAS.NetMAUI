using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class TaasFile : BaseEntity {
        public String Name { get; set; }
        public int Size { get; set; }
        public Guid ObjectId { get; set; }
        public byte[] FileData { get; set; }
        public bool? Synched { get; set; }
        public long ApiId { get; set; }
        public ICollection<ChecklistDetailTaasFile> ChecklistDetailTaasFiles { get; set; }
        public ICollection<ChecklistTaasFile> ChecklistTaasFiles { get; set; }
    }
}
