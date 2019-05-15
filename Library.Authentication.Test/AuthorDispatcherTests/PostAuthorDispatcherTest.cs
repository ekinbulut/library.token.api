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
    public class PostAuthorDispatcherTest
    {
        public PostAuthorDispatcherTest()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _optionsMock = new Mock<IOptions<AuthorServiceEndPointConstants>>();

            _optionsMock.Setup(t => t.Value).Returns(() => new AuthorServiceEndPointConstants
                                                           {Post = "http://api.authors.com/api/author"});

            _service = new AuthorServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);
        }

        private readonly Mock<IDispatcher> _dispatcherMock;
        private readonly Mock<IOptions<AuthorServiceEndPointConstants>> _optionsMock;

        private AuthorServiceDispatcher _service;

        [Fact]
        public void WhenAuthorServiceEndPointConstantsPost_IsNull_ThrowsInvalidOperationException()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new AuthorServiceEndPointConstants {Post = ""});

            _service = new AuthorServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);

            Assert.Throws<InvalidOperationException>(() =>
                                                         _service.RouteToAuthorPost(
                                                             It.IsAny<PostAuthorDispatcherRequest>()));
        }

        [Fact]
        public void WhenRouteToAuthorPost_Success()
        {
            _service.RouteToAuthorPost(new PostAuthorDispatcherRequest()
                                       {
                                           Bio = "bio",
                                           Dead = null,
                                           Name = "name",
                                           Books = null,
                                           Birthday = DateTime.Now,
                                           Location = "",
                                           ImageUrl = ""
                                       });

            _dispatcherMock.Verify(
                t => t.Dispatch<object, object>(It.IsAny<object>(), It.IsAny<string>(), null, HttpRequestCode.POST,
                                                null, null), Times.Once);
        }
    }
}