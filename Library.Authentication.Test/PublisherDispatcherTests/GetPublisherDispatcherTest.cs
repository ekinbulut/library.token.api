using System;
using System.Collections.Generic;
using System.Linq;
using Library.Authentication.Service.Dispatchers.Publisher;
using Library.Authentication.Service.Requests.Publisher;
using Library.Authentication.Service.Responses.Publisher;
using Library.Authentication.Service.ServiceModels.Publisher;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.PublisherDispatcherTests
{
    public class GetPublisherDispatcherTest
    {
        private readonly Mock<IOptions<PublisherServiceEndPointConstants>> _optionsMock;
        private readonly PublisherServiceDispatcher _sut;
        
        public GetPublisherDispatcherTest()
        {
            var dispatcherMock = new Mock<IDispatcher>();
            _optionsMock = new Mock<IOptions<PublisherServiceEndPointConstants>>();
            
            _optionsMock.Setup(t => t.Value).Returns(() => new PublisherServiceEndPointConstants
            {
                Get = "http://api.publisher.com/api/publisher"
            });
            
            dispatcherMock.Setup(t =>
                    t.Dispatch<object, GetPublisherDispatcherResponse>(
                        null, It.IsAny<string>(), null, HttpRequestCode.GET,
                        It.IsAny<Dictionary<string, object>>(), null))
                .Returns(() => new GetPublisherDispatcherResponse
                {
                    Total = 1,
                    Publishers = new List<PublisherModel>()
                                 {
                                     new PublisherModel()
                                     {
                                         Name = "name",
                                         Series = new List<string>()
                                                  {
                                                      "serie",
                                                      "serie"
                                                  }
                                         ,Id = "5c9f2f04f1d4914a40f7580c"
                                     }
                                 }
                });
            
            _sut = new PublisherServiceDispatcher(dispatcherMock.Object, _optionsMock.Object);
        }

        [Fact]
        public void When_PublisherServiceEndPointConstantsGet_IsNull_ThrowsInvalidOperationException()
        {
            _optionsMock.Setup(t => t.Value).Returns(() => new PublisherServiceEndPointConstants {Get = ""});

            Assert.Throws<InvalidOperationException>(() => _sut.RouteToPublisher("5c9f2f04f1d4914a40f7580c"));
        }

        [Fact]
        public void When_RouteToPublisherGet_Returns_Valid_Response()
        {


            var actual = _sut.RouteToPublishers( new GetPublisherDispatcherRequest
                                                                            {
                                                                                Limit = 10, Offset = 0
                                                                            });

            Assert.Single(actual.Publishers);
            var publisherModel = actual.Publishers.FirstOrDefault();
            
            Assert.Equal(1, actual.Total);
            Assert.Equal("5c9f2f04f1d4914a40f7580c", publisherModel.Id);
            Assert.Equal("name", publisherModel.Name);
            Assert.NotNull(publisherModel.Series);

        }

        [Fact]
        public void When_RouteToPublisherGetById_Returns_Valid_Response()
        {

            var actual = _sut.RouteToPublisher("5c9f2f04f1d4914a40f7580c");

            Assert.Single(actual.Publishers);
        }
        
        
    }
}