using DataTrans.Application.Services.Implementation;
using DataTrans.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using OnMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Application.Extensions
{
    public static class AddApplicationExtensision
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITeacherSubjectService, TeacherSubjectService>();
            services.AddScoped<OnMapping>();
            return services;
        }
    }
}
