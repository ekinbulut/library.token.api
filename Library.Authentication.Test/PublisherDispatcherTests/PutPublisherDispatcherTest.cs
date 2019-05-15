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
    public class PutPublisherDispatcherTest
    {
        public PutPublisherDispatcherTest()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _optionsMock = new Mock<IOptions<PublisherServiceEndPointConstants>>();

            _optionsMock.Setup(t => t.Value).Returns(() => new PublisherServiceEndPointConstants
                                                           {Put = "http://api.authors.com/api/author"});

            _service = new PublisherServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);
        }

        private readonly Mock<IDispatcher> _dispatcherMock;
        private readonly Mock<IOptions<PublisherServiceEndPointConstants>> _optionsMock;

        private PublisherServiceDispatcher _service;

        [Fact]
        public void When_PublisherServiceEndPointConstantsPut_IsNull_ThrowsInvalidOperationException()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new PublisherServiceEndPointConstants {Put = ""});

            _service = new PublisherServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);

            Assert.Throws<InvalidOperationException>(() =>
                                                         _service.RouteToPublisherPut(
                                                             It.IsAny<PutPublisherDispatcherRequest>()));
        }

        [Fact]
        public void When_RouteToPublisherPut_Returns_Success_VerifyDispatcherCall_Once()
        {
            _service.RouteToPublisherPut(new PutPublisherDispatcherRequest
                                      {
                                          Id = "id",
                                          Name = "name",
                                          Series = null
                                      });

            _dispatcherMock.Verify(
                t => t.Dispatch<object, object>(It.IsAny<object>(), It.IsAny<string>(), null, HttpRequestCode.PUT, null,
                                                null), Times.Once);
        }
    }
}