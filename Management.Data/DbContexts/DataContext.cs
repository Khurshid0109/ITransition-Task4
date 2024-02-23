using Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Management.Data.DbContexts;
public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
    }

    // Add your tables here
    public DbSet<User> Users { get; set; }
}
