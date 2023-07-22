using System.Reflection;
using MediatR;
using SkillTest.Core.Application.Services;
using SkillTest.Core.Application.Services.Impl;
using SkillTest.Core.Domain.Repositories.Framwork;
using SkillTest.Core.Infrastructures.Repository.Framwork;
using SkillTest.Core.Repositories;
using SkillTest.Core.Infrastructures.Repository;

namespace SkillTest.API.UI.Extentions
{
    public static class DependencyInjection
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddInjection(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.Load("SkillTest.Core.Application"), Assembly.Load("SkillTest.Core.Domain"), Assembly.Load("SkillTest.API.UI"));
            services.AddAutoMapper(Assembly.Load("SkillTest.Core.Application"));

            // Service
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
