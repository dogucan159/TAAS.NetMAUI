using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class KeyRequirement : BaseEntity {
        public String Code { get; set; }
        public String GeneralCode { get; set; }
        public String Description { get; set; }
    }
}
