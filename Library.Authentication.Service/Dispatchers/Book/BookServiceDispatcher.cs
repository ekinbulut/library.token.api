using System;
using System.Collections.Generic;
using Library.Authentication.Service.Requests;
using Library.Authentication.Service.Responses;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Library.Services.Common.Resources;
using Microsoft.Extensions.Options;

namespace Library.Authentication.Service.Dispatchers.Book
{
    public class BookServiceDispatcher : IBookServiceDispatcher
    {
        private readonly IOptions<BookServiceEndPointConstants> _bookServiceEndPoints;
        private readonly IDispatcher _dispatcher;

        public BookServiceDispatcher(IOptions<BookServiceEndPointConstants> bookServiceEndPoints,
                                     IDispatcher dispatcher)
        {
            _bookServiceEndPoints = bookServiceEndPoints;
            _dispatcher = dispatcher;
        }

        public BookDispatcherResponse RouteToBookServiceGet(BookDispatcherRequest request)
        {
            var url = _bookServiceEndPoints.Value.Get;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

#pragma warning disable S1854 // Dead stores should be removed
            url = url.Replace("{userid}", request.UserId.ToString());
#pragma warning restore S1854 // Dead stores should be removed

            var queryParameters = new Dictionary<string, object>
                                  {
                                      {"offset", request.Offset}, {"limit", request.Limit}
                                  };

            var result =
                _dispatcher.Dispatch<object, BookDispatcherResponse>(null, url, null, HttpRequestCode.GET,
                                                                     queryParameters);

            return result;
        }

        public void RouteToBookServicePost(PostBookDispatcherRequest request)
        {
            var url = _bookServiceEndPoints.Value.Post;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

#pragma warning disable S1854 // Dead stores should be removed
            url = url.Replace("{userid}", request.UserId.ToString());
#pragma warning restore S1854 // Dead stores should be removed

            var restRequest = new
                              {
                                  name = request.Name, authorId = request.AuthorId, publisherId = request.PublisherId
                                  , tag = request.Tag, no = request.No, publisherDate = request.PublisherDate
                                  , skinType = request.SkinType, libraryId = request.LibraryId
                              };


            _dispatcher.Dispatch<object, object>(restRequest, url, null, HttpRequestCode.POST, null);
        }

        public void RouteToBookServiceDelete(int userId, int bookId)
        {
            var url = _bookServiceEndPoints.Value.Delete;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

#pragma warning disable S1854 // Dead stores should be removed
            url = url.Replace("{userid}", userId.ToString());
#pragma warning restore S1854 // Dead stores should be removed

            url = url + $"/{bookId}";

            _dispatcher.Dispatch<object, object>(null, url, null, HttpRequestCode.DELETE, null);
        }

        public void RouteToBookServicePut(PutBookDispatcherRequest request)
        {
            var url = _bookServiceEndPoints.Value.Put;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

#pragma warning disable S1854 // Dead stores should be removed
            url = url.Replace("{userid}", request.UserId.ToString());
            url = url.Replace("{id}", request.Id.ToString());
#pragma warning restore S1854 // Dead stores should be removed

            var restRequest = new
                              {
                                  name = request.Name, authorId = request.AuthorId, publisherId = request.PublisherId
                                  , tag = request.Tag, no = request.No, publisherDate = request.PublisherDate
                                  , skinType = request.SkinType, libraryId = request.LibraryId
                              };


            _dispatcher.Dispatch<object, object>(restRequest, url, null, HttpRequestCode.PUT, null);
        }

        public BookDispatcherResponse RouteToBookServiceGet(int userId, int bookId)
        {
            var url = _bookServiceEndPoints.Value.Get;

            if (string.IsNullOrEmpty(url))
                throw new InvalidOperationException(ErrorResources.EndpointIsNotValidOrEmpty);

#pragma warning disable S1854 // Dead stores should be removed
            url = url.Replace("{userid}", userId.ToString());
#pragma warning restore S1854 // Dead stores should be removed

            var queryParameters = new Dictionary<string, object>
                                  {
                                      {"id", bookId}
                                  };

            var result =
                _dispatcher.Dispatch<object, BookDispatcherResponse>(null, url, null, HttpRequestCode.GET,
                                                                     queryParameters);

            return result;
        }
    }
}