using HRM;
using HRM.Data;
using HRM.Models;
using HRM.Services;
using HRM.Services.CompaniesServices;
using HRM.Services.OffitialHollidaysServices;
using HRM.Services.RequestsServices;
using HRM.Services.RequestTypesService;
using HRM.Services.RoleTypesServices;
using HRM.Services.SettingsServices;
using HRM.Services.StatusesServices;
using HRM.Services.StatusTypesServices;
using HRM.Services.TeamsServices;
using HRM.Services.UserLevelsServices;
using HRM.Services.UsersServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HRMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
    });

//builder.Services.AddScoped<IMyDependency, MyDependency>();
builder.Services.AddScoped<IUsersControlService, UsersControlService>();
builder.Services.AddScoped<IStatusesControlService, StatusesControlService>();

builder.Services.AddScoped<IGenericControlService<StatusType>, StatusTypesControlService>();
builder.Services.AddScoped<IGenericControlService<Company>, CompaniesControlService>();
builder.Services.AddScoped<IGenericControlService<UserLevel>, UserLevelsControlService>();
builder.Services.AddScoped<IGenericControlService<Team>, TeamsControlService>();
builder.Services.AddScoped<IGenericControlService<Setting>, SettingsControlService>();
builder.Services.AddScoped<IGenericControlService<RoleType>, RoleTypesControlService>();
builder.Services.AddScoped<IGenericControlService<RequestType>, RequestTipesControlService>();
builder.Services.AddScoped<IGenericControlService<Request>, RequestsControlService>();
builder.Services.AddScoped<IGenericControlService<OffitialHolliday>, OffitialHollidaysControlService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<HRMContext>();
        SampleData.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
