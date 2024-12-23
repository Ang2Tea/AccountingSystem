using AccountingSystem.EntityFramework.EntityFrameworkCore;
using AccountingSystem.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AccountingSystemDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AccountingSystem")));

builder.Services.AddRazorPages();
builder.Services.Di();

var app = builder.Build();

app.Migrate<AccountingSystemDbContext>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();