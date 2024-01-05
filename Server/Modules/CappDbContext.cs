using Microsoft.EntityFrameworkCore;
using Server.Modules.Core.Models;

namespace Server.Modules;

public class CappDbContext : DbContext
{
    public CappDbContext(DbContextOptions<CappDbContext> options) : base(options)
    {
    }

    public DbSet<Part> Parts { get; set; } = null!;
    public DbSet<Procedure> Procedures { get; set; } = null!;
    public DbSet<Step> Steps { get; set; } = null!;
}