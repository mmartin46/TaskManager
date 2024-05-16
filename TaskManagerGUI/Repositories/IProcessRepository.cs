using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public interface IProcessRepository
    {
        Task<List<ProcessModel>> GetProcessesByNameAsync();
    }
}