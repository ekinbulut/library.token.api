using System;
using Library.Authentication.Service.Dispatchers.Book;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.BookDispatcherServiceTests
{
    public class DeleteBookDispatcherServiceTests
    {
        public DeleteBookDispatcherServiceTests()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _bookServiceEndPointsMock = new Mock<IOptions<BookServiceEndPointConstants>>();
        }

        private readonly Mock<IDispatcher> _dispatcherMock;
        private readonly Mock<IOptions<BookServiceEndPointConstants>> _bookServiceEndPointsMock;
        private BookServiceDispatcher _bookDispatchService;


        [Fact]
        public void WhenRouteToBookServiceDelete_DispatchReturnsSuccess()
        {

            _bookServiceEndPointsMock.Setup(t => t.Value).Returns(() => new BookServiceEndPointConstants
                                                                        {Delete = "https://{userid}"});

            _bookDispatchService = new BookServiceDispatcher(_bookServiceEndPointsMock.Object, _dispatcherMock.Object);

            _bookDispatchService.RouteToBookServiceDelete(1, 5);

            _dispatcherMock.Verify(
                t => t.Dispatch<object, object>(null, It.IsAny<string>(), null, HttpRequestCode.DELETE, null, null),
                Times.Once);
        }

        [Fact]
        public void WhenRouteToBookServiceDelete_ThrowsInvalidOperationException()
        {
            _bookServiceEndPointsMock.Setup(t => t.Value)
                .Returns(() => new BookServiceEndPointConstants {Delete = string.Empty});

            _bookDispatchService = new BookServiceDispatcher(_bookServiceEndPointsMock.Object, _dispatcherMock.Object);

            Assert.Throws<InvalidOperationException>(() =>
                                                         _bookDispatchService.RouteToBookServiceDelete(1, 5));
        }
    }
}