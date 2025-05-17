using DataTrans.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Domain.Entities
{
    public class SystemUser : IdentityUser
    {

        public string Name;

        //public string NAME
        //{
        //    get
        //    {
        //        return Name;
        //    }
        //    set
        //    {
        //        Name = value;
        //    }
        //}


        public string ImageUrl { get; set; }
        public string RefreshToken { get; set; }
        public string NoneHashedPassword { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
