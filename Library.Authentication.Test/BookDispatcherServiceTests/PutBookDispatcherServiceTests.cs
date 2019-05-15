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
    public class PutBookDispatcherServiceTests
    {
        public PutBookDispatcherServiceTests()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _bookServiceEndPointsMock = new Mock<IOptions<BookServiceEndPointConstants>>();
        }

        private readonly Mock<IDispatcher> _dispatcherMock;
        private readonly Mock<IOptions<BookServiceEndPointConstants>> _bookServiceEndPointsMock;
        private BookServiceDispatcher _bookDispatchService;


        [Fact]
        public void WhenRouteToBookServicePut_DispatchReturnsSuccess()
        {
            var request = new PutBookDispatcherRequest
                          {
                              UserId = 1, No = 5, Name = "aaa", Tag = "tag", ShelfId = 1, AuthorId = "id", SkinType = 1
                              , LibraryId = "id", PublisherId = 1, PublisherDate = Convert.ToDateTime("01.01.2019"), Id = 1
                          };

            _bookServiceEndPointsMock.Setup(t => t.Value)
                .Returns(() => new BookServiceEndPointConstants {Put = "https://{userid}"});

            _dispatcherMock.Setup(t =>
                                      t.Dispatch<object, object>(It.IsAny<object>(), "https://1", null
                                                                 , HttpRequestCode.PUT, null, null))
                .Verifiable();

            _bookDispatchService = new BookServiceDispatcher(_bookServiceEndPointsMock.Object, _dispatcherMock.Object);

            _bookDispatchService.RouteToBookServicePut(request);

            _dispatcherMock.Verify(
                t => t.Dispatch<object, object>(It.IsAny<object>(), "https://1", null, HttpRequestCode.PUT, null, null),
                Times.Once);
        }

        [Fact]
        public void WhenRouteToBookServicePut_ThrowsInvalidOperationException()
        {
            _bookServiceEndPointsMock.Setup(t => t.Value)
                .Returns(() => new BookServiceEndPointConstants {Put = string.Empty});

            _bookDispatchService = new BookServiceDispatcher(_bookServiceEndPointsMock.Object, _dispatcherMock.Object);

            Assert.Throws<InvalidOperationException>(() =>
                                                         _bookDispatchService.RouteToBookServicePut(
                                                             new PutBookDispatcherRequest()));
        }
    }
}