using Lamazon.Services.Extensions;
using Lamazon.Web.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
string connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

builder.Services.AddControllersWithViews();

builder.Services.InjectRepositories();
builder.Services.InjectServices();
builder.Services.InjectAutoMapper();
builder.Services.InjectFluentValidators();
builder.Services.InjectDbContext(connectionString);
builder.Host.InjectSerilogConfiguration(connectionString);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.LoginPath = "/Users/Login";
    opt.SlidingExpiration = true;
    opt.ExpireTimeSpan = TimeSpan.FromHours(1);
});

builder.Services.AddAuthorization();

// Build the app
var app = builder.Build();

// Configure the middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseDebugIpAddressMiddleware();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Administration",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
