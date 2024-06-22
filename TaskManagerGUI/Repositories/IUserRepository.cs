﻿using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public interface IUserRepository
    {
        Task Add(RegisterModel? userModel);
        Task<List<LoginModel>> Get();
    }
}