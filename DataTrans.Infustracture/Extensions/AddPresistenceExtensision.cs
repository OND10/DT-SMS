using DataTrans.Domain.Interfaces;
using DataTrans.Domain.Shared;
using DataTrans.Infustracture.Data;
using DataTrans.Infustracture.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace DataTrans.Infustracture.Extensions
{
    public static class AddPresistenceExtensision
    {
        public static IServiceCollection AddPresistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

          
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
         

            //services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return services;
        }

    }
}
