using Microsoft.EntityFrameworkCore;
using TglApi.Models;

namespace TglApi.Services;

public class TglApiDbContext : DbContext
{
    public TglApiDbContext(DbContextOptions<TglApiDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
}