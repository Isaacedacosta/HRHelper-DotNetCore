using HRHelper.Application.Interface;
using HRHelper.Application.Services;
using HRHelper.Data.Repositories;
using HRHelper.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRHelper.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {

            #region Services
            services.AddScoped<IUserService, UserService>();
            #endregion


            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>(); 
            #endregion

        }
    }
}
