using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.UnitTest
{
    public class APIConsumer
    {
        private RestClient _client;
        //http://localhost:4752/
        private string baseUrl = "http://localhost:4752/";
        private string bearerToken = "";
        public APIConsumer()
        {
            _client = new RestClient(baseUrl);
        }
        public dynamic Get(string endpoint, Dictionary<string, object> headers = null)
        {
            if (bearerToken!=null)
                _client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", bearerToken));

            if (headers != null)
            {
                foreach (var key in headers.Keys)
                    _client.AddDefaultHeader(key, headers[key].ToString());
            }
            var request = new RestRequest(endpoint, DataFormat.Json);
            var response = _client.Get(request);
            return response.Content;
        }

        public dynamic Post<T>(string endpoint, T model)
        {
            if (bearerToken != null)
                _client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", bearerToken));


            var request = new RestRequest(endpoint).AddJsonBody(model);
            var response = _client.Post(request);
            return response.Content;
        }
        public dynamic Delete(string endpoint, Dictionary<string, object> headers = null)
        {
            if (bearerToken != null)
                _client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", bearerToken));

            if (headers != null)
            {
                foreach (var key in headers.Keys)
                    _client.AddDefaultHeader(key, headers[key].ToString());
            }
            var request = new RestRequest(endpoint, DataFormat.Json);
            var response = _client.Delete(request);
            return response.Content;
        }
    }
}
