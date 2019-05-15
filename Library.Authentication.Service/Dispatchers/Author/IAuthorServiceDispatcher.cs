using Library.Authentication.Service.Requests.Authors;
using Library.Authentication.Service.Responses.Authors;

namespace Library.Authentication.Service.Dispatchers.Author
{
    public interface IAuthorServiceDispatcher
    {
        GetAuthorDispatcherResponse RouteToAuthorGet(GetAuthorDispatcherRequest request);
        GetAuthorDispatcherResponse RouteToAuthorGet(string id);
        GetAuthorDispatcherResponse RouteToAuthorGet(GetAuthorSearchDispatcherRequest request);
        void RouteToAuthorPost(PostAuthorDispatcherRequest request);
        void RouteToAuthorPut(PutAuthorDispatcherRequest request);
        void RouteToAuthorDelete(string id);
    }
}