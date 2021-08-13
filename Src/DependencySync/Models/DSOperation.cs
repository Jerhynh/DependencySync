namespace DependencySync.Models
{
    class DSOperation
    {
        public List<string> Dependencies = new();
        public bool CopySubDirectories { get; set; } = true;
        public string DeliveryPath { get; set; } = string.Empty;
    }
}
