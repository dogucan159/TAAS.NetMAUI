using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.DTOs {
    public class KeyRequirementDto : BaseDto {
        public String Code { get; set; }
        public String GeneralCode { get; set; }
        public String Description { get; set; }
    }
}
