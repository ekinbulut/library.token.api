using System;
using System.Collections.Generic;
using Library.Authentication.Service.Requests.Publisher;
using Library.Authentication.Service.Responses.Publisher;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Library.Services.Common.Resources;
using Microsoft.Extensions.Options;

namespace Library.Authentication.Service.Dispatchers.Publisher
{
    public class PublisherServiceDispatcher : IPublisherServiceDispatcher
    {
        private readonly IDispatcher _dispatcher;
        private readonly IOptions<PublisherServiceEndPointConstants> _options;

        public PublisherServiceDispatcher(IDispatcher dispatcher, IOptions<PublisherServiceEndPointConstants> options)
        {
            _dispatcher = dispatcher;
            _options = options;
        }

        public GetPublisherDispatcherResponse RouteToPublishers(GetPublisherDispatcherRequest request)
        {
            var url = _options.Value.Get;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            var queryParameters = new Dictionary<string, object>
                                  {
                                      {"offset", request.Offset}, {"limit", request.Limit}
                                  };

            var response =
                _dispatcher.Dispatch<object, GetPublisherDispatcherResponse>(null, url, null, HttpRequestCode.GET,
                                                                          queryParameters);

            return response;
        }
        
        public GetPublisherDispatcherResponse RouteToPublisher(string id)
        {
            var url = _options.Value.Get;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            url = url + $"/{id}";

            var response =
                _dispatcher.Dispatch<object, GetPublisherDispatcherResponse>(null, url, null, HttpRequestCode.GET,
                                                                          null);

            return response;
        }

        public void RouteToPublisherPost(PostPublisherDispatcherRequest request)
        {
            var url = _options.Value.Post;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            var restRequest = new
                              {
                                  name = request.Name, series = request.Series
                              };


            _dispatcher.Dispatch<object, object>(restRequest, url, null, HttpRequestCode.POST, null);
        }
        
        public void RouteToPublisherPut(PutPublisherDispatcherRequest request)
        {
            var url = _options.Value.Put;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            url = url.Replace("{id}", request.Id);

            var restRequest = new
                              {
                                  name = request.Name, series = request.Series
                              };

            _dispatcher.Dispatch<object, object>(restRequest, url, null, HttpRequestCode.PUT, null);
        }
        
        public void RouteToPublisherDelete(string id)
        {
            var url = _options.Value.Delete;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            url = url.Replace("{id}", id);

            _dispatcher.Dispatch<object, object>(null, url, null, HttpRequestCode.DELETE, null);
        }
    }
}