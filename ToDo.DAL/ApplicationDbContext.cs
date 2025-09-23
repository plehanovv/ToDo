using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ToDo.DAL.Interceptors;

namespace ToDo.DAL;

public class ApplicationDbContext : DbContext
{
    private readonly DateInterceptor _dateInterceptor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, DateInterceptor dateInterceptor)
        : base(options)
    {
        _dateInterceptor = dateInterceptor;
    }
    
    public async Task EnsureDatabaseMigratedAsync()
    {
        await Database.MigrateAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_dateInterceptor != null)
            optionsBuilder.AddInterceptors(_dateInterceptor);
    }
}