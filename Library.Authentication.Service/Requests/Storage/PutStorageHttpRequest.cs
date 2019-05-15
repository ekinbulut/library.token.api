using System.Diagnostics.CodeAnalysis;

namespace Library.Authentication.Service.Requests.Storage
{
    [ExcludeFromCodeCoverage]
    public class PutStorageHttpRequest
    {
        public string Name { get; set; }
        public int RackNumber { get; set; }
    }
}