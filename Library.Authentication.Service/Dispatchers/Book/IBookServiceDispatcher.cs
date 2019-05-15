using Library.Authentication.Service.Requests;
using Library.Authentication.Service.Responses;

namespace Library.Authentication.Service.Dispatchers.Book
{
    public interface IBookServiceDispatcher
    {
        BookDispatcherResponse RouteToBookServiceGet(BookDispatcherRequest request);
        BookDispatcherResponse RouteToBookServiceGet(int userId, int bookId);
        void RouteToBookServicePost(PostBookDispatcherRequest request);
        void RouteToBookServiceDelete(int userId, int bookId);
        void RouteToBookServicePut(PutBookDispatcherRequest request);
    }
}