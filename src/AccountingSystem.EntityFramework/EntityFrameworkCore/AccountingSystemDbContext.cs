using AccountingSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.EntityFramework.EntityFrameworkCore;

public class AccountingSystemDbContext(
    DbContextOptions<AccountingSystemDbContext> options
    ) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /* Include modules to your migration db context */

        modelBuilder.ConfigureInventoryService();
        /* Configure your own tables/entities inside here */
    }

}