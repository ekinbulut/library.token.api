using System.Collections.Generic;

namespace Library.Authentication.Service.Requests.Publisher
{
    public class PutPublisherHttpRequest
    {
        public string Name { get; set; }
        public List<string> Series { get; set; }
    }
}