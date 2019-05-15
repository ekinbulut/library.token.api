using System;
using Library.Authentication.Service.Dispatchers.Book;
using Library.Authentication.Service.Requests;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.BookDispatcherServiceTests
{
    public class PostBookDispatcherServiceTests
    {
        public PostBookDispatcherServiceTests()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _bookServiceEndPointsMock = new Mock<IOptions<BookServiceEndPointConstants>>();
        }

        private readonly Mock<IDispatcher>                            _dispatcherMock;
        private readonly Mock<IOptions<BookServiceEndPointConstants>> _bookServiceEndPointsMock;
        private          BookServiceDispatcher                        _bookDispatchService;


        [Fact]
        public void WhenRouteToBookServicePost_DispatchReturnsSuccess()
        {
            var request = new PostBookDispatcherRequest
                          {
                              UserId = 1, No = 5, Name = "aaa", Tag = "tag", ShelfId = 1, AuthorId = "id", SkinType = 1
                              , LibraryId = "id", PublisherId = 1, PublisherDate = Convert.ToDateTime("01.01.2019")
                          };

            _bookServiceEndPointsMock.Setup(t => t.Value)
                                     .Returns(() => new BookServiceEndPointConstants {Post = "https://{userid}"});

            _dispatcherMock.Setup(t =>
                                      t.Dispatch<object, object>(It.IsAny<object>(), "https://1", null
                                                                 , HttpRequestCode.POST, null, null))
                           .Verifiable();

            _bookDispatchService = new BookServiceDispatcher(_bookServiceEndPointsMock.Object, _dispatcherMock.Object);

            _bookDispatchService.RouteToBookServicePost(request);

            _dispatcherMock.Verify(
                t => t.Dispatch<object, object>(It.IsAny<object>(), "https://1", null, HttpRequestCode.POST, null,
                                                null), Times.Once);
        }

        [Fact]
        public void WhenRouteToBookServicePost_ThrowsInvalidOperationException()
        {
            _bookServiceEndPointsMock.Setup(t => t.Value)
                                     .Returns(() => new BookServiceEndPointConstants {Post = string.Empty});

            _bookDispatchService = new BookServiceDispatcher(_bookServiceEndPointsMock.Object, _dispatcherMock.Object);

            Assert.Throws<InvalidOperationException>(() =>
                                                         _bookDispatchService.RouteToBookServicePost(
                                                             new PostBookDispatcherRequest()));
        }
    }
}