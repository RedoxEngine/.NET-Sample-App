using RestSharp;
using System;
using System.Net.Http;
using NewPatient = RedoxApi.Models.Patientadmin.Newpatient.Newpatient;
using PatientUpdate = RedoxApi.Models.Patientadmin.Patientupdate.Patientupdate;

namespace RedoxApi
{
    public class RedoxClient
    {
        private RestClient Client { get; set; }

        public RedoxClient(string apiKey, string secret)
        {
            this.Client = new RestClient(Urls.Base)
            {
                Authenticator = new RedoxAuthenticator(apiKey, secret)
            };
        }

        private IRestResponse<T> Post<T>(T data) where T : new()
        {
            var request = new RestRequest(Urls.Endpoint, Method.POST);
            request.AddJsonBody(data);
            var response = Client.Execute<T>(request);

            return response;
        }

        private IRestResponse<T> Query<T>(T data) where T : new()
        {
            var request = new RestRequest(Urls.Query, Method.POST);
            request.AddJsonBody(data);
            var response = Client.Execute<T>(request);

            return response;
        }

        public IRestResponse<NewPatient> NewPatient(NewPatient patient)
        {
            patient.Meta = new Models.Patientadmin.Newpatient.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "NewPatient",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return Post(patient);
        }

        public IRestResponse<PatientUpdate> UpdatePatient(PatientUpdate patient)
        {
            patient.Meta = new Models.Patientadmin.Patientupdate.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "PatientUpdate",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };
                        
            return Post(patient);
        }

        //and so on. include a function for each type of message Redox supports.
    }
}
