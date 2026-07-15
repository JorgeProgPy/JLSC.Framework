using JLSC.Framework.Domain.Core.Catalogos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Catalogos;

public class CatalogoConfiguration : IEntityTypeConfiguration<Catalogo>
{
    public void Configure(EntityTypeBuilder<Catalogo> builder)
    {
        builder.ToTable("Catalogo", "core");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.PublicId).IsUnique();

        builder.HasIndex(x => x.Codigo).IsUnique();

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(500);

        builder.Property(x => x.Orden);

        builder.Property(x => x.EsSistema)
            .HasDefaultValue(true);

        builder.Property(x => x.Activo)
            .HasDefaultValue(true);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Catalogo)
            .HasForeignKey(x => x.CatalogoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}