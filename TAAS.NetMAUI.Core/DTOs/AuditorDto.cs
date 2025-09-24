using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.DTOs {
    public class AuditorDto : BaseDto {
        public required String FirstName { get; set; }
        public required String LastName { get; set; }
        public required String IdentificationNumber { get; set; }
        public required String Password { get; set; }
        public String? AccessToken { get; set; }
    }
}
