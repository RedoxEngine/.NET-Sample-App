using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedoxApi.Models
{
    public class AuthorizationResponse
    {
        public string accessToken { get; set; }
        public DateTime expires { get; set; }
        public string refreshToken { get; set; }
    }
}
