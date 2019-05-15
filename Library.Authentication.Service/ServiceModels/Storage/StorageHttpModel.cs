using System.Diagnostics.CodeAnalysis;

namespace Library.Authentication.Service.ServiceModels.Storage
{
    [ExcludeFromCodeCoverage]
    public class StorageHttpModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int[] RackNumber { get; set; }
    }
}