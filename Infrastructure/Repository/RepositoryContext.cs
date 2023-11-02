using Microsoft.EntityFrameworkCore;
using KanbanBoard.Api.Domain.Models;

namespace KanbanBoard.Api.Infrastructure.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
        {

        }

        public DbSet<Card> Card { get; set; }

    }
}