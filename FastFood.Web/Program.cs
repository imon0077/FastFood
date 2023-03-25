using FastFood.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.UI.Services;
using FastFood.Web.Models;
using FastFood.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContextConnection")));

//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>()
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddRazorPages();

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
DataSeeding();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();

//app.MapControllerRoute(
//        name: "areas",
//        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );

app.MapControllerRoute(
    name: "default",
    pattern: "{area=admin}/{controller=categories}/{action=Index}/{id?}");

app.Run();

void DataSeeding()
{
    //using var provider = new ServiceCollection()
    //.AddScoped<DbInitializer>()
    //.BuildServiceProvider();

    //await using (var scope = app.Services.CreateAsyncScope())
    //{
    //    var DbInitizer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    //} 

    using (var scope = app.Services.CreateScope())
    {
        var DbInitizer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitizer.Initialize();
    }
}
