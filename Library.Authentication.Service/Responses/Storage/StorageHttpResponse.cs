using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Library.Authentication.Service.ServiceModels.Storage;

namespace Library.Authentication.Service.Responses.Storage
{
    [ExcludeFromCodeCoverage]
    public class StorageHttpResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<StorageHttpModel> Storages { get; set; }
    }
}