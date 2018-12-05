using RedoxApi.ModelActions;
using RestSharp;
using System;
using System.Net.Http;
using PatientAdmin = RedoxApi.Models.Patientadmin;

namespace RedoxApi
{
    public class RedoxClient
    {
        private RestClient client { get; set; }

        public RedoxClient(string apiKey, string secret)
        {
            this.client = new RestClient(Urls.Base)
            {
                Authenticator = new RedoxAuthenticator(apiKey, secret)
            };
        }

        internal IRestResponse<T> Post<T, S>(S data) where T : new()
        {
            var request = new RestRequest(Urls.Endpoint, Method.POST);
            request.AddJsonBody(data);
            var response = client.Execute<T>(request);

            return response;
        }

        internal IRestResponse<T> Post<T>(T data) where T : new()
        {
            var request = new RestRequest(Urls.Endpoint, Method.POST);
            request.AddJsonBody(data);
            var response = client.Execute<T>(request);

            return response;
        }

        internal IRestResponse<T> Query<T>(T data) where T : new()
        {
            var request = new RestRequest(Urls.Query, Method.POST);
            request.AddJsonBody(data);
            var response = client.Execute<T>(request);

            return response;
        }

        public PatientAdminActions PatientAdmin { get { return new PatientAdminActions(this); } }

        public SchedulingActions Scheduling { get { return new SchedulingActions(this); } }

        public MediaActions Media { get { return new MediaActions(this); } }
        
    }



}
