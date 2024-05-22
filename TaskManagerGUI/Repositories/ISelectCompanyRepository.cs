using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public interface ISelectCompanyRepository
    {
        Task<List<SelectCompanyModel>> GetCompanySymbols();
    }
}