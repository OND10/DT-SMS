﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Application.Dtos.Request
{
    public record RefreshTokenRequestDto(string AccessToken, string RefreshToken);
}
