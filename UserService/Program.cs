using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UserService.Abstraction;
using UserService.Repository;
using UserService.Security;
using UserService.Services;

namespace UserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                options =>
                {
                    options.AddSecurityDefinition(
                        JwtBearerDefaults.AuthenticationScheme,
                        new()
                        {
                            In = ParameterLocation.Header,
                            Description = "Enter JWT",
                            Name = "Authorization",
                            Type = SecuritySchemeType.Http,
                            BearerFormat = "JWT Token",
                            Scheme = JwtBearerDefaults.AuthenticationScheme
                        });
                    options.AddSecurityRequirement(
                        new()
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = JwtBearerDefaults.AuthenticationScheme
                                    }
                                },
                                new List<string>()
                            }

                        });
                });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            var jwt = builder.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>()
                      ?? throw new Exception("JwtConfiguration not found");
            builder.Services.AddSingleton(provider => jwt);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,

                    IssuerSigningKey = new RsaSecurityKey(RSATools.GetPublicKey())
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}