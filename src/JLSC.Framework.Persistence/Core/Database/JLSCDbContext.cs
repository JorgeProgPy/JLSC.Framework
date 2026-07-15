using JLSC.Framework.Domain.Core.Archivos.Entities;
using JLSC.Framework.Domain.Core.Catalogos.Entities;
using JLSC.Framework.Domain.Core.Contactos.Entities;
using JLSC.Framework.Domain.Core.Geografia.Entities;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Database;

public class JLSCDbContext : DbContext
{
    public JLSCDbContext(DbContextOptions<JLSCDbContext> options)
        : base(options)
    {
    }

    #region CORE

    // ==========================
    // Catálogos
    // ==========================

    public DbSet<Catalogo> Catalogos => Set<Catalogo>();

    public DbSet<CatalogoItem> CatalogoItems => Set<CatalogoItem>();

    // ==========================
    // Geografía
    // ==========================

    public DbSet<Pais> Paises => Set<Pais>();

    public DbSet<Departamento> Departamentos => Set<Departamento>();

    public DbSet<Provincia> Provincias => Set<Provincia>();

    public DbSet<Ciudad> Ciudades => Set<Ciudad>();

    public DbSet<Ubicacion> Ubicaciones => Set<Ubicacion>();

    // ==========================
    // Contactos
    // ==========================

    public DbSet<Contacto> Contactos => Set<Contacto>();

    public DbSet<ContactoItem> ContactoItems => Set<ContactoItem>();

    // ==========================
    // Archivos
    // ==========================

    public DbSet<CarpetaArchivo> CarpetasArchivo => Set<CarpetaArchivo>();

    public DbSet<Archivo> Archivos => Set<Archivo>();

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(JLSCDbContext).Assembly);
    }
}