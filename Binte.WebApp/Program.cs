using Binte.Data;
using Binte.Data.Entities.Account;
using Binte.Services;
using Binte.Services.Education;
using Binte.Services.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
#region connection

var connection = builder.Configuration.GetConnectionString("MrtConnection");

builder.Services.AddDbContext<BinteContext>(p => p.UseSqlServer(connection));

builder.Services.AddIdentity<BinteUser, BinteRole>()
    .AddUserManager<BinteUserManager>()
    .AddEntityFrameworkStores<BinteContext>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddTransient(typeof(IRepository<>),typeof(EntityRepository<>));

#endregion

#region Services
builder.Services.AddScoped<ISettingService, SettingSevice>();
builder.Services.AddScoped<IEduCategoryService, EduCategoryService>();
builder.Services.AddScoped<IEducationService, EducationService>();
builder.Services.AddScoped<IEduGroupService, EduGroupService>();


#endregion

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute("Default","{Controller=Home}/{Action=Index}/{Id?}");

app.Run();
