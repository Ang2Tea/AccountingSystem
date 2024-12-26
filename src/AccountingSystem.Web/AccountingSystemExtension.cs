using System.Text.RegularExpressions;
using AccountingSystem.Application.Contract.ShopItems;
using AccountingSystem.Application.Contract.Transactions;
using AccountingSystem.Application.Contract.Users;
using AccountingSystem.Application.ShopItems;
using AccountingSystem.Application.Transactions;
using AccountingSystem.Application.Users;
using AccountingSystem.Domain.Core;
using AccountingSystem.Domain.ShopItems;
using AccountingSystem.Domain.Transactions;
using AccountingSystem.Domain.Users;
using AccountingSystem.EntityFramework.EntityFrameworkCore;
using AccountingSystem.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingSystem.Web;

public static class AccountingSystemExtension
{
    public static void Di(this IServiceCollection services)
    {
        // Repositories
        services.AddTransient<IRepository<User, Guid>, EfRepository<User, Guid>>();
        services.AddTransient<IRepository<Category, Guid>, EfRepository<Category, Guid>>();
        services.AddTransient<IRepository<ShopItem, Guid>, EfRepository<ShopItem, Guid>>();
        services.AddTransient<IRepository<TransactionShopItem, Guid>, EfRepository<TransactionShopItem, Guid>>();
        
        // Services
        services.AddTransient<IUserAppService, UserAppService>();
        services.AddTransient<ICategoryAppService, CategoryAppService>();
        services.AddTransient<IShopItemAppService, ShopItemsAppService>();
        services.AddTransient<ITransactionAppService, TransactionAppService>();
    }
}

public static class MigrationExtensions
{
    public static void Migrate<TContext>( this IApplicationBuilder builder) where TContext : DbContext
    {
        using var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var ctx = scope.ServiceProvider.GetRequiredService<TContext>();

        var sp = ctx.GetInfrastructure();

        var modelDiffer = sp.GetRequiredService<IMigrationsModelDiffer>();
        var migrationsAssembly = sp.GetRequiredService<IMigrationsAssembly>();

        var modelInitializer = sp.GetRequiredService<IModelRuntimeInitializer>();
        var sourceModel = modelInitializer.Initialize(migrationsAssembly.ModelSnapshot!.Model);

        var designTimeModel = sp.GetRequiredService<IDesignTimeModel>();
        var readOptimizedModel = designTimeModel.Model;

        var diffsExist = modelDiffer.HasDifferences(
            sourceModel.GetRelationalModel(),
            readOptimizedModel.GetRelationalModel());

        if (diffsExist)
        {
            return;
        }

        ctx.Database.Migrate();
    }
}