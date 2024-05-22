using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public interface ISelectCompanyRepository
    {
        Task<List<SelectCompanyModel>> GetCompanySymbols();
        Task<List<SelectListItem>> GetCompanySelectListItems();
    }
}