using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using Library.Authentication.Service.Dispatchers.Storage;
using Library.Authentication.Service.Requests.Storage;
using Library.Authentication.Service.Responses.Storage;
using Library.Authentication.Service.ServiceModels.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Authentication.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/storage")]
    [Authorize]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageServiceDispatcher _storageServiceDispatcher;

        public StorageController(IStorageServiceDispatcher storageServiceDispatcher)
        {
            _storageServiceDispatcher = storageServiceDispatcher;
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(StorageHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] GetStorageHttpRequest request)
        {
            var response = _storageServiceDispatcher.RouteToStorageGet(new StorageServiceRequest
                                                                       {
                                                                           Offset = request.Offset
                                                                           , Limit = request.Limit
                                                                       });

            var httpResponse = new StorageHttpResponse
                               {
                                   TotalCount = response.Total, Storages = response.StorageCollection.Select(
                                       t => new StorageHttpModel
                                            {
                                                Id = t.Id, Name = t.Name, RackNumber = t.RackNumber
                                            })
                               };

            return StatusCode((int) HttpStatusCode.OK, httpResponse);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(StorageHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromRoute] string id)
        {
            var response = _storageServiceDispatcher.RouteToStorageGet(id);

            var httpResponse = new StorageHttpResponse
                               {
                                   TotalCount = response.Total, Storages = response.StorageCollection.Select(
                                       t => new StorageHttpModel
                                            {
                                                Id = t.Id, Name = t.Name, RackNumber = t.RackNumber
                                            })
                               };

            return StatusCode((int) HttpStatusCode.OK, httpResponse);
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        public IActionResult Put([FromRoute] string id, [FromBody] PutStorageHttpRequest request)
        {
            _storageServiceDispatcher.RouteToStoragePut(new PutStorageServiceRequest
                                                        {
                                                            Id = id, Name = request.Name
                                                            , RackNumber = request.RackNumber
                                                        });


            return StatusCode((int) HttpStatusCode.Accepted);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public IActionResult Post([FromBody] PostStorageHttpRequest request)
        {
            _storageServiceDispatcher.RouteToStoragePost(new PostStorageServiceRequest
                                                         {
                                                             Name = request.Name, RackNumber = request.RackNumber
                                                         });


            return StatusCode((int) HttpStatusCode.Created);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public IActionResult Delete([FromRoute] string id)
        {
            _storageServiceDispatcher.RouteToStorageDelete(id);

            return StatusCode((int) HttpStatusCode.OK);
        }
    }
}