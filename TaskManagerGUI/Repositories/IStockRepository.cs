using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public interface IStockRepository
    {
        Task<Dictionary<string, StockModel>> ProcessStockApi(string company, int minutes = 5);
    }
}