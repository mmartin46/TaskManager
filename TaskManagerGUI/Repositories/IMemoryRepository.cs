using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public interface IMemoryRepository
    {
        Task<MemoryModel> GetMemoryModelAsync();
    }
}