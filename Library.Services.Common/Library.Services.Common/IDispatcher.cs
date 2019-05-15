using System.Collections.Generic;
using Library.Services.Common.Enums;

namespace Library.Services.Common
{
    public interface IDispatcher
    {
        TResponse Dispatch<TRequest, TResponse>(TRequest request, string baseUrl, string resource
                                                , HttpRequestCode httpRequest, Dictionary<string, object> queryParameter
                                                , Dictionary<string, object> headers = null)
            where TResponse : class, new();
    }
}