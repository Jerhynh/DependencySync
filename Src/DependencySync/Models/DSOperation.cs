namespace DependencySync.Models
{
    class DSOperation
    {
        public List<string> Dependencies = new();
        public string DeliveryPath { get; set; } = string.Empty;
    }
}
