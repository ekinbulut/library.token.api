using System.Diagnostics.CodeAnalysis;

namespace Library.Authentication.Service.Requests.Storage
{
    
    [ExcludeFromCodeCoverage]
    public class PostStorageHttpRequest
    {
        public string Name { get; set; }
        public int RackNumber { get; set; }
    }
}