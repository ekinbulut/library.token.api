using System;
using System.Collections.Generic;
using System.Linq;
using Library.Authentication.Service.Dispatchers.Storage;
using Library.Authentication.Service.Requests.Storage;
using Library.Authentication.Service.Responses.Storage;
using Library.Authentication.Service.ServiceModels.Storage;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.StorageDispatcherTests
{
    public class GetStorageDispatcherTest
    {
        private readonly Mock<IDispatcher>                               _dispatcherMock;
        private readonly Mock<IOptions<StorageServiceEndPointConstants>> _optionsMock;

        private readonly StorageServiceDispatcher _sut;

        public GetStorageDispatcherTest()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _optionsMock = new Mock<IOptions<StorageServiceEndPointConstants>>();

            _optionsMock.Setup(t => t.Value).Returns(new StorageServiceEndPointConstants
                                                     {
                                                         Get = "http://storage.librayos.io/api/storage"
                                                     });

            _dispatcherMock.Setup(s => s.Dispatch<object, StorageServiceResponse>(
                                      null, It.IsAny<string>(), null, HttpRequestCode.GET
                                      , It.IsAny<Dictionary<string, object>>(), null))
                           .Returns(() =>
                                        new StorageServiceResponse()
                                        {
                                            Total = 1, StorageCollection = new List<StorageModel>()
                                                                           {
                                                                               new StorageModel()
                                                                               {
                                                                                   Id = "id", Name = "name"
                                                                                   , RackNumber = null
                                                                               }
                                                                           }
                                        });
            _sut = new StorageServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);
        }

        [Fact]
        public void If_OptionValue_IsNull_Throw_InvalidException()
        {
            _optionsMock.Setup(t => t.Value).Returns(new StorageServiceEndPointConstants());

            Assert.Throws<InvalidOperationException>(() => _sut.RouteToStorageGet(new StorageServiceRequest()
                                                                                  {
                                                                                      Offset = 0, Limit = 10
                                                                                  }));
        }

        [Fact]
        public void If_Dispatcher_Returns_Response_Operation_Completes()
        {
            var actual = _sut.RouteToStorageGet(new StorageServiceRequest()
                                                {
                                                    Offset = 0, Limit = 10
                                                });
            var storageModel = actual.StorageCollection.FirstOrDefault();

            Assert.Equal(1, actual.Total);
            Assert.Equal("id", storageModel.Id);
            Assert.Equal("name", storageModel.Name);
            Assert.Null(storageModel.RackNumber);
        }
        
        [Fact]
        public void If_Dispatcher_Returns_Response_OperationById_Completes()
        {
            var actual = _sut.RouteToStorageGet("id");
            var storageModel = actual.StorageCollection.FirstOrDefault();

            Assert.Equal(1, actual.Total);
            Assert.Equal("id", storageModel.Id);
            Assert.Equal("name", storageModel.Name);
            Assert.Null(storageModel.RackNumber);
        }
        
        [Fact]
        public void If_OptionValue_IsNull_Throw_InvalidException_OperationById()
        {
            _optionsMock.Setup(t => t.Value).Returns(new StorageServiceEndPointConstants());

            Assert.Throws<InvalidOperationException>(() => _sut.RouteToStorageGet("id"));
        }
    }
}