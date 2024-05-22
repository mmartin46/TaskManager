using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagerGUI.Data;
using TaskManagerGUI.Models;
using TaskManagerGUI.Constants;

namespace TaskManagerGUI.Repositories
{
    public class SelectCompanyRepository : ISelectCompanyRepository
    {
        private readonly SelectCompanyContext _context = null;
        public SelectCompanyRepository(SelectCompanyContext context)
        {
            _context = context;
        }

        public async Task<List<SelectCompanyModel>> GetCompanySymbols()
        {
            List<SelectCompanyModel> companiesToAppend = new List<SelectCompanyModel>();
            var companiesFromDatabase = await _context.Companies.ToListAsync();
            if (companiesFromDatabase != null || companiesFromDatabase.Count > 0)
            {
                foreach (var company in companiesFromDatabase)
                {
                    if (companiesToAppend.Where(x => x.Name.Equals(company.Name)).Count() < 1)
                    {
                        companiesToAppend.Add(new SelectCompanyModel()
                        {
                            Id = company.Id,
                            Symbol = company.Symbol,
                            Name = company.Name
                        });
                    }
                }
            }
            return companiesToAppend;
        }

        public async Task<List<SelectListItem>> GetCompanySelectListItems()
        {
            List<SelectListItem> companyNames = new List<SelectListItem>();
            foreach (SelectCompanyModel company in await GetCompanySymbols())
            {
                companyNames.Add(new SelectListItem() { Value=company.Symbol, Text=company.Name });
            }
            return companyNames.Take(ValueConstants.MaxCompanyNames).ToList();
        }

    }
}
