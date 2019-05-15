using System;
using Library.Authentication.Service.Dispatchers.Author;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.AuthorDispatcherTests
{
    public class DeleteAuthorDispatcherTest
    {
        public DeleteAuthorDispatcherTest()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _optionsMock = new Mock<IOptions<AuthorServiceEndPointConstants>>();
        }

        private readonly Mock<IDispatcher> _dispatcherMock;
        private readonly Mock<IOptions<AuthorServiceEndPointConstants>> _optionsMock;

        private AuthorServiceDispatcher _service;

        [Fact]
        public void WhenDispatchOperation_IsSuccessful_VerifyDispatchCalledOnce()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new AuthorServiceEndPointConstants
                                                           {
                                                               Delete = "http://api.com/delete"
                                                           });


            _service = new AuthorServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);

            _service.RouteToAuthorDelete("5c9f2f04f1d4914a40f7580c");

            _dispatcherMock.Verify(
                t => t.Dispatch<object, object>(null, It.IsAny<string>(), null, HttpRequestCode.DELETE, null, null),
                Times.Once);
        }

        [Fact]
        public void WhenOptionValueIsNull_ThrowInvalidObjectException()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new AuthorServiceEndPointConstants());

            _service = new AuthorServiceDispatcher(null, _optionsMock.Object);
            Assert.Throws<InvalidOperationException>(() => _service.RouteToAuthorDelete("5c9f2f04f1d4914a40f7580c"));
        }
    }
}