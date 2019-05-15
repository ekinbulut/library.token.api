using Library.Authentication.Service.Requests.Publisher;
using Library.Authentication.Service.Responses.Publisher;

namespace Library.Authentication.Service.Dispatchers.Publisher
{
    public interface IPublisherServiceDispatcher
    {
        GetPublisherDispatcherResponse RouteToPublishers(GetPublisherDispatcherRequest request);
        GetPublisherDispatcherResponse RouteToPublisher(string id);
        void RouteToPublisherPost(PostPublisherDispatcherRequest request);
        void RouteToPublisherPut(PutPublisherDispatcherRequest request);
        void RouteToPublisherDelete(string id);
    }
}