using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using Library.Authentication.Service.Dispatchers.Book;
using Library.Authentication.Service.Extentions;
using Library.Authentication.Service.Requests;
using Library.Authentication.Service.Responses;
using Library.Authentication.Service.ServiceModels.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Authentication.Api.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]

    [ExcludeFromCodeCoverage]
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookServiceDispatcher _bookDispatcher;

        public BookController(IBookServiceDispatcher bookDispatcher)
        {
            _bookDispatcher = bookDispatcher;
        }

        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [ProducesResponseType(typeof(BookHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] BookHttpRequest request)
        {
            var user = HttpContext.User.ExctractClaims();

            var dispatcherResponse = _bookDispatcher.RouteToBookServiceGet(new BookDispatcherRequest
            {
                UserId = user.Id, Offset = request.Offset, Limit = request.Limit
            });

            var response = new BookHttpResponse
            {
                Books = dispatcherResponse.Books.Select(t => new BookHttpModel
                {
                    Id = t.Id, Name = t.Name, AuthorId = t.AuthorId, PublisherId = t.PublisherId,
                    PublishDate = t.PublishDate, Tag = t.Tag, No = t.No, SkinType = t.SkinType, LibraryId = t.LibraryId,
                    ShelfId = t.ShelfId
                })
            };

            return StatusCode((int) HttpStatusCode.OK, response);
        }

        [Route("{id}")]
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(BookHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromRoute] int id)
        {
            var user = HttpContext.User.ExctractClaims();

            var dispatcherResponse = _bookDispatcher.RouteToBookServiceGet(user.Id, id);

            var response = new BookHttpResponse
            {
                Books = dispatcherResponse.Books.Select(t => new BookHttpModel
                {
                    Id = t.Id, Name = t.Name, AuthorId = t.AuthorId, PublisherId = t.PublisherId,
                    PublishDate = t.PublishDate, Tag = t.Tag, No = t.No, SkinType = t.SkinType, LibraryId = t.LibraryId,
                    ShelfId = t.ShelfId
                })
            };

            return StatusCode((int) HttpStatusCode.OK, response);
        }

        [Route("{id}")]
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromRoute] int id, [FromBody] PutBookHttpRequest request)
        {
            var user = HttpContext.User.ExctractClaims();

            _bookDispatcher.RouteToBookServicePut(new PutBookDispatcherRequest
            {
                Id = id, UserId = user.Id, Name = request.Name, AuthorId = request.AuthorId,
                LibraryId = request.LibraryId, No = request.No, PublisherDate = request.PublisherDate,
                PublisherId = request.PublisherId, SkinType = request.SkinType, Tag = request.Tag,
                ShelfId = request.ShelfId
            });


            return StatusCode((int) HttpStatusCode.Accepted);
        }


        [Route("")]
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] PostBookHttpRequest request)
        {
            var user = HttpContext.User.ExctractClaims();

            _bookDispatcher.RouteToBookServicePost(new PostBookDispatcherRequest
            {
                UserId = user.Id, Name = request.Name, AuthorId = request.AuthorId, LibraryId = request.LibraryId,
                No = request.No, PublisherDate = request.PublisherDate, PublisherId = request.PublisherId,
                SkinType = request.SkinType, Tag = request.Tag, ShelfId = request.ShelfId
            });


            return StatusCode((int) HttpStatusCode.Created);
        }

        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public IActionResult Delete([FromRoute] int id)
        {
            var user = HttpContext.User.ExctractClaims();

            _bookDispatcher.RouteToBookServiceDelete(user.Id, id);

            return StatusCode((int) HttpStatusCode.Created);
        }
    }
}