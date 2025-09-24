using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Core.DTOs {
    public class TaasFileUpdateDto : BaseDto {
        public Guid ObjectId { get; set; }
        public long ApiId { get; set; }
    }
}
