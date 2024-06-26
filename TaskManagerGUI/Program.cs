using TaskManagerGUI.Data;
using TaskManagerGUI.Hubs;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor;
using TaskManagerGUI.Repositories;
using Microsoft.AspNetCore.RateLimiting;
using TaskManagerGUI.Middleware;
using TaskManagerGUI.Cache;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var configuration = builder.Configuration;

builder.Services.AddDbContext<SelectCompanyContext>(options => options.UseSqlServer(
    configuration.GetConnectionString("DatabaseConnection")
));

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(
    configuration.GetConnectionString("UserConnection")
));

builder.Services.AddDbContext<ServiceDatabaseContext>(options => options.UseSqlServer(
    configuration.GetConnectionString("ServiceConnection")
));

builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1020 * 1020 * 10;
    options.CompactionPercentage = 0.25;
    options.ExpirationScanFrequency = TimeSpan.FromSeconds(30);
});


// Add services to the container.
builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddLoginRateLimiter();

builder.Services.AddSignalR();

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ISelectCompanyRepository, SelectCompanyRepository>();
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();
builder.Services.AddScoped<IMemoryRepository, MemoryRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseLoginRateLimiter();

app.UseAuthorization();



app.MapHub<MemoryStatsHub>("/memoryStatsHub");
app.MapHub<ProcessHub>("/processHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
