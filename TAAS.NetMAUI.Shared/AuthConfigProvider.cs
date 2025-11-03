using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Shared {
    public static class AuthConfigProvider {
        public static AuthSettings GetAuthSettings() {
#if DEBUG_TEST || RELEASE_TEST
            return new AuthSettings {
                TokenEndpoint = "https://test-giris.hmb.gov.tr/oauth2.0/accessToken",
                ClientId = "taas-client",
                ClientSecret = "?d52X6/uUuZ?P%2bwG",
                GrantType= "client_credentials",
                Scope= "0FFLINETAAS"
            };
#elif RELEASE_PROD
            return new AuthSettings {
                TokenEndpoint = "https://release-prod.example.com/token",
                ClientId = "release-prod-client-id",
                ClientSecret = "release-prod-secret",
                GrantType= "release-prod-grant-type",
                Scope= "sccope"
            };
#else
            return null;
#endif
        }
    }
}
