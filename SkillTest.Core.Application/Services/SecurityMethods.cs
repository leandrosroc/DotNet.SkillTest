using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SkillTest.Core.Application.Configuration;

namespace SkillTest.Core.Application.Services
{
    // Gerenciamento de CORS (Cross-Origin Resource Sharing) e JWT (JSON Web Tokens).
    public static class SecurityMethods
    {
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";
        public static void AddCusumCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(option =>
            {
                option.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    // ou por hard IP ou via appsetting.json
                    //builder.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod();
                    // ou via arquivo conf de configurações do aplicativo
                    //builder.WithOrigins(configuration["Cors:Origin"]).AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        public static void addCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            // opção para recuperar a chave Jwt por opção e por injeção
            services.Configure<SecurityOption>(configuration.GetSection("Jwt"));
        }

        public static void AddCustomJWT(this IServiceCollection services, IConfiguration configuration)
        {
            SecurityOption secutityOption = new();
            configuration.GetSection("Jwt").Bind(secutityOption);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                //string chave = config["Jwt:Key"]; recuperação direta de configurações ou arquivos secretos se você não passar pela configuração -> SecutityOption
                string key = secutityOption.Key;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {

                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)),

                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true,

                };
            });
        }


    }
}
