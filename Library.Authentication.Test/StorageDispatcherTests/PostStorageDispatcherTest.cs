using System;
using Library.Authentication.Service.Dispatchers.Storage;
using Library.Authentication.Service.Requests.Storage;
using Library.Authentication.Service.Settings;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Library.Authentication.Test.StorageDispatcherTests
{
    public class PostStorageDispatcherTest
    {
        private readonly Mock<IDispatcher>                               _dispatcherMock;
        private readonly Mock<IOptions<StorageServiceEndPointConstants>> _optionsMock;

        private readonly StorageServiceDispatcher _sut;

        public PostStorageDispatcherTest()
        {
            _dispatcherMock = new Mock<IDispatcher>();
            _optionsMock = new Mock<IOptions<StorageServiceEndPointConstants>>();

            _optionsMock.Setup(t => t.Value).Returns(new StorageServiceEndPointConstants
                                                     {
                                                         Post = "http://storage.librayos.io/api/storage"
                                                     });

            _dispatcherMock
                .Setup(s => s.Dispatch<object, object>(It.IsAny<object>(), It.IsAny<string>(), null, HttpRequestCode.POST
                                                       , null, null)).Verifiable();

            _sut = new StorageServiceDispatcher(_dispatcherMock.Object, _optionsMock.Object);
        }
        
        [Fact]
        public void If_OptionValue_IsNull_Throw_InvalidException()
        {
            _optionsMock.Setup(t => t.Value).Returns(new StorageServiceEndPointConstants());

            Assert.Throws<InvalidOperationException>(() => _sut.RouteToStoragePost(new PostStorageServiceRequest()
                                                                                  {
                                                                                       Name = "name"
                                                                                      , RackNumber = 1,
                                                                                  }));
        }

        [Fact]
        public void Verify_If_Operation_Completes()
        {
            _sut.RouteToStoragePost(new PostStorageServiceRequest()
                                   {
                                         Name = "name"
                                       , RackNumber = 1
                                   });
            _dispatcherMock.Verify(s => s.Dispatch<object, object>(It.IsAny<object>(), It.IsAny<string>(), null
                                                                   , HttpRequestCode.POST
                                                                   , null, null));
        }
    }
}