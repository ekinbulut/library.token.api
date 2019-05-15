using System.Collections.Generic;
using Library.Authentication.Service.ServiceModels.Publisher;

namespace Library.Authentication.Service.Responses.Publisher
{
    public class GetPublisherHttpResponse
    {
        public IEnumerable<PublisherModel> Publishers { get; set; }
        public int? Total { get; set; }
    }
}