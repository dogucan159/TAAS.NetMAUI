using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Shared {
    public static class ApiConfigProvider {
        public static String GetAuthSettings() {
#if DEBUG || RELEASE
            return "https://dev-taas.hmb.gov.tr/backend/";
#elif DEBUG_TEST || RELEASE_TEST
            return "https://test-taas.hmb.gov.tr/gateway/offlinetaas/";
#elif RELEASE_PROD
            return "https://release-prod.example.com/api/";
#else
            return null;
#endif
        }
    }
}
