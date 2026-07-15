using JLSC.Framework.Domain.Core.Archivos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Archivos;

public class CarpetaArchivoConfiguration : IEntityTypeConfiguration<CarpetaArchivo>
{
    public void Configure(EntityTypeBuilder<CarpetaArchivo> builder)
    {
        builder.ToTable("CarpetaArchivo", "core");

        // ==========================
        // Clave primaria
        // ==========================

        builder.HasKey(x => x.Id);

        // ==========================
        // Índices
        // ==========================

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.CarpetaPadreId,
            x.Codigo
        }).IsUnique();

        // ==========================
        // Propiedades
        // ==========================

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Icono)
            .HasMaxLength(100);

        builder.Property(x => x.CssClass)
            .HasMaxLength(100);

        builder.Property(x => x.Descripcion)
            .HasMaxLength(500);

        builder.Property(x => x.Visible)
            .HasDefaultValue(true);

        builder.Property(x => x.EsRaiz)
            .HasDefaultValue(false);

        builder.Property(x => x.Activo)
            .HasDefaultValue(true);

        // ==========================
        // Relaciones
        // ==========================

        builder.HasOne(x => x.CarpetaPadre)
            .WithMany(x => x.Subcarpetas)
            .HasForeignKey(x => x.CarpetaPadreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}