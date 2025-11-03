using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Utilities {
    public interface ITokenUtility {
        public Task<String> GetToken();
    }
}
