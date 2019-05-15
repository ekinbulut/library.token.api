using System.Collections.Generic;

namespace Library.Authentication.Service.ServiceModels.Publisher
{
    public class PublisherModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Series { get; set; }
    }
}