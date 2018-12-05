using System;
using System.Collections.Generic;
using System.Text;

namespace RedoxApi
{
    // TODO: Make these environment specific or configurable?
    public static class Urls
    {
        public static string Base = "https://api.redoxengine.com/";
        public static string Authenticate = "auth/authenticate";
        public static string RefreshToken = "auth/refreshToken";
        public static string Endpoint = "endpoint";
        public static string Query = "query";
    }
}
