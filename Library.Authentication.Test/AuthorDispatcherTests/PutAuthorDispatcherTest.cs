using System;
using Library.Authentication.Service.Dispatchers.Author;
using Library.Authentication.Service.Requests.Authors;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.AuthorDispatcherTests
{
    public class PutAuthorDispatcherTest
    {
        public PutAuthorDispatcherTest()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _optionsMock = new Mock<IOptions<AuthorServiceEndPointConstants>>();

            _optionsMock.Setup(t => t.Value).Returns(() => new AuthorServiceEndPointConstants
                                                           {Put = "http://api.authors.com/api/author"});

            _service = new AuthorServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);
        }

        private readonly Mock<IDispatcher> _dispatcherMock;
        private readonly Mock<IOptions<AuthorServiceEndPointConstants>> _optionsMock;

        private AuthorServiceDispatcher _service;

        [Fact]
        public void WhenAuthorServiceEndPointConstantsPost_IsNull_ThrowsInvalidOperationException()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new AuthorServiceEndPointConstants {Put = ""});

            _service = new AuthorServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);

            Assert.Throws<InvalidOperationException>(() =>
                                                         _service.RouteToAuthorPut(
                                                             It.IsAny<PutAuthorDispatcherRequest>()));
        }

        [Fact]
        public void WhenRouteToAuthorPut_Success()
        {
            _service.RouteToAuthorPut(new PutAuthorDispatcherRequest()
                                      {
                                          Id = "id",
                                          Bio = "bio",
                                          Dead = null,
                                          Name = "name",
                                          Books = null,
                                          Birthday = DateTime.Now,
                                          Location = "",
                                          ImageUrl = ""
                                             
                                      });

            _dispatcherMock.Verify(
                t => t.Dispatch<object, object>(It.IsAny<object>(), It.IsAny<string>(), null, HttpRequestCode.PUT, null,
                                                null), Times.Once);
        }
    }
}