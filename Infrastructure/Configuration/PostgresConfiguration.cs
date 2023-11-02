using Microsoft.EntityFrameworkCore;
using KanbanBoard.Api.Infrastructure.Repository;

namespace KanbanBoard.Api.Infrastructure.Configuration
{
    public static class PostgresConfiguration
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseNpgsql(configuration.GetConnectionString("KanbanDbPostgres")));

    }
}