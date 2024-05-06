using System.ComponentModel.DataAnnotations;

namespace TaskManagerGUI.Models
{
    public class MemoryModel
    {
        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public double MegaBytes { get; set; }
    }
}
