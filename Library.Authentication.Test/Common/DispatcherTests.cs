using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Library.Services.Common;
using Library.Services.Common.Enums;
using Moq;
using RestSharp;
using Xunit;

namespace Library.Authentication.Test.Common
{
    public class DispatcherTests
    {
        public DispatcherTests()
        {
            _restClientMock = new Mock<IRestClient>();
            _restRequestMock = new Mock<IRestRequest>();
        }

        private readonly Mock<IRestClient> _restClientMock;
        private readonly Mock<IRestRequest> _restRequestMock;
        private Dispatcher _dispatcher;

        [Fact]
        public void When_DispatchResponse_ThrowInvalidOperationException()
        {
            _restClientMock.Setup(t => t.Execute<object>(_restRequestMock.Object))
                .Returns(() => new RestResponse<object>());

            _dispatcher = new Dispatcher(_restClientMock.Object, _restRequestMock.Object);

            Assert.Throws<InvalidOperationException>(() => _dispatcher.Dispatch<object, object>(new object(),
                                                                                                "http://api.com", ""
                                                                                                , HttpRequestCode.GET,
                                                                                                new Dictionary<string,
                                                                                                    object>()
                                                                                                , new Dictionary<string,
                                                                                                    object>()));
        }

        [Fact]
        public void When_DispatchResponse_WithHeadersAndQueryParams_ReturnsOK()
        {
            _restClientMock.Setup(t => t.Execute<object>(_restRequestMock.Object)).Returns(() =>
                                                                                               new RestResponse<object>
                                                                                               {
                                                                                                   StatusCode =
                                                                                                       HttpStatusCode.OK
                                                                                               });

            _dispatcher = new Dispatcher(_restClientMock.Object, _restRequestMock.Object);

            _dispatcher.Dispatch<object, object>(new object(), "http://api.com", "",
                                                 HttpRequestCode.GET,
                                                 new Dictionary<string, object> { { "params", "param" } },
                                                 new Dictionary<string, object> { { "header", "header" } });
        }

        [Fact]
        public void When_DispatchResponse_WithNullHeaders_ReturnsOK()
        {
            _restClientMock.Setup(t => t.Execute<object>(_restRequestMock.Object))
                .Returns(() => new RestResponse<object> { StatusCode = HttpStatusCode.OK });

            _dispatcher = new Dispatcher(_restClientMock.Object, _restRequestMock.Object);

            _dispatcher.Dispatch<object, object>(new object(), "http://api.com", "",
                                                 HttpRequestCode.GET
                                                 , new Dictionary<string, object> { { "param1", 1 } });
        }

        [Fact]
        public void When_DispatchResponse_WithNullQueryParams_ReturnsOK()
        {
            _restClientMock.Setup(t => t.Execute<object>(_restRequestMock.Object)).Returns(() =>
                                                                                               new RestResponse<object>
                                                                                               {
                                                                                                   StatusCode =
                                                                                                       HttpStatusCode.OK
                                                                                               });

            _dispatcher = new Dispatcher(_restClientMock.Object, _restRequestMock.Object);

            _dispatcher.Dispatch<object, object>(new object(), "http://api.com", "",
                                                 HttpRequestCode.GET, null,
                                                 new Dictionary<string, object> { { "header", "head" } });
        }

        [Fact]
        public void When_DispatchResponse_WithNullRequestObject_ReturnsOK()
        {
            _restClientMock.Setup(t => t.Execute<object>(_restRequestMock.Object)).Returns(() =>
                                                                                               new RestResponse<object>
                                                                                               {
                                                                                                   StatusCode =
                                                                                                       HttpStatusCode.OK
                                                                                               });

            _dispatcher = new Dispatcher(_restClientMock.Object, _restRequestMock.Object);

            _dispatcher.Dispatch<object, object>(null, "http://api.com", "",
                                                 HttpRequestCode.GET,
                                                 new Dictionary<string, object> { { "params", "param" } },
                                                 new Dictionary<string, object> { { "header", "header" } });
        }

        [Fact]
        public void When_Request_ParametersIsnotNull_ClearParameters()
        {
            _restRequestMock.SetupGet(t => t.Parameters).Returns(() => new List<Parameter>());
            _restClientMock.Setup(t => t.Execute<object>(_restRequestMock.Object)).Returns(() =>
                                                                                               new RestResponse<object>
                                                                                               {
                                                                                                   StatusCode =
                                                                                                       HttpStatusCode.OK
                                                                                               });

            _dispatcher = new Dispatcher(_restClientMock.Object, _restRequestMock.Object);

            _dispatcher.Dispatch<object, object>(null, "http://api.com", "",
                                                 HttpRequestCode.GET,
                                                 new Dictionary<string, object> { { "params", "param" } },
                                                 new Dictionary<string, object> { { "header", "header" } });
        }
    }
}
