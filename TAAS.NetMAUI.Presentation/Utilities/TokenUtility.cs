using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Shared;

namespace TAAS.NetMAUI.Presentation.Utilities {
    public class TokenUtility : ITokenUtility {
        private readonly IServiceManager _manager;
        private readonly AuthSettings _settings;

        public TokenUtility( IServiceManager manager ) {
            _manager = manager;
            _settings = AuthConfigProvider.GetAuthSettings();
        }

        private async Task<string> GetServiceToServiceToken() {
            try {
                var client = new HttpClient();

                var tokenEndpoint = _settings.TokenEndpoint; // Replace with actual endpoint

                var requestBody = new FormUrlEncodedContent( new[]
                {
                    new KeyValuePair<string, string>("client_id", _settings.ClientId),
                    new KeyValuePair<string, string>("client_secret", _settings.ClientSecret),
                    new KeyValuePair<string, string>("grant_type", _settings.GrantType),
                    new KeyValuePair<string, string>("scope", _settings.Scope)
                } );

                var request = new HttpRequestMessage( HttpMethod.Post, tokenEndpoint ) {
                    Content = requestBody
                };

                request.Content.Headers.ContentType = new MediaTypeHeaderValue( "application/x-www-form-urlencoded" );

                var response = await client.SendAsync( request );
                var responseContent = await response.Content.ReadAsStringAsync();

                var tokenResult = JsonConvert.DeserializeObject<TokenResult>( responseContent )?.access_token;

                return tokenResult ?? String.Empty;
            }
            catch ( Exception ex ) {
                throw new Exception( ex.Message );
            }
        }

        public async Task<string> GetToken() {
            String token = "";
#if DEBUG_TEST || RELEASE_TEST || RELEASE_PROD
            token = await GetServiceToServiceToken();
#else
            var sessionUserId = Preferences.Get( "SessionUserId", -1L );
            var sessionUser = await _manager.AuditorService.GetById( sessionUserId, false );
            token = sessionUser.AccessToken ?? "";
            
#endif
            return token;
        }
    }
}
