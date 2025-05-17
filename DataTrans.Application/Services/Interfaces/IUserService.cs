using DataTrans.Application.Common.Handler;
using DataTrans.Application.Dtos.Request;
using DataTrans.Application.Dtos.Response;
using DataTrans.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<LoginResponseDto>> Login(LoginRequestDto request);
        Task<Result<UserResponseDto>> Register(RegisterRequestDto request);
        Task<Result<bool>> AddUserToRole(UserRoleRequestDto request);
        Task<Result<string>> GenerateUserEmailConfirmationTokenAsync(SystemUser user);
        Task<Result<SystemUser>> FindUserByIdAsync(string userId);
        Task<Result<IdentityResult>> ConfirmUserEmailAsync(SystemUser user, string token);
        Task<Result<SystemUser>> FindUserByUsernameAsync(string username);
        Task<Result<IEnumerable<string>>> GetUserRolesAsync(SystemUser user);
        Task<Result<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<Result<IEnumerable<SystemUser>>> GetAllAsync();
        Task<Result<UserResponseDto>> GetByIdAsync(string Id);
        Task<Result<UserResponseDto>> UpdateAsync(string userId, UpdateUserRequestDto request);
        Task<Result<bool>> DeleteAsync(string userId);
    }
}
