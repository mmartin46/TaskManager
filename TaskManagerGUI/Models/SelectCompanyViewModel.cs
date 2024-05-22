using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManagerGUI.Models
{
    public class SelectCompanyViewModel
    {
        public string SelectedCompany { get; set; }
        public List<SelectListItem> Values { get; set; }
    }
}
