﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Application.Dtos.Response
{
    public class LoginResponseDto
    {
        private UserResponseDto user { get; set; }
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; }
        public List<string> Roles { get; set; }
        
        

        public UserResponseDto User
        {
            get
            {
                return new UserResponseDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    ImageUrl = user.ImageUrl,
                };
            }
            set
            {
                user = value;
            }
        }
    }
}
