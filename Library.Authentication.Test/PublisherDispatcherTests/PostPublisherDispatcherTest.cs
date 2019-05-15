using System;
using Library.Authentication.Service.Dispatchers.Publisher;
using Library.Authentication.Service.Requests.Publisher;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.PublisherDispatcherTests
{
    public class PostPublisherDispatcherTest
    {
        public PostPublisherDispatcherTest()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _optionsMock = new Mock<IOptions<PublisherServiceEndPointConstants>>();

            _optionsMock.Setup(t => t.Value).Returns(() => new PublisherServiceEndPointConstants
                                                           {Post = "http://api.publisher.com/api/publisher"});

            _service = new PublisherServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);
        }

        private readonly Mock<IDispatcher> _dispatcherMock;
        private readonly Mock<IOptions<PublisherServiceEndPointConstants>> _optionsMock;

        private PublisherServiceDispatcher _service;

        [Fact]
        public void WhenPublisherServiceEndPointConstantsPost_IsNull_ThrowsInvalidOperationException()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new PublisherServiceEndPointConstants {Post = ""});

            _service = new PublisherServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);

            Assert.Throws<InvalidOperationException>(() =>
                                                         _service.RouteToPublisherPost(
                                                             It.IsAny<PostPublisherDispatcherRequest>()));
        }

        [Fact]
        public void WhenRouteToPublisherPost_Success()
        {
            _service.RouteToPublisherPost(new PostPublisherDispatcherRequest
                                          {
                                              Name = "name",
                                              Series = null
                                          });
                                      

            _dispatcherMock.Verify(
                t => t.Dispatch<object, object>(It.IsAny<object>(), It.IsAny<string>(), null, HttpRequestCode.POST,
                                                null, null), Times.Once);
        }
    }
}