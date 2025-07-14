using BeToff.Entities;
using BeToff.DAL;
using Microsoft.EntityFrameworkCore;
using BeToff.BLL.Interface;
using BeToff.BLL;
using BeToff.DAL.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);
string ConnectionString = "BeToffDbContext";

// Add services to the container.
builder.Services.AddControllersWithViews();


// Loading Databases (DI)
builder.Services.AddDbContext<BeToffDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionString) ?? throw new InvalidOperationException("Connection string 'BeToffDbContext' not found."))
);
//builder.Services.AddTransient<IWebHostEnvironment, WebHostEnvironment>
builder.Services.AddTransient<IUserDao, UserDao>();
builder.Services.AddTransient<IUserBc, UserBc>();
builder.Services.AddTransient<IPhotoDao, PhotoDao>();
builder.Services.AddTransient<IPhotoBc, PhotoBc>();
builder.Services.AddTransient<IFamillyDao, FamillyDao>();
builder.Services.AddTransient<IFamillyBc, FamillyBc>();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 512 * 1024 * 1024; //500 Mo en bytes
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 512 * 1024 * 1024; // 500 Mo en bytes
});

builder.Services.AddMemoryCache();
//builder.Services.AddSession(opt =>
//{
//    opt.IdleTimeout = TimeSpan.FromDays(1);
//    opt.Cookie.Expiration = TimeSpan.FromDays(1);
//    opt.Cookie.HttpOnly = true;
//    opt.Cookie.IsEssential = true;
//}
//);

// Service for Authentification with Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();



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

app.UseFileServer();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "Media/")),
    RequestPath = "/Media",

    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
            "Cache-Control", "no-store, no-cache, must-revalidate");
        ctx.Context.Response.Headers.Append("Pragma", "no-cache");
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
