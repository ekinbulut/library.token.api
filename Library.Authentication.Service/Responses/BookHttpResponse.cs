using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Library.Authentication.Service.ServiceModels.Books;

namespace Library.Authentication.Service.Responses
{
    [ExcludeFromCodeCoverage]
    public class BookHttpResponse
    {
        public IEnumerable<BookHttpModel> Books;
        public int Total => Books.Count();
    }
}