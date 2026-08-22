using JLSC.Framework.Domain.Core.Seguridad.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Seguridad;

/// <summary>
/// Configuración de persistencia de la entidad Usuario.
/// </summary>
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
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
        EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable(
            "Usuarios",
            "Seguridad");
    }

    // =====================================================
    // Llave primaria
    // =====================================================

    private static void ConfigurarLlavePrimaria(
        EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(x => x.Id);
    }

    // =====================================================
    // Propiedades
    // =====================================================

    private static void ConfigurarPropiedades(
        EntityTypeBuilder<Usuario> builder)
    {
        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PersonaId)
            .IsRequired();

        builder.Property(x => x.NombreUsuario)
            .HasMaxLength(Usuario.MaxNombreUsuarioLength)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(Usuario.MaxPasswordHashLength)
            .IsRequired();

        builder.Property(x => x.UltimoAcceso);

        builder.Property(x => x.DebeCambiarPassword)
            .IsRequired();

        builder.Property(x => x.Bloqueado)
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
        EntityTypeBuilder<Usuario> builder)
    {
        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => x.PersonaId)
            .IsUnique();

        builder.HasIndex(x => x.NombreUsuario)
            .IsUnique();
    }

    // =====================================================
    // Relaciones
    // =====================================================

    private static void ConfigurarRelaciones(
        EntityTypeBuilder<Usuario> builder)
    {
        builder.HasOne(x => x.Persona)
            .WithMany()
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}