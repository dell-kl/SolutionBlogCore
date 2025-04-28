using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.AccesoDatos.Data.SeederDB;
using BlogCore.Data;
using BlogCore.Models;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// AddDefaultIdentity -> Agrega automaticamente lo que es la UI... (pero debemos especificar si queremos trabajar con 
// otras Entidades con AddRole

// !!! IMPROTANTE === estos de aca abajo hay agregar la Interfaz Grafica (especificarle) 
// AddIdentityCore -> agrega validacion avanzada para implementar con autenticacion de tercero y doble factor
// AddIdentity -> las mismas funciones que el anterior, pero no implementa validaciones de Tercero


builder.Services.AddDefaultIdentity<ApplicationUser>
(options => options.SignIn.RequireConfirmedAccount = false)
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<
ValidarInicioSesion<ApplicationUser>
>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddScoped<ISeedDb, SeedDb>();
builder.Services.AddScoped<IUnitofWork, UnitofWork>();

var app = builder.Build();

//ejecucion de la siembre de datos. 
using (var scope = app.Services.CreateScope() )
{
    var services = scope.ServiceProvider;
    try
    {
        var serviceDbSeed = services.GetRequiredService<ISeedDb>();
        serviceDbSeed.InicializarDatos(); 
    }
    catch(Exception e )
    {
        Console.WriteLine("====================================================");
        Console.WriteLine("[!!] ERROR NO SE PUEDO INICIALIZAR LA SIEMBRA DE DATOS");
        Console.WriteLine("====================================================");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Client}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
