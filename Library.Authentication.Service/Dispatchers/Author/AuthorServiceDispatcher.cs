using System;
using System.Collections.Generic;
using Library.Authentication.Service.Requests.Authors;
using Library.Authentication.Service.Responses.Authors;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Library.Services.Common.Resources;
using Microsoft.Extensions.Options;

namespace Library.Authentication.Service.Dispatchers.Author
{
    
    public class AuthorServiceDispatcher : IAuthorServiceDispatcher
    {
        private readonly IDispatcher _dispatcher;
        private readonly IOptions<AuthorServiceEndPointConstants> _options;

        public AuthorServiceDispatcher(IDispatcher dispatcher, IOptions<AuthorServiceEndPointConstants> options)
        {
            _dispatcher = dispatcher;
            _options = options;
        }

        public GetAuthorDispatcherResponse RouteToAuthorGet(GetAuthorDispatcherRequest request)
        {
            var url = _options.Value.Get;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            var queryParameters = new Dictionary<string, object>
                                  {
                                      {"offset", request.Offset}, {"limit", request.Limit}
                                  };

            var response =
                _dispatcher.Dispatch<object, GetAuthorDispatcherResponse>(null, url, null, HttpRequestCode.GET,
                                                                          queryParameters);

            return response;
        }

        public GetAuthorDispatcherResponse RouteToAuthorGet(string id)
        {
            var url = _options.Value.Get;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            url = url + $"/{id}";

            var response =
                _dispatcher.Dispatch<object, GetAuthorDispatcherResponse>(null, url, null, HttpRequestCode.GET,
                                                                          null);

            return response;
        }

        public GetAuthorDispatcherResponse RouteToAuthorGet(GetAuthorSearchDispatcherRequest request)
        {
            var url = _options.Value.Search;

            if (string.IsNullOrEmpty(request.SearchKey))
            {
                throw new ArgumentNullException(ErrorResources.SearchKeyCannotBeNull);
            }

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);


            var response =
                _dispatcher.Dispatch<object, GetAuthorDispatcherResponse>(null, url, null, HttpRequestCode.GET,
                    new Dictionary<string, object>()
                    {
                        {"SearchText", request.SearchKey}
                    });

            return response;
        }

        public void RouteToAuthorPost(PostAuthorDispatcherRequest request)
        {
            var url = _options.Value.Post;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            var restRequest = new
                              {
                                  name = request.Name, bio = request.Bio, birthday = request.Birthday
                                  , dead = request.Dead, location = request.Location, imageUrl = request.ImageUrl
                                  , books = request.Books
                              };


            _dispatcher.Dispatch<object, object>(restRequest, url, null, HttpRequestCode.POST, null);
        }

        public void RouteToAuthorPut(PutAuthorDispatcherRequest request)
        {
            var url = _options.Value.Put;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            url = url.Replace("{id}", request.Id);

            var restRequest = new
                              {
                                  name = request.Name, bio = request.Bio, birthday = request.Birthday
                                  , dead = request.Dead, location = request.Location, imageUrl = request.ImageUrl
                                  , books = request.Books
                              };

            _dispatcher.Dispatch<object, object>(restRequest, url, null, HttpRequestCode.PUT, null);
        }

        public void RouteToAuthorDelete(string id)
        {
            var url = _options.Value.Delete;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

            url = url.Replace("{id}", id.ToString());

            _dispatcher.Dispatch<object, object>(null, url, null, HttpRequestCode.DELETE, null);
        }
    }
}