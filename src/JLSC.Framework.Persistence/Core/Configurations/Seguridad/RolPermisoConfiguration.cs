using JLSC.Framework.Domain.Core.Seguridad.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Seguridad;

/// <summary>
/// Configuración de persistencia de la entidad RolPermiso.
/// </summary>
public class RolPermisoConfiguration
    : IEntityTypeConfiguration<RolPermiso>
{
    public void Configure(
        EntityTypeBuilder<RolPermiso> builder)
    {
        ConfigurarTabla(builder);

        ConfigurarLlavePrimaria(builder);

        ConfigurarPropiedades(builder);

        ConfigurarIndices(builder);

        ConfigurarRelaciones(builder);
    }

    // =====================================================
    // Tabla
    // =====================================================

    private static void ConfigurarTabla(
        EntityTypeBuilder<RolPermiso> builder)
    {
        builder.ToTable(
            "RolPermisos",
            "Seguridad");
    }

    // =====================================================
    // Llave primaria
    // =====================================================

    private static void ConfigurarLlavePrimaria(
        EntityTypeBuilder<RolPermiso> builder)
    {
        builder.HasKey(x => x.Id);
    }

    // =====================================================
    // Propiedades
    // =====================================================

    private static void ConfigurarPropiedades(
        EntityTypeBuilder<RolPermiso> builder)
    {
        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.RolId)
            .IsRequired();

        builder.Property(x => x.PermisoId)
            .IsRequired();

        builder.Property(x => x.Activo)
            .IsRequired();

        builder.Property(x => x.FechaCreacion)
            .IsRequired();

        builder.Property(x => x.FechaModificacion);

        builder.Property(x => x.UsuarioCreacionId);

        builder.Property(x => x.UsuarioModificacionId);
    }

    // =====================================================
    // Índices
    // =====================================================

    private static void ConfigurarIndices(
        EntityTypeBuilder<RolPermiso> builder)
    {
        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.RolId,
            x.PermisoId
        })
        .IsUnique();

        builder.HasIndex(x => x.RolId);

        builder.HasIndex(x => x.PermisoId);
    }

    // =====================================================
    // Relaciones
    // =====================================================

    private static void ConfigurarRelaciones(
        EntityTypeBuilder<RolPermiso> builder)
    {
        // -------------------------------------------------
        // RolPermiso -> Rol
        // -------------------------------------------------

        builder.HasOne(x => x.Rol)
            .WithMany()
            .HasForeignKey(x => x.RolId)
            .OnDelete(DeleteBehavior.Restrict);

        // -------------------------------------------------
        // RolPermiso -> Permiso
        // -------------------------------------------------

        builder.HasOne(x => x.Permiso)
            .WithMany()
            .HasForeignKey(x => x.PermisoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}