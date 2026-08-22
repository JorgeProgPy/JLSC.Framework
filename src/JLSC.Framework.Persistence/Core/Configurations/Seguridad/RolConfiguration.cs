using JLSC.Framework.Domain.Core.Seguridad.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Seguridad;

/// <summary>
/// Configuración de persistencia de la entidad Rol.
/// </summary>
public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
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
        EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable(
            "Roles",
            "Seguridad");
    }

    // =====================================================
    // Llave primaria
    // =====================================================

    private static void ConfigurarLlavePrimaria(
        EntityTypeBuilder<Rol> builder)
    {
        builder.HasKey(x => x.Id);
    }

    // =====================================================
    // Propiedades
    // =====================================================

    private static void ConfigurarPropiedades(
        EntityTypeBuilder<Rol> builder)
    {
        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(Rol.MaxNombreLength)
            .IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(Rol.MaxDescripcionLength);

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
        EntityTypeBuilder<Rol> builder)
    {
        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => x.Nombre);
    }

    // =====================================================
    // Relaciones
    // =====================================================

    private static void ConfigurarRelaciones(
        EntityTypeBuilder<Rol> builder)
    {
    }
}