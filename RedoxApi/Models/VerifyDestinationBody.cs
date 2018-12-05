using Newtonsoft.Json;

namespace RedoxApi.Models
{
    public class VerifyDestinationBody
    {
        [JsonProperty(PropertyName = "verification-token")]
        public string verification_token { get; set; }
        public string challenge { get; set; }
    }
}
