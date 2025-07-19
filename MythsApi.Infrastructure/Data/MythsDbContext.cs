using Microsoft.EntityFrameworkCore;
using MythsApi.Core.Entities;

namespace MythsApi.Infrastructure.Data
{
    public class MythsDbContext : DbContext
    {
        public MythsDbContext(DbContextOptions<MythsDbContext> options) : base(options) { }

        public DbSet<Myth> Myths => Set<Myth>();
        public DbSet<Deity> Deities => Set<Deity>();
        public DbSet<Pantheon> Pantheons => Set<Pantheon>();
    }
}
