using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using Library.Authentication.Service.Dispatchers.Publisher;
using Library.Authentication.Service.Requests.Authors;
using Library.Authentication.Service.Requests.Publisher;
using Library.Authentication.Service.Responses.Publisher;
using Library.Authentication.Service.ServiceModels.Publisher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Authentication.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/publishers")]
    [Authorize]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherServiceDispatcher _publisherServiceDispatcher;

        public PublisherController(IPublisherServiceDispatcher publisherServiceDispatcher)
        {
            _publisherServiceDispatcher = publisherServiceDispatcher;
        }
        
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(GetPublisherHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] GetAuthorHttpRequest request)
        {
            var dispatcherResponse = _publisherServiceDispatcher.RouteToPublishers(new GetPublisherDispatcherRequest()
                                                                             {
                                                                                 Limit = request.Limit,
                                                                                 Offset = request.Offset
                                                                             });

            var response = new GetPublisherHttpResponse
                           {
                               Total = dispatcherResponse.Total, Publishers = dispatcherResponse.Publishers.Select(
                                   t => new PublisherModel
                                        {
                                            Id = t.Id, Name = t.Name , Series = t.Series
                                        })
                           };

            return StatusCode((int) HttpStatusCode.OK, response);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(GetPublisherHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromRoute] string id)
        {
            var dispatcherResponse = _publisherServiceDispatcher.RouteToPublisher(id);

            var response = new GetPublisherHttpResponse
                           {
                               Total = dispatcherResponse.Total, Publishers = dispatcherResponse.Publishers.Select(
                                   t => new PublisherModel
                                        {
                                            Id = t.Id, Name = t.Name , Series = t.Series
                                        })
                           };

            return StatusCode((int) HttpStatusCode.OK, response);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public IActionResult Post([FromBody] PostPublisherHttpRequest request)
        {
            _publisherServiceDispatcher.RouteToPublisherPost(new PostPublisherDispatcherRequest
                                                     {
                                                         Name = request.Name, Series = request.Series
                                                     });

            return StatusCode((int) HttpStatusCode.Created);
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        public IActionResult Put([FromRoute] string id, [FromBody] PutPublisherHttpRequest request)
        {
            _publisherServiceDispatcher.RouteToPublisherPut(new PutPublisherDispatcherRequest
                                                    {
                                                        Id = id, Name = request.Name, Series = request.Series
                                                    });
            return StatusCode((int) HttpStatusCode.Accepted);
        }
        
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public IActionResult Delete([FromRoute] string id)
        {
            _publisherServiceDispatcher.RouteToPublisherDelete(id);
            return StatusCode((int) HttpStatusCode.OK);
        }
    }
}