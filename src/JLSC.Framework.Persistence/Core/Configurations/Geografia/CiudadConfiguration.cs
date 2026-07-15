using JLSC.Framework.Domain.Core.Geografia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Geografia;

public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        builder.ToTable("Ciudad", "core");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.ProvinciaId,
            x.Codigo
        }).IsUnique();

        builder.Property(x => x.Codigo)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Activo)
            .HasDefaultValue(true);

        builder.HasOne(x => x.Provincia)
            .WithMany(x => x.Ciudades)
            .HasForeignKey(x => x.ProvinciaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}