using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using AutoMapper;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Web.Configuration;
using Web.Handler;
//using Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Add Automapper in services
var automapper = new MapperConfiguration(item => item.AddProfile(new AutomapperHandler()));
IMapper mapper=automapper.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCoreServices(builder.Configuration);


var app = builder.Build();

using(var scope=app.Services.CreateScope())
{
    var scopedprovider=scope.ServiceProvider;
    try
    {
        var usermanager = scopedprovider.GetRequiredService<UserManager<ApplicationUser>>();
        var rolemanager = scopedprovider.GetRequiredService<RoleManager<IdentityRole>>();
        var identityContext = scopedprovider.GetRequiredService<AppDbContext>();
        await AppIdentityDbContextSeed.SeedAsync(identityContext,usermanager, rolemanager);
    }
    catch(Exception ex)
    {
        //throw new Exception(ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
