using JLSC.Framework.Domain.Core.Seguridad.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Seguridad;

/// <summary>
/// Configuración de persistencia de la entidad Menu.
/// </summary>
public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
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
        EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable(
            "Menus",
            "Seguridad");
    }

    // =====================================================
    // Llave primaria
    // =====================================================

    private static void ConfigurarLlavePrimaria(
        EntityTypeBuilder<Menu> builder)
    {
        builder.HasKey(x => x.Id);
    }

    // =====================================================
    // Propiedades
    // =====================================================

    private static void ConfigurarPropiedades(
        EntityTypeBuilder<Menu> builder)
    {
        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ModuloId)
            .IsRequired();

        builder.Property(x => x.MenuPadreId);

        builder.Property(x => x.Nombre)
            .HasMaxLength(Menu.MaxNombreLength)
            .IsRequired();

        builder.Property(x => x.Ruta)
            .HasMaxLength(Menu.MaxRutaLength)
            .IsRequired();

        builder.Property(x => x.Icono)
            .HasMaxLength(Menu.MaxIconoLength);

        builder.Property(x => x.Orden)
            .IsRequired();

        builder.Property(x => x.MostrarEnMenu)
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
        EntityTypeBuilder<Menu> builder)
    {
        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => x.ModuloId);

        builder.HasIndex(x => x.MenuPadreId);

        builder.HasIndex(x => x.Nombre);

        builder.HasIndex(x => x.Orden);
    }

    // =====================================================
    // Relaciones
    // =====================================================

    private static void ConfigurarRelaciones(
        EntityTypeBuilder<Menu> builder)
    {
        // -------------------------------------------------
        // Menu -> Modulo
        // -------------------------------------------------

        builder.HasOne(x => x.Modulo)
            .WithMany()
            .HasForeignKey(x => x.ModuloId)
            .OnDelete(DeleteBehavior.Restrict);

        // -------------------------------------------------
        // Menu -> MenuPadre
        // -------------------------------------------------

        builder.HasOne(x => x.MenuPadre)
            .WithMany()
            .HasForeignKey(x => x.MenuPadreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}