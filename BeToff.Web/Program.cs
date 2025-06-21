using BeToff.Entities;
using BeToff.DAL;
using Microsoft.EntityFrameworkCore;
using BeToff.BLL.Interface;
using BeToff.BLL;
using BeToff.DAL.Interface;


var builder = WebApplication.CreateBuilder(args);
string ConnectionString = "BeToffDbContext";

// Add services to the container.
builder.Services.AddControllersWithViews();


// Loading Databases (DI)
builder.Services.AddDbContext<BeToffDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionString) ?? throw new InvalidOperationException("Connection string 'BeToffDbContext' not found."))
);

builder.Services.AddTransient<IUserDao, UserDao>();
builder.Services.AddTransient<IUserBc, UserBc>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
