using System.Collections.Generic;

namespace Library.Authentication.Service.Requests.Publisher
{
    public class PutPublisherDispatcherRequest
    {
        public string       Id     { get; set; }
        public string       Name   { get; set; }
        public List<string> Series { get; set; }
    }
}