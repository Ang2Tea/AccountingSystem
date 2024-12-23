using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AccountingSystem.EntityFramework.EntityFrameworkCore;

public class AccountingSystemDbContextFactory 
    : IDesignTimeDbContextFactory<AccountingSystemDbContext>
{
    private const string ConnectionString = "AccountingSystem";
    
    public AccountingSystemDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AccountingSystemDbContext>()
            .UseNpgsql(
                configuration.GetConnectionString(ConnectionString),
                b =>
                {
                    b.MigrationsHistoryTable("__AccountingSystem_Migrations");
                });

        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return new AccountingSystemDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AccountingSystem.Web2/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
  
}