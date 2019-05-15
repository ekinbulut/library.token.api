using System.Diagnostics.CodeAnalysis;

namespace Library.Authentication.Service.Requests
{
    [ExcludeFromCodeCoverage]
    public class BookHttpRequest
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}