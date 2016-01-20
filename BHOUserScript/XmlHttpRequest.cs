using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BHOUserScript
{
    internal class XmlHttpRequestDetails
    {
        [JsonProperty(PropertyName = "url")]
        public string Url = null;

        [JsonProperty(PropertyName = "method")]
        public string Method = null;

        [JsonProperty(PropertyName = "data")]
        public string Data = null;

        [JsonProperty(PropertyName = "headers")]
        public Dictionary<string, string> Headers = null;

        [JsonProperty(PropertyName = "username")]
        public string Username = null;

        [JsonProperty(PropertyName = "password")]
        public string Password = null;
    }

    internal class XmlHttpRequestResponse
    {
        [JsonProperty(PropertyName = "readyState")]
        public int ReadyState = 0;

        [JsonProperty(PropertyName = "responseHeaders")]
        public string ResponseHeaders;

        [JsonProperty(PropertyName = "responseText")]
        public string ResponseText = String.Empty;

        [JsonProperty(PropertyName = "status")]
        public int Status = 200;

        [JsonProperty(PropertyName = "statusText")]
        public string StatusText = String.Empty;

        [JsonProperty(PropertyName = "finalUrl")]
        public string FinalUrl = String.Empty;
    }
}
