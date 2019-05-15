using System.Collections.Generic;
using Library.Authentication.Service.ServiceModels.Storage;

namespace Library.Authentication.Service.Responses.Storage
{
    public class StorageServiceResponse
    {
        public int Total { get; set; }
        public IEnumerable<StorageModel> StorageCollection { get; set; }
    }
}