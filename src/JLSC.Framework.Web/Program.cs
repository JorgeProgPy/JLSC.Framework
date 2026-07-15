using JLSC.Framework.Application.Core.Archivos.Interfaces;
using JLSC.Framework.Application.Core.Catalogos.Interfaces;
using JLSC.Framework.Contracts.Core.Archivos.Interfaces;
using JLSC.Framework.Infrastructure.Core.Archivos.Storage.Local.Options;
using JLSC.Framework.Infrastructure.Core.Archivos.Storage.Local.Services;
using JLSC.Framework.Persistence.Core.Archivos.Repositories;
using JLSC.Framework.Persistence.Core.Catalogos.Services;
using JLSC.Framework.Persistence.Core.Database;
using JLSC.Framework.Persistence.Core.Database.Seeders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddScoped<ICatalogoService, CatalogoService>();

var app = builder.Build(); 

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