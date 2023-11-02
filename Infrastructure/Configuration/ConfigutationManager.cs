

using KanbanBoard.Api.Domain.Repository;
using KanbanBoard.Api.Utils.Security;
using OasTools.Domain.Services;

namespace KanbanBoard.Api.Infrastructure.Configuration
{
    public static class ConfigutationManager
    {
        public static void ConfigureServicesManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICardRepository, CardRepository>();

            services.AddTransient<IJwtToken, JwtToken>();
        }
    }
}