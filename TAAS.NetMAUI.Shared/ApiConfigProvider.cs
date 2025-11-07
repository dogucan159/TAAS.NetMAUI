using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Shared {
    public static class ApiConfigProvider {
        public static EndpointSettings GetEndpointSettings() {
            var rootAddress = "";
            var controllerName = "";
#if DEBUG || RELEASE
            rootAddress= "https://dev-taas.hmb.gov.tr/backend/";
#elif DEBUG_TEST || RELEASE_TEST
            rootAddress = "https://test-taas.hmb.gov.tr/gateway/offlinetaas/";
            controllerName = "taas-offline/";
#elif RELEASE_PROD
            rootAddress= "https://release-prod.example.com/api/taas-offline";
#else
            rootAddress = null;
#endif
            return new EndpointSettings {
                RootAddress = rootAddress,
                ControllerName = controllerName
            };

        }
    }
}
