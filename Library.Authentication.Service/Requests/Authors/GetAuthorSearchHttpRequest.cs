using System.Diagnostics.CodeAnalysis;

namespace Library.Authentication.Service.Requests.Authors
{
    [ExcludeFromCodeCoverage]
    public class GetAuthorSearchHttpRequest
    {
        public string SearchKey { get; set; }
    }
}