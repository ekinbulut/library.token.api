using System;
using System.Collections.Generic;
using System.Linq;
using Library.Authentication.Service.Dispatchers.Book;
using Library.Authentication.Service.Requests;
using Library.Authentication.Service.Responses;
using Library.Authentication.Service.ServiceModels.Books;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.BookDispatcherServiceTests
{
    public class GetBookDispatchServiceTests
    {
        public GetBookDispatchServiceTests()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _bookServiceEndPointsMock = new Mock<IOptions<BookServiceEndPointConstants>>();
        }

        private readonly Mock<IDispatcher> _dispatcherMock;
        private readonly Mock<IOptions<BookServiceEndPointConstants>> _bookServiceEndPointsMock;
        private BookServiceDispatcher _bookDispatchService;

        [Fact]
        public void WhenRouteToBookServiceGet_ReturnsResult()
        {
            _bookServiceEndPointsMock.Setup(t => t.Value)
                .Returns(() => new BookServiceEndPointConstants {Get = "addr"});

            var queryParameters = new Dictionary<string, object>
                                  {
                                      {"offset", 1}, {"limit", 10}
                                  };

            _dispatcherMock.Setup(t => t.Dispatch<object, BookDispatcherResponse>(null,
                                                                                  _bookServiceEndPointsMock.Object.Value
                                                                                      .Get, null, HttpRequestCode.GET
                                                                                  , queryParameters, null))
                .Returns(() => new BookDispatcherResponse
                               {
                                   Books = new List<BookModel>
                                           {
                                               new BookModel()
                                               {
                                                   Id = 1,
                                                   No = 1,
                                                   Tag = "tag",
                                                   Name = "name",
                                                   ShelfId = 1,
                                                   AuthorId = "id",
                                                   SkinType = 1,
                                                   LibraryId = "id",
                                                   PublishDate = Convert.ToDateTime("01.01.2019"),
                                                   PublisherId = 1
                                               }
                                           }
                                   , Total = 1
                               });

            _bookDispatchService = new BookServiceDispatcher(_bookServiceEndPointsMock.Object, _dispatcherMock.Object);

            var actual = _bookDispatchService.RouteToBookServiceGet(new BookDispatcherRequest
                                                                    {UserId = 1, Limit = 10, Offset = 1});

            var bookModel = actual.Books.FirstOrDefault();
            Assert.Equal(1, actual.Total);
            
            Assert.Equal(1, bookModel.Id);
            Assert.Equal(1, bookModel.No);
            Assert.Equal("tag", bookModel.Tag);
            Assert.Equal("name", bookModel.Name);
            Assert.Equal(1, bookModel.ShelfId);
            Assert.Equal("id", bookModel.AuthorId);
            Assert.Equal(1, bookModel.SkinType);
            Assert.Equal("id", bookModel.LibraryId);
            Assert.Equal(Convert.ToDateTime("01.01.2019"), bookModel.PublishDate);
            Assert.Equal(1, bookModel.PublisherId);

        }

        [Fact]
        public void WhenRouteToBookServiceGetById_ReturnsResult()
        {
            _bookServiceEndPointsMock.Setup(t => t.Value)
                .Returns(() => new BookServiceEndPointConstants {Get = "addr"});

            var queryParameters = new Dictionary<string, object>
                                  {
                                      {"id", 1}
                                  };

            _dispatcherMock.Setup(t => t.Dispatch<object, BookDispatcherResponse>(null,
                                                                                  _bookServiceEndPointsMock.Object.Value
                                                                                      .Get, null, HttpRequestCode.GET
                                                                                  , queryParameters, null))
                .Returns(() => new BookDispatcherResponse
                               {
                                   Books = new List<BookModel>
                                           {
                                               new BookModel()
                                           }
                                   , Total = 1
                               });

            _bookDispatchService = new BookServiceDispatcher(_bookServiceEndPointsMock.Object, _dispatcherMock.Object);

            var actual = _bookDispatchService.RouteToBookServiceGet(1, 1);

            Assert.Equal(1, actual.Total);
        }

        [Fact]
        public void WhenUrlIsNotNull_ThrowException()
        {
            _bookServiceEndPointsMock.Setup(t => t.Value).Returns(() => new BookServiceEndPointConstants {Get = ""});

            _bookDispatchService = new BookServiceDispatcher(_bookServiceEndPointsMock.Object, _dispatcherMock.Object);

            Assert.Throws<InvalidOperationException>(() =>
                                                         _bookDispatchService.RouteToBookServiceGet(
                                                             new BookDispatcherRequest
                                                             {
                                                                 UserId = It.IsAny<int>(), Limit = It.IsAny<int>()
                                                                 , Offset = It.IsAny<int>()
                                                             }));
        }
    }
}