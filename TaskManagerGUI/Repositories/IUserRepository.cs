using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Add(RegisterModel? userModel);
        Task<List<LoginModel>> Get();
    }
}