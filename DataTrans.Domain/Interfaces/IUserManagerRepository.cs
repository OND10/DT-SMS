﻿using DataTrans.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Domain.Interfaces
{
    public interface IUserManagerRepository
    {
        public Task<SystemUser> FindUserByEmailAsync(string email);
        public Task<bool> CheckUserPasswordAsync(SystemUser user, string password);
        public Task<IList<string>> GetUserRolesAsync(SystemUser user);
        public Task<IdentityResult> CreateUserAsync(SystemUser user, string password);
        public Task<IdentityResult> AddUserToRoleAsync(SystemUser user, string role);
        public Task<string> GenerateUserEmailConfirmationTokenAsync(SystemUser user);
        public Task<IdentityResult> ConfirmUserEmailAsync(SystemUser user, string token);
        public Task<SystemUser> FindUserByIdAsync(string userId);
        public Task<SystemUser> FindUserByNameAsync(string userName);
        public Task<bool> AssignRoleToUser(string email, string roleName);
        public Task<SystemUser> UpdateAsync(SystemUser user);
        public Task<IEnumerable<SystemUser>> GetAllAsync();
        public Task<SystemUser> GetByIdAsync(string Id);
        public Task<bool> DeleteAsync(string userId);
    }
}
