using HRM;
using HRM.Data;
using HRM.Models;
using HRM.Services;
using HRM.Services.CompaniesServices;
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
builder.Services.AddScoped<IGenericControlService<StatusType>, GenericControlService<StatusType>>();
builder.Services.AddScoped<IGenericControlService<Company>, GenericControlService<Company>>();
builder.Services.AddScoped<IGenericControlService<UserLevel>, GenericControlService<UserLevel>>();
builder.Services.AddScoped<IGenericControlService<Team>, GenericControlService<Team>>();
builder.Services.AddScoped<IGenericControlService<Setting>, GenericControlService<Setting>>();
builder.Services.AddScoped<IGenericControlService<RoleType>, GenericControlService<RoleType>>();
builder.Services.AddScoped<IGenericControlService<RequestType>, GenericControlService<RequestType>>();
builder.Services.AddScoped<IGenericControlService<Request>, GenericControlService<Request>>();
builder.Services.AddScoped<IGenericControlService<OffitialHolliday>, GenericControlService<OffitialHolliday>>();

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
