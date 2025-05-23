﻿using DataTrans.Application.Common.Handler;
using DataTrans.Application.Shared.Validations;

namespace DataTrans.Application.Dtos.Request
{
    public class LoginRequestDto : IEmailValidation, IPasswordValidation
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public async Task<Result<string>> Emailvalidate()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return await Result<string>.FaildAsync(false, "Please Enter a UserName");
            }
            
            return null;
        }

        public async Task<Result<string>> Passwordvalidate()
        {
            if (Password.Length is <= 6)
            {
                return await Result<string>.FaildAsync(false, "Password must be more than 6 characters");
            }

            return null;
        }
    }
}
