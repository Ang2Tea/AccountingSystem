using System.Text;
using AccountingSystem.EntityFramework.EntityFrameworkCore;
using AccountingSystem.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AccountingSystemDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AccountingSystem")));

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Categories", "admin");
    options.Conventions.AuthorizeFolder("/ShopItems", "admin");
    options.Conventions.AuthorizeFolder("/Transactions", "admin");
    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AllowAnonymousToFolder("/Auth");
} );
builder.Services.Di();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var config = builder.Configuration["PrivateKey"];
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

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