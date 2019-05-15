using System.Collections.Generic;
using Library.Authentication.Service.ServiceModels.Authors;

namespace Library.Authentication.Service.Responses.Authors
{
    public class GetAuthorDispatcherResponse
    {
        public IEnumerable<AuthorModel> Authors;
        public int? Total { get; set; }
    }
}