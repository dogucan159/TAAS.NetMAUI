using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class TaskType : BaseEntity {
        public String Code { get; set; }
        public String Description { get; set; }

        public long SystemAuditTypeId { get; set; }
        public AuditType SystemAuditType { get; set; }
    }
}
