using System.ComponentModel.DataAnnotations;

namespace TaskManagerGUI.Models
{
    public class DiskModel
    {
        [Required]
        public string FriendlyName { get; set; }
        [Required]
        public string HealthStatus { get; set; }
        public string OperationalStatus { get; set; }
        public double TotalSize { get; set; }
        public string PartitionStyle { get; set; }
        public PartitionModel Partition { get; set; }
    }
}
