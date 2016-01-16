using System;
using System.Collections.Generic;

namespace BHOUserScript
{
    class XmlHttpRequestDetails
    {
        public string url;

        public string method;

        public string data = null;

        public Dictionary<string, string> headers = null;

        public string username = null;

        public string password = null;
    }

    class XmlHttpRequestResponse
    {
        public int readyState = 0;

        public string[] responseHeaders;

        public string responseText = String.Empty;

        public int status = 200;

        public string statusText = String.Empty;

        public string finalUrl = String.Empty;
    }
}
