using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialProject.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("myconn");
builder.Services.AddDbContext<CustomIdentityDbContext>(options =>
{
    options.UseSqlServer(connection, b => b.MigrationsAssembly("SocialProject.WebUI"));
});

builder.Services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
    .AddEntityFrameworkStores<CustomIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IPasswordHasher<CustomIdentityUser>, PasswordHasher<CustomIdentityUser>>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
