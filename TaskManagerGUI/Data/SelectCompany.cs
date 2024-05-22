using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TaskManagerGUI.Data
{
    public class SelectCompany
    {
        [Key]
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string reportDate { get; set; }
        public string fiscalDate { get; set; }
        public string Estimate {  get; set; }
        public string Currency { get; set; }
    }
}
