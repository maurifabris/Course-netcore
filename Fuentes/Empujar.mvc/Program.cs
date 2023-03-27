using Adrian.core.Config;
using Adrian.mvc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//#####################################
//            APPSETTING             //
//#####################################

//************************************
//AGREGADO MANUALMENTE para leer el APPSETTINGS.JSON
//************************************

ConfigDB.MainDBConnectionString = builder.Configuration.GetConnectionString("MainConnection");


//#####################################
//              SERVICIOS            //
//#####################################

//************************************
//AGREGADO MANUALMENTE 
//************************************
//Inicializo el DBContext principal
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection")));

//Inicializo Identity con los valores por Default e indico que se permiten 10 intentos fallidos y de alcanzarlo debe esperar 15 minutos
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

//Configuro Identity con los valores del archivo de configuracion
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = ConfigSecurity.RequireDigit;
    options.Password.RequiredLength = ConfigSecurity.RequiredLength;
    options.Password.RequireNonAlphanumeric = ConfigSecurity.RequireNonLetterOrDigit;
    options.Password.RequireUppercase = ConfigSecurity.RequireUppercase;
    options.Password.RequireLowercase = ConfigSecurity.RequireLowercase;
});

builder.Services.AddControllersWithViews();
builder.Services.AddResponseCaching();
//************************************

// Add services to the container.
builder.Services.AddRazorPages();


//#####################################
//            CONFIGURACION          //
//#####################################

//************************************
//AGREGADO MANUALMENTE para fijar la cultura
//************************************
var cultureInfo = new CultureInfo("es-AR");

cultureInfo.NumberFormat.CurrencySymbol = "$ ";
cultureInfo.NumberFormat.CurrencyDecimalSeparator = ",";
cultureInfo.NumberFormat.CurrencyDecimalDigits = 2;
cultureInfo.NumberFormat.CurrencyGroupSeparator = ".";
cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
cultureInfo.NumberFormat.NumberDecimalDigits = 2;
cultureInfo.NumberFormat.NumberGroupSeparator = ".";
cultureInfo.NumberFormat.PercentDecimalSeparator = ",";
cultureInfo.NumberFormat.PercentGroupSeparator = ".";
cultureInfo.NumberFormat.PercentDecimalDigits = 2;

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
//************************************

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//************************************
//AGREGADO MANUALMENTE para chaching
app.UseResponseCaching();
//************************************

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapRazorPages();

app.Run();
