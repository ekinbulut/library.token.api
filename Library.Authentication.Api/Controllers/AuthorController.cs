using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using Library.Authentication.Service.Dispatchers.Author;
using Library.Authentication.Service.Requests.Authors;
using Library.Authentication.Service.Responses.Authors;
using Library.Authentication.Service.ServiceModels.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Authentication.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/authors")]
    [Authorize]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorServiceDispatcher _authorDispatchService;

        public AuthorController(IAuthorServiceDispatcher authorDispatchService)
        {
            _authorDispatchService = authorDispatchService;
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(GetAuthorHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] GetAuthorHttpRequest request)
        {
            var dispatcherResponse = _authorDispatchService.RouteToAuthorGet(new GetAuthorDispatcherRequest()
                                                                             {
                                                                                 Limit = request.Limit,
                                                                                 Offset = request.Offset
                                                                             });

            var response = new GetAuthorHttpResponse
                           {
                               Total = dispatcherResponse.Total, Authors = dispatcherResponse.Authors.Select(
                                   t => new AuthorHttpModel
                                        {
                                            Id = t.Id, Name = t.Name, Bio = t.Data.Bio, Birthday = t.Data.Birthday
                                            , Dead = t.Data.Dead, Location = t.Data.Location, ImageUrl = t.Data.ImageUrl
                                            , Books = t.Data.Books
                                        })
                           };

            return StatusCode((int) HttpStatusCode.OK, response);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(GetAuthorHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromRoute] string id)
        {
            var dispatcherResponse = _authorDispatchService.RouteToAuthorGet(id);

            var response = new GetAuthorHttpResponse
                           {
                               Total = dispatcherResponse.Total, Authors = dispatcherResponse.Authors.Select(
                                   t => new AuthorHttpModel
                                        {
                                            Id = t.Id, Name = t.Name, Bio = t.Data.Bio, Birthday = t.Data.Birthday
                                            , Dead = t.Data.Dead, Location = t.Data.Location, ImageUrl = t.Data.ImageUrl
                                            , Books = t.Data.Books
                                        })
                           };

            return StatusCode((int) HttpStatusCode.OK, response);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public IActionResult Post([FromBody] PostAuthorHttpRequest request)
        {
            _authorDispatchService.RouteToAuthorPost(new PostAuthorDispatcherRequest
                                                     {
                                                         Name = request.Name, Bio = request.Bio
                                                         , Birthday = request.Birthday, Dead = request.Dead
                                                         , Location = request.Location, ImageUrl = request.ImageUrl
                                                         , Books = request.Books
                                                     });

            return StatusCode((int) HttpStatusCode.Created);
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        public IActionResult Put([FromRoute] string id, [FromBody] PutAuthorHttpRequest request)
        {
            _authorDispatchService.RouteToAuthorPut(new PutAuthorDispatcherRequest
                                                    {
                                                        Id = id, Name = request.Name, Bio = request.Bio
                                                        , Birthday = request.Birthday, Dead = request.Dead
                                                        , Location = request.Location, ImageUrl = request.ImageUrl
                                                        , Books = request.Books
                                                    });
            return StatusCode((int) HttpStatusCode.Accepted);
        }

        [Route("search")]
        [HttpGet]
        [ProducesResponseType(typeof(GetAuthorHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] GetAuthorSearchHttpRequest request)
        {
            
            
            var dispatcherResponse = _authorDispatchService.RouteToAuthorGet(new GetAuthorSearchDispatcherRequest()
            {
                SearchKey = request.SearchKey
            });
            
            var response = new GetAuthorHttpResponse
            {
                Total = dispatcherResponse.Total, Authors = dispatcherResponse.Authors.Select(
                    t => new AuthorHttpModel
                    {
                        Id = t.Id, Name = t.Name, Bio = t.Data.Bio, Birthday = t.Data.Birthday
                        , Dead = t.Data.Dead, Location = t.Data.Location, ImageUrl = t.Data.ImageUrl
                        , Books = t.Data.Books
                    })
            };

            return StatusCode((int) HttpStatusCode.OK, response);
            
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public IActionResult Delete([FromRoute] string id)
        {
            _authorDispatchService.RouteToAuthorDelete(id);
            return StatusCode((int) HttpStatusCode.OK);
        }
    }
}