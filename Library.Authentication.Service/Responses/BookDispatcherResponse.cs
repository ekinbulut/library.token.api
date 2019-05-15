using System.Collections.Generic;
using Library.Authentication.Service.ServiceModels.Books;

namespace Library.Authentication.Service.Responses
{
    public class BookDispatcherResponse
    {
        public IEnumerable<BookModel> Books;
        public int Total { get; set; }
    }
}