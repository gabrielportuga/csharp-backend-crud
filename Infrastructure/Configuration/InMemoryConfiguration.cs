using Microsoft.EntityFrameworkCore;
using KanbanBoard.Api.Infrastructure.Repository;

namespace KanbanBoard.Api.Infrastructure.Configuration
{
    public static class InMemoryConfiguration
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseInMemoryDatabase("dealerDbMemory"));

    }
}