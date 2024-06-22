﻿using Microsoft.EntityFrameworkCore;
using TaskManagerGUI.Data;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext = null;
        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<List<LoginModel>> Get()
        {
            List<LoginModel> allUsers = new List<LoginModel>();
            var users = await _userContext.Users.ToListAsync();

            if (users.Count == 0)
            {
                return new List<LoginModel>();
            }

            foreach (var user in users)
            {
                allUsers.Add
                (
                    new LoginModel
                    {
                        Username = user.Username,
                        Password = user.Password,
                        Email = user.Email
                    }
                );
            }
            return allUsers;
        }



        public async Task Add(RegisterModel? userModel)
        {
            bool userExists = false;
            if (userModel == null)
            {
                return;
            }

            Logins user = new Logins
            {
                Username = userModel.Username,
                Password = userModel.Password,
                Email = userModel.Email
            };

            userExists = _userContext.Users.Where(user => user.Username == userModel.Username).Any();
            if (!userExists)
            {
                await _userContext.Users.AddAsync(user);
                await _userContext.SaveChangesAsync();
            }
        }
    }
}
