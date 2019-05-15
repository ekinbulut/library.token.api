using System.Diagnostics.CodeAnalysis;

namespace Library.Authentication.Service.Requests.Storage
{
    [ExcludeFromCodeCoverage]
    public class GetStorageHttpRequest
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}