using System;
using System.Collections.Generic;
using Library.Authentication.Service.Requests.Storage;
using Library.Authentication.Service.Responses.Storage;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Library.Services.Common.Resources;
using Microsoft.Extensions.Options;

namespace Library.Authentication.Service.Dispatchers.Storage
{
    public class StorageServiceDispatcher : IStorageServiceDispatcher
    {
        private readonly IDispatcher _dispatcher;
        private readonly IOptions<StorageServiceEndPointConstants> _options;

        public StorageServiceDispatcher(IDispatcher dispatcher, IOptions<StorageServiceEndPointConstants> options)
        {
            _dispatcher = dispatcher;
            _options = options;
        }

        public StorageServiceResponse RouteToStorageGet(StorageServiceRequest request)
        {
            var url = _options.Value.Get;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            var queryParameters = new Dictionary<string, object>
                                  {
                                      {"offset", request.Offset}, {"limit", request.Limit}
                                  };

            var response = _dispatcher.Dispatch<object, StorageServiceResponse>(null, url, null, HttpRequestCode.GET,
                                                                                queryParameters);

            return response;
        }

        public StorageServiceResponse RouteToStorageGet(string id)
        {
            var url = _options.Value.Get;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            url = url + $"/{id}";

            var response = _dispatcher.Dispatch<object, StorageServiceResponse>(null, url, null, HttpRequestCode.GET,
                                                                                null);

            return response;
        }

        public void RouteToStoragePost(PostStorageServiceRequest request)
        {
            var url = _options.Value.Post;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            var restRequest = new
                              {
                                  name = request.Name, rackNumber = request.RackNumber
                              };


            _dispatcher.Dispatch<object, object>(restRequest, url, null, HttpRequestCode.POST, null);
        }

        public void RouteToStoragePut(PutStorageServiceRequest request)
        {
            var url = _options.Value.Put;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            url = url.Replace("{id}", request.Id);

            var restRequest = new
                              {
                                  name = request.Name, rackNumber = request.RackNumber
                              };

            _dispatcher.Dispatch<object, object>(restRequest, url, null, HttpRequestCode.PUT, null);
        }

        public void RouteToStorageDelete(string id)
        {
            var url = _options.Value.Delete;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            url = url.Replace("{id}", id);

            _dispatcher.Dispatch<object, object>(null, url, null, HttpRequestCode.DELETE, null);
        }
    }
}