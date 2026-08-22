using JLSC.Framework.Domain.Core.Seguridad.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Seguridad;

/// <summary>
/// Configuración de persistencia de la entidad Permiso.
/// </summary>
public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
{
    public void Configure(EntityTypeBuilder<Permiso> builder)
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
        EntityTypeBuilder<Permiso> builder)
    {
        builder.ToTable(
            "Permisos",
            "Seguridad");
    }

    // =====================================================
    // Llave primaria
    // =====================================================

    private static void ConfigurarLlavePrimaria(
        EntityTypeBuilder<Permiso> builder)
    {
        builder.HasKey(x => x.Id);
    }

    // =====================================================
    // Propiedades
    // =====================================================

    private static void ConfigurarPropiedades(
        EntityTypeBuilder<Permiso> builder)
    {
        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ModuloId)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(Permiso.MaxNombreLength)
            .IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(Permiso.MaxDescripcionLength);

        builder.Property(x => x.Orden)
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
        EntityTypeBuilder<Permiso> builder)
    {
        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => x.ModuloId);

        builder.HasIndex(x => x.Nombre);

        builder.HasIndex(x => x.Orden);
    }

    // =====================================================
    // Relaciones
    // =====================================================

    private static void ConfigurarRelaciones(
        EntityTypeBuilder<Permiso> builder)
    {
        builder.HasOne(x => x.Modulo)
            .WithMany()
            .HasForeignKey(x => x.ModuloId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}