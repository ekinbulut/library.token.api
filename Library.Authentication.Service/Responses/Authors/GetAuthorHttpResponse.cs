using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Library.Authentication.Service.ServiceModels.Authors;

namespace Library.Authentication.Service.Responses.Authors
{
    [ExcludeFromCodeCoverage]
    public class GetAuthorHttpResponse
    {
        public IEnumerable<AuthorHttpModel> Authors = new List<AuthorHttpModel>();
        public int? Total { get; set; }
    }
}