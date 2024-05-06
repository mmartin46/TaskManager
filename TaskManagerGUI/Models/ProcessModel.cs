using System.ComponentModel.DataAnnotations;

namespace TaskManagerGUI.Models
{
    public class ProcessModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProcessName { get; set; }
        public double CPU { get; set; }
    }
}
