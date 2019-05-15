using System.Collections.Generic;
using Library.Authentication.Service.ServiceModels.Publisher;
using Newtonsoft.Json;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Library.Authentication.Service.Responses.Publisher
{
    public class GetPublisherDispatcherResponse
    {
        [JsonProperty(PropertyName = "publisherCollection")]
        [SerializeAs(Name = "publisherCollection")]
        [DeserializeAs(Name = "publisherCollection")]
        public List<PublisherModel> Publishers { get; set; }
        public int? Total { get; set; }

    }
}