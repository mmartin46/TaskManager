using System.ComponentModel.DataAnnotations;

namespace TaskManagerGUI.Data
{
    public class Services
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public string DisplayName { get; set; }
    }
}
