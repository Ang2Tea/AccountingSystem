using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.EntityFramework.EntityFrameworkCore;

public static class AccountingSystemDbContextModelCreatingExtensions
{
    public static void ConfigureInventoryService(this ModelBuilder builder)
    {
        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(InventoryServiceDbProperties.DbTablePrefix + "Questions", InventoryServiceDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
    }
}