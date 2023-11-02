using System.Text;
using KanbanBoard.Api.Infrastructure.Configuration;
using KanbanBoard.Api.Utils.Configurations;
using KanbanBoard.Api.Utils.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace KanbanBoard.Api
{
    public class Startup
    {
        public IConfigurationRoot _configuration { get; }

        public Startup(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            InMemoryConfiguration.ConfigureSqlContext(services, _configuration);
            ConfigutationManager.ConfigureServicesManager(services, _configuration);

            KanbanBoardConfig.JwtConfig(_configuration);

            services.AddControllers();
            services.AddAuthorization();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(opt =>
            {
                //opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Description = "Please enter token"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = KanbanBoardConfig.JwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = KanbanBoardConfig.JwtAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KanbanBoardConfig.JwtSecretKey)) // Replace with your secret key
                };
            });


            services.AddAutoMapper(typeof(KanbanBoardProfile));

        }
        public void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.MapControllers();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<JwtTokenValidationMiddleware>();
        }
    }
}