using JLSC.Framework.Domain.Core.Catalogos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Catalogos;

public class CatalogoItemConfiguration : IEntityTypeConfiguration<CatalogoItem>
{
    public void Configure(EntityTypeBuilder<CatalogoItem> builder)
    {
        builder.ToTable("CatalogoItem", "core");

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
            x.CatalogoId,
            x.Codigo
        }).IsUnique();

        // ==========================
        // Datos del negocio
        // ==========================

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Valor)
            .HasMaxLength(250);

        builder.Property(x => x.Descripcion)
            .HasMaxLength(500);

        // ==========================
        // Presentación (UI)
        // ==========================

        builder.Property(x => x.Icono)
            .HasMaxLength(100);

        builder.Property(x => x.Color)
            .HasMaxLength(20);

        builder.Property(x => x.CssClass)
            .HasMaxLength(100);

        // ==========================
        // Comportamiento
        // ==========================

        builder.Property(x => x.UrlBase)
            .HasMaxLength(300);

        builder.Property(x => x.Target)
            .HasMaxLength(15);

        // ==========================
        // Sistema
        // ==========================

        builder.Property(x => x.Orden)
            .HasDefaultValue(0);

        builder.Property(x => x.Activo)
            .HasDefaultValue(true);

        // ==========================
        // Relaciones
        // ==========================

        builder.HasOne(x => x.Catalogo)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CatalogoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}