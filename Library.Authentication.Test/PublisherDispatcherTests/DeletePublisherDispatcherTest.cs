using System;
using Library.Authentication.Service.Dispatchers.Publisher;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.PublisherDispatcherTests
{
    public class DeletePublisherDispatcherTest
    {
        public DeletePublisherDispatcherTest()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _optionsMock = new Mock<IOptions<PublisherServiceEndPointConstants>>();
        }

        private readonly Mock<IDispatcher> _dispatcherMock;
        private readonly Mock<IOptions<PublisherServiceEndPointConstants>> _optionsMock;

        private PublisherServiceDispatcher _service;

        [Fact]
        public void WhenDispatchOperation_IsSuccessful_VerifyDispatchCalledOnce()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new PublisherServiceEndPointConstants
                                                           {
                                                               Delete = "http://api.com/delete"
                                                           });


            _service = new PublisherServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);

            _service.RouteToPublisherDelete("5c9f2f04f1d4914a40f7580c");

            _dispatcherMock.Verify(
                t => t.Dispatch<object, object>(null, It.IsAny<string>(), null, HttpRequestCode.DELETE, null, null),
                Times.Once);
        }

        [Fact]
        public void WhenOptionValueIsNull_ThrowInvalidObjectException()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new PublisherServiceEndPointConstants());

            _service = new PublisherServiceDispatcher(null, _optionsMock.Object);
            Assert.Throws<InvalidOperationException>(() => _service.RouteToPublisherDelete("5c9f2f04f1d4914a40f7580c"));
        }
    }
}