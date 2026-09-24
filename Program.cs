using GiftOfTheGivers.Web.Data;
using GiftOfTheGivers.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o => { o.SignIn.RequireConfirmedAccount = false; o.Password.RequiredLength = 6; o.Password.RequireDigit = true; o.Password.RequireUppercase = true; o.Password.RequireNonAlphanumeric = false; })
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/Account/Login");
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
var app = builder.Build();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection(); app.UseStaticFiles(); app.UseRouting(); app.UseAuthentication(); app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
using (var scope = app.Services.CreateScope()) { var services = scope.ServiceProvider; var db = services.GetRequiredService<ApplicationDbContext>(); await db.Database.EnsureCreatedAsync(); await SeedData.Initialize(services); }
app.Run();
