using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Data;
using SmartHomeManager.Models;
using SmartHomeManager.Services.DeviceService;
using SmartHomeManager.Services.RoomService;
using SmartHomeManager.Services.EmailService;
using Microsoft.Extensions.Options;
using SmartHomeManager.Services;
using SmartHomeManager.Services.AccessLevelService;
using SmartHomeManager.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IAccessLevelService, AccessLevelService>();

builder.Services.Configure<SMTPSettings>(builder.Configuration.GetSection("SMTPSettings"));
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();