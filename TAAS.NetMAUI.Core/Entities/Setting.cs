using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.Entities {
    public class Setting : BaseEntity {
        public String Key { get; set; }
        public String Value { get; set; }
    }
}
