using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RedoxApi.Models;

namespace RedoxApi
{
    /// <summary>
    /// Keeps track of the token and refreshes it when necessary. Runs before client.execute().
    /// </summary>
    class RedoxAuthenticator : IAuthenticator
    {
        private Object credentials { get { return new { apiKey = apiKey, secret = secret, refreshToken = refreshToken }; } }
        private string apiKey { get; set; }
        private string secret { get; set; }
        private string token { get; set; }
        private string refreshToken { get; set; }
        private DateTime? tokenExpires { get; set; }

        public RedoxAuthenticator(string apiKey, string secret)
        {
            this.apiKey = apiKey;
            this.secret = secret;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            //get a token if we don't have one yet
            if (!string.IsNullOrEmpty(token)) {
                if (tokenExpires != null && DateTime.Now > tokenExpires)
                {
                    //refresh token
                    GetToken(Urls.RefreshToken);                    
                }                
            }
            else
            {
                //get the token for the first time
                GetToken(Urls.Authenticate);
            }

            //ok we've got a token, so put the authorization header on the request
            request.AddHeader("Authorization", "Bearer " + token);
        }


        private void GetToken(string endpoint)
        {            
            var authClient = new RestClient(Urls.Base);
            var authorizationRequest = new RestRequest(endpoint, Method.POST);
            authorizationRequest.AddHeader("Content-type", "application/json");
            authorizationRequest.AddJsonBody(credentials);

            var response = authClient.Execute<AuthorizationResponse>(authorizationRequest);

            //probably something can go wrong with the request, check it out here...
            
            if (response.Data != null)
            {
                token = response.Data.accessToken;
                tokenExpires = response.Data.expires;
                refreshToken = response.Data.refreshToken;
            }
        }
    }
}
