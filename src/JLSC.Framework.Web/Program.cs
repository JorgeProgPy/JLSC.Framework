using JLSC.Framework.Application.Core.Archivos.Interfaces;
using JLSC.Framework.Application.Core.Catalogos.Interfaces;
using JLSC.Framework.Application.Core.Personas.Interfaces;
using JLSC.Framework.Application.Core.Portal.Commands.ActivarAccesoRapido;
using JLSC.Framework.Application.Core.Portal.Commands.ActivarBanner;
using JLSC.Framework.Application.Core.Portal.Commands.ActivarIndicadorInstitucional;
using JLSC.Framework.Application.Core.Portal.Commands.ActivarProyectoDestacado;
using JLSC.Framework.Application.Core.Portal.Commands.ActualizarAccesoRapido;
using JLSC.Framework.Application.Core.Portal.Commands.ActualizarBanner;
using JLSC.Framework.Application.Core.Portal.Commands.ActualizarConfiguracionInstitucional;
using JLSC.Framework.Application.Core.Portal.Commands.ActualizarIndicadorInstitucional;
using JLSC.Framework.Application.Core.Portal.Commands.ActualizarProyectoDestacado;
using JLSC.Framework.Application.Core.Portal.Commands.CrearAccesoRapido;
using JLSC.Framework.Application.Core.Portal.Commands.CrearBanner;
using JLSC.Framework.Application.Core.Portal.Commands.CrearConfiguracionInstitucional;
using JLSC.Framework.Application.Core.Portal.Commands.CrearIndicadorInstitucional;
using JLSC.Framework.Application.Core.Portal.Commands.CrearProyectoDestacado;
using JLSC.Framework.Application.Core.Portal.Commands.DesactivarAccesoRapido;
using JLSC.Framework.Application.Core.Portal.Commands.DesactivarBanner;
using JLSC.Framework.Application.Core.Portal.Commands.DesactivarIndicadorInstitucional;
using JLSC.Framework.Application.Core.Portal.Commands.DesactivarProyectoDestacado;
using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerAccesoRapido;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerAccesosRapidos;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerBanner;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerBanners;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerConfiguracionInstitucional;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerIndicadoresInstitucionales;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerIndicadorInstitucional;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerProyectoDestacado;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerProyectosDestacados;
using JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarModulo;
using JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarPermiso;
using JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarRol;
using JLSC.Framework.Application.Core.Seguridad.Commands.AsignarPermisoRol;
using JLSC.Framework.Application.Core.Seguridad.Commands.CrearModulo;
using JLSC.Framework.Application.Core.Seguridad.Commands.CrearPermiso;
using JLSC.Framework.Application.Core.Seguridad.Commands.CrearRol;
using JLSC.Framework.Application.Core.Seguridad.Commands.EliminarModulo;
using JLSC.Framework.Application.Core.Seguridad.Commands.EliminarPermiso;
using JLSC.Framework.Application.Core.Seguridad.Commands.EliminarRol;
using JLSC.Framework.Application.Core.Seguridad.Commands.QuitarPermisoRol;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerModuloPorId;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerModulos;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerPermisos;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerPermisosRol;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerRoles;
using JLSC.Framework.Contracts.Core.Archivos.Interfaces;
using JLSC.Framework.Infrastructure.Core.Archivos.Storage.Local.Options;
using JLSC.Framework.Infrastructure.Core.Archivos.Storage.Local.Services;
using JLSC.Framework.Persistence.Core.Archivos.Repositories;
using JLSC.Framework.Persistence.Core.Catalogos.Services;
using JLSC.Framework.Persistence.Core.Database;
using JLSC.Framework.Persistence.Core.Database.Seeders;
using JLSC.Framework.Persistence.Core.Personas.Repositories;
using JLSC.Framework.Persistence.Core.Portal.Repositories;
using JLSC.Framework.Persistence.Core.Seguridad.Repositories;


using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IConfiguracionInstitucionalRepository, ConfiguracionInstitucionalRepository>();
builder.Services.AddScoped<CrearConfiguracionInstitucionalHandler>();
builder.Services.AddScoped<ActualizarConfiguracionInstitucionalHandler>();
builder.Services.AddScoped<ObtenerConfiguracionInstitucionalHandler>();



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<JLSCDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));



// ==========================
// Almacenamiento local
// ==========================

builder.Services.Configure<LocalStorageOptions>(options =>
{
    var rutaConfigurada = builder.Configuration[
        $"{LocalStorageOptions.SectionName}:RutaBase"];

    if (string.IsNullOrWhiteSpace(rutaConfigurada))
    {
        throw new InvalidOperationException(
            "La ruta base del almacenamiento local no está configurada.");
    }

    options.RutaBase = Path.IsPathRooted(rutaConfigurada)
        ? Path.GetFullPath(rutaConfigurada)
        : Path.GetFullPath(
            Path.Combine(
                builder.Environment.ContentRootPath,
                rutaConfigurada));
});

builder.Services.AddScoped<IStorageService, LocalStorageService>();

builder.Services.AddScoped<IArchivoRepository, ArchivoRepository>();

builder.Services.AddScoped<ICarpetaArchivoRepository, CarpetaArchivoRepository>();

builder.Services.AddScoped<IArchivoReferenciaRepository, ArchivoReferenciaRepository>();

builder.Services.AddScoped<ICatalogoService, CatalogoService>();

builder.Services.AddScoped<IArchivoRepository, ArchivoRepository>();

builder.Services.AddScoped<ICarpetaArchivoRepository, CarpetaArchivoRepository>();

builder.Services.AddScoped<IArchivoReferenciaRepository, ArchivoReferenciaRepository>();

builder.Services.AddScoped<ICatalogoService, CatalogoService>();

builder.Services.AddScoped<IModuloRepository, ModuloRepository>();

builder.Services.AddScoped<ObtenerModulosHandler>();

builder.Services.AddScoped<CrearModuloHandler>();

builder.Services.AddScoped<ObtenerModuloPorIdHandler>();

builder.Services.AddScoped<ActualizarModuloHandler>();

builder.Services.AddScoped<EliminarModuloHandler>();

builder.Services.AddScoped<IRolRepository, RolRepository>();

builder.Services.AddScoped<CrearRolHandler>();

builder.Services.AddScoped<ObtenerRolesHandler>();

builder.Services.AddScoped<ActualizarRolHandler>();

builder.Services.AddScoped<EliminarRolHandler>();

builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();

builder.Services.AddScoped<CrearPermisoHandler>();
builder.Services.AddScoped<ObtenerPermisosHandler>();
builder.Services.AddScoped<ActualizarPermisoHandler>();
builder.Services.AddScoped<EliminarPermisoHandler>();

builder.Services.AddScoped<AsignarPermisoRolHandler>();
builder.Services.AddScoped<ObtenerPermisosRolHandler>();
builder.Services.AddScoped<QuitarPermisoRolHandler>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();
builder.Services.AddScoped<IRolPermisoRepository, RolPermisoRepository>();

builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<CrearBannerHandler>();
builder.Services.AddScoped<ActualizarBannerHandler>();
builder.Services.AddScoped<ActivarBannerHandler>();
builder.Services.AddScoped<DesactivarBannerHandler>();
builder.Services.AddScoped<ObtenerBannerHandler>();
builder.Services.AddScoped<ObtenerBannersHandler>();

builder.Services.AddScoped<
    IIndicadorInstitucionalRepository,
    IndicadorInstitucionalRepository>();

builder.Services.AddScoped<CrearIndicadorInstitucionalHandler>();
builder.Services.AddScoped<ActualizarIndicadorInstitucionalHandler>();
builder.Services.AddScoped<ActivarIndicadorInstitucionalHandler>();
builder.Services.AddScoped<DesactivarIndicadorInstitucionalHandler>();
builder.Services.AddScoped<ObtenerIndicadorInstitucionalHandler>();
builder.Services.AddScoped<ObtenerIndicadoresInstitucionalesHandler>();
builder.Services.AddScoped<IAccesoRapidoRepository, AccesoRapidoRepository>();

builder.Services.AddScoped<CrearAccesoRapidoHandler>();
builder.Services.AddScoped<ActualizarAccesoRapidoHandler>();
builder.Services.AddScoped<ActivarAccesoRapidoHandler>();
builder.Services.AddScoped<DesactivarAccesoRapidoHandler>();
builder.Services.AddScoped<ObtenerAccesoRapidoHandler>();
builder.Services.AddScoped<ObtenerAccesosRapidosHandler>();

builder.Services.AddScoped<IProyectoDestacadoRepository, ProyectoDestacadoRepository>();

builder.Services.AddScoped<CrearProyectoDestacadoHandler>();
builder.Services.AddScoped<ActualizarProyectoDestacadoHandler>();
builder.Services.AddScoped<ActivarProyectoDestacadoHandler>();
builder.Services.AddScoped<DesactivarProyectoDestacadoHandler>();
builder.Services.AddScoped<ObtenerProyectoDestacadoHandler>();
builder.Services.AddScoped<ObtenerProyectosDestacadosHandler>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ==========================
// Inicialización de datos
// ==========================

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<JLSCDbContext>();

    await CoreDataSeeder.SeedAsync(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
   
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