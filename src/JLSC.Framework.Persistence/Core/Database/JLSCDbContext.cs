using JLSC.Framework.Domain.Core.Archivos.Entities;
using JLSC.Framework.Domain.Core.Catalogos.Entities;
using JLSC.Framework.Domain.Core.Contactos.Entities;
using JLSC.Framework.Domain.Core.Geografia.Entities;
using JLSC.Framework.Domain.Core.Personas.Entities;
using JLSC.Framework.Domain.Core.Seguridad.Entities;
using JLSC.Framework.Domain.Core.Portal.Entities;

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
    // Personas
    // ==========================

    public DbSet<Persona> Personas => Set<Persona>();

    // ==========================
    // Archivos
    // ==========================

    public DbSet<CarpetaArchivo> CarpetasArchivo => Set<CarpetaArchivo>();

    public DbSet<Archivo> Archivos => Set<Archivo>();

    #endregion

    public DbSet<ArchivoReferencia> ArchivoReferencias =>
        Set<ArchivoReferencia>();


    // ==========================
    // Seguridad
    // ==========================

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    public DbSet<Rol> Roles => Set<Rol>();

    public DbSet<Permiso> Permisos => Set<Permiso>();

    public DbSet<Modulo> Modulos => Set<Modulo>();

    public DbSet<Menu> Menus => Set<Menu>();

    public DbSet<UsuarioRol> UsuariosRoles => Set<UsuarioRol>();


    // ==========================
    // Portal
    // ==========================

    public DbSet<Banner> Banners => Set<Banner>();


    public DbSet<IndicadorInstitucional> IndicadoresInstitucionales
    => Set<IndicadorInstitucional>();


    public DbSet<RolPermiso> RolesPermisos => Set<RolPermiso>();

    public DbSet<AccesoRapido> AccesosRapidos
    => Set<AccesoRapido>();

    public DbSet<ProyectoDestacado> ProyectosDestacados
    => Set<ProyectoDestacado>();


    public DbSet<ConfiguracionInstitucional> ConfiguracionesInstitucionales
       => Set<ConfiguracionInstitucional>();

    public DbSet<RedSocialInstitucional> RedesSocialesInstitucionales
        => Set<RedSocialInstitucional>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(JLSCDbContext).Assembly);
    }
}