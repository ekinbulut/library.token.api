using Library.Authentication.Service.Requests.Storage;
using Library.Authentication.Service.Responses.Storage;

namespace Library.Authentication.Service.Dispatchers.Storage
{
    public interface IStorageServiceDispatcher
    {
        StorageServiceResponse RouteToStorageGet(StorageServiceRequest request);
        StorageServiceResponse RouteToStorageGet(string id);
        void RouteToStoragePost(PostStorageServiceRequest request);
        void RouteToStoragePut(PutStorageServiceRequest request);
        void RouteToStorageDelete(string id);
    }
}