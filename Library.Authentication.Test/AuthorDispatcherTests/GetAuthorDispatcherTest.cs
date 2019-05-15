using System;
using System.Collections.Generic;
using System.Linq;
using Library.Authentication.Service.Dispatchers.Author;
using Library.Authentication.Service.Requests.Authors;
using Library.Authentication.Service.Responses.Authors;
using Library.Authentication.Service.ServiceModels.Authors;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.AuthorDispatcherTests
{
    public class GetAuthorDispatcherTest
    {
        private readonly Mock<IOptions<AuthorServiceEndPointConstants>> _optionsMock;
        private readonly AuthorServiceDispatcher _sut;
        
        public GetAuthorDispatcherTest()
        {
            var dispatcherMock = new Mock<IDispatcher>();
            _optionsMock = new Mock<IOptions<AuthorServiceEndPointConstants>>();
            
            _optionsMock.Setup(t => t.Value).Returns(() => new AuthorServiceEndPointConstants
            {
                Search  = "http://api.authors.com/api/author/search",
                Get = "http://api.authors.com/api/author"
            });
            
            dispatcherMock.Setup(t =>
                    t.Dispatch<object, GetAuthorDispatcherResponse>(
                        null, It.IsAny<string>(), null, HttpRequestCode.GET,
                        It.IsAny<Dictionary<string, object>>(), null))
                .Returns(() => new GetAuthorDispatcherResponse
                {
                    Total = 1,
                    Authors = new List<AuthorModel>
                    {
                        new AuthorModel
                        {
                            Id = "id",
                            Name = "name",
                            Data = new AuthorModelMetaData
                            {
                                Dead = null,
                                Birthday = Convert.ToDateTime("01.01.2019"),
                                Books = null,
                                ImageUrl = null,
                                Location = null,
                                Bio = "bio"
                            }
                        }
                    }
                });
            
            _sut = new AuthorServiceDispatcher(dispatcherMock.Object, _optionsMock.Object);
        }

        [Fact]
        public void When_AuthorServiceEndPointConstantsGet_IsNull_ThrowsInvalidOperationException()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new AuthorServiceEndPointConstants {Get = ""});

            Assert.Throws<InvalidOperationException>(() => _sut.RouteToAuthorGet("5c9f2f04f1d4914a40f7580c"));
        }

        [Fact]
        public void When_RouteToAuthorGet_Returns_Valid_Response()
        {


            var actual = _sut.RouteToAuthorGet( new GetAuthorDispatcherRequest
                                                                            {
                                                                                Limit = 10, Offset = 0
                                                                            });

            Assert.Single(actual.Authors);
            var authorModel = actual.Authors.FirstOrDefault();
            
            Assert.Equal(1, actual.Total);
            Assert.Equal("id", authorModel.Id);
            Assert.Equal("name", authorModel.Name);
            Assert.NotNull(authorModel.Data);
            Assert.Equal("bio",authorModel.Data.Bio);
            Assert.Null(authorModel.Data.Dead);
            Assert.Null(authorModel.Data.Books);
            Assert.Null(authorModel.Data.ImageUrl);
            Assert.Null(authorModel.Data.Location);
            Assert.Equal(Convert.ToDateTime("01.01.2019"), authorModel.Data.Birthday);

        }

        [Fact]
        public void When_RouteToAuthorGetById_Returns_Valid_Response()
        {

            var actual = _sut.RouteToAuthorGet("5c9f2f04f1d4914a40f7580c");

            Assert.Single(actual.Authors);
        }
        
        [Fact]
        public void When_RouteToAuthorSearchGet_Returns_Valid_Response()
        {

            var actual = _sut.RouteToAuthorGet(new GetAuthorSearchDispatcherRequest()
            {
                SearchKey = "search-key"
            });

            Assert.Single(actual.Authors);
        }

        [Fact]
        public void When_SearchKey_IsNullOrEmpty_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.RouteToAuthorGet(new GetAuthorSearchDispatcherRequest()
            {
                SearchKey = string.Empty
            }));
        }

        [Fact]
        public void When_SearchEndpoint_IsNullOrEmpty_Throw_InvalidOperationException()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new AuthorServiceEndPointConstants
            {
                Get = "http://api.authors.com/api/author",
                Search = ""
            });
            
            Assert.Throws<InvalidOperationException>(() => _sut.RouteToAuthorGet(new GetAuthorSearchDispatcherRequest()
            {
               SearchKey = "search-key"
            }));
        }
        
    }
}