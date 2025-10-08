using BeToff.Entities;
using BeToff.DAL;
using Microsoft.EntityFrameworkCore;
using BeToff.BLL.Interface;
using BeToff.BLL;
using BeToff.DAL.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using BeToff.BLL.Service.Impl;
using BeToff.BLL.Service.Interface;
using BeToff.Web.Hubs;
using BeToff.Web.WebServices;
using BeToff.DAL.Service;


var builder = WebApplication.CreateBuilder(args);
string ConnectionString = "BeToffDbContext";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

// Loading Databases (DI)
builder.Services.AddDbContext<BeToffDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionString) ?? throw new InvalidOperationException("Connection string 'BeToffDbContext' not found."))
);

//connection MongoDB
builder.Services.Configure<CommentDatabaseSettings>(
    builder.Configuration.GetSection("NonRelationalSetting"));

builder.Services.Configure<ConversationDatabaseSettings>(
    builder.Configuration.GetSection("NonRelationalSetting"));

builder.Services.Configure<MessageDatabaseSettings>(
    builder.Configuration.GetSection("NonRelationalSetting"));

builder.Services.Configure<ConversationGroupDatabaseSettings>(
    builder.Configuration.GetSection("NonRelationalSetting"));

builder.Services.AddTransient<IUserDao, UserDao>();
builder.Services.AddTransient<IUserBc, UserBc>();
builder.Services.AddTransient<IPhotoDao, PhotoDao>();
builder.Services.AddTransient<IPhotoBc, PhotoBc>();
builder.Services.AddTransient<IFamillyDao, FamillyDao>();
builder.Services.AddTransient<IFamillyBc, FamillyBc>();
builder.Services.AddTransient<IRegistrationDao, RegistrationDao>();
builder.Services.AddTransient<IRegistrationBc, RegistrationBc>();
builder.Services.AddTransient<IInvitationDao, InvitationDao>();
builder.Services.AddTransient<IUserInvitationService, UserInvitationService>();
builder.Services.AddTransient<IPhotoFamilyDao, PhotoFamilyDao>();
builder.Services.AddTransient<IPhotoFamilyBc, PhotoFamilyBc>();
builder.Services.AddTransient<IChatService, ChatService>();
builder.Services.AddTransient<IChatGroupService, ChatGroupService>();

builder.Services.AddSingleton<ICommentService, CommentService>();
builder.Services.AddSingleton<IConversationService, ConversationService>();
builder.Services.AddSingleton<IMessageService, MessageService>();
builder.Services.AddSingleton<IConversationGroupsService, ConversationGroupsService>();

builder.Services.AddHostedService<WebBackgroundService>();

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

app.MapHub<NotificationHub>("/Notification");
app.MapHub<ConversationHub>("/Chat");
app.MapHub<ConversationGroupsHub>("/ChatGroup");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
