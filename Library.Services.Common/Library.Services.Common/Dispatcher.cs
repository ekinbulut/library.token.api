using System;
using System.Collections.Generic;
using System.Net;
using Library.Services.Common.Enums;
using Library.Services.Common.Resources;
using Newtonsoft.Json;
using RestSharp;

namespace Library.Services.Common
{
    public class Dispatcher : IDispatcher
    {
        private readonly IRestClient _restClient;
        private readonly IRestRequest _restRequest;

        public Dispatcher(IRestClient restClient, IRestRequest restRequest)
        {
            _restClient = restClient;
            _restRequest = restRequest;
        }

        public TResponse Dispatch<TRequest, TResponse>(TRequest request, string baseUrl, string resource,
                                                       HttpRequestCode httpRequest
                                                       , Dictionary<string, object> queryParameter,
                                                       Dictionary<string, object> headers = null)
            where TResponse : class, new()
        {
            _restRequest.Parameters?.Clear();

            _restRequest.Method = (Method) httpRequest;

            _restClient.BaseUrl = new Uri(baseUrl);

            if (headers != null)
                foreach (var item in headers)
                    _restRequest.AddHeader(item.Key, item.Value.ToString());

            if (request != null) _restRequest.AddJsonBody(request);

            if (queryParameter != null && queryParameter.Count > 0)
                foreach (var item in queryParameter)
                    _restRequest.AddQueryParameter(item.Key, item.Value.ToString());

            var response = _restClient.Execute<TResponse>(_restRequest);

            if (HttpStatusCode.OK != response.StatusCode && HttpStatusCode.Created != response.StatusCode &&
                HttpStatusCode.Accepted != response.StatusCode)
                throw new InvalidOperationException(ErrorResources.RestClientInvalidOperation);

            var content = JsonConvert.DeserializeObject<TResponse>(response.Content);

            return content;
        }
    }
}