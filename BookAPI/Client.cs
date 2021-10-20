using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookAPI
{
    public class Client
    {
        public HttpClient Authapi()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44311/");
            return client;
        }
        public HttpClient Index()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44392/");
            return client;

        }
    }
}
