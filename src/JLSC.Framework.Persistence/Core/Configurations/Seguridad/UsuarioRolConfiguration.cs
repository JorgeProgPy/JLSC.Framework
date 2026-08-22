using JLSC.Framework.Domain.Core.Seguridad.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Seguridad;

/// <summary>
/// Configuración de persistencia de la entidad UsuarioRol.
/// </summary>
public class UsuarioRolConfiguration
    : IEntityTypeConfiguration<UsuarioRol>
{
    public void Configure(
        EntityTypeBuilder<UsuarioRol> builder)
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
        EntityTypeBuilder<UsuarioRol> builder)
    {
        builder.ToTable(
            "UsuarioRoles",
            "Seguridad");
    }

    // =====================================================
    // Llave primaria
    // =====================================================

    private static void ConfigurarLlavePrimaria(
        EntityTypeBuilder<UsuarioRol> builder)
    {
        builder.HasKey(x => x.Id);
    }

    // =====================================================
    // Propiedades
    // =====================================================

    private static void ConfigurarPropiedades(
        EntityTypeBuilder<UsuarioRol> builder)
    {
        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.UsuarioId)
            .IsRequired();

        builder.Property(x => x.RolId)
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
        EntityTypeBuilder<UsuarioRol> builder)
    {
        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.UsuarioId,
            x.RolId
        })
        .IsUnique();

        builder.HasIndex(x => x.UsuarioId);

        builder.HasIndex(x => x.RolId);
    }

    // =====================================================
    // Relaciones
    // =====================================================

    private static void ConfigurarRelaciones(
        EntityTypeBuilder<UsuarioRol> builder)
    {
        // -------------------------------------------------
        // UsuarioRol -> Usuario
        // -------------------------------------------------

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        // -------------------------------------------------
        // UsuarioRol -> Rol
        // -------------------------------------------------

        builder.HasOne(x => x.Rol)
            .WithMany()
            .HasForeignKey(x => x.RolId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}