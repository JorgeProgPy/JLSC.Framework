using JLSC.Framework.Domain.Core.Geografia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Geografia;

public class PaisConfiguration : IEntityTypeConfiguration<Pais>
{
    public void Configure(EntityTypeBuilder<Pais> builder)
    {
        builder.ToTable("Pais", "core");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.PublicId).IsUnique();

        builder.HasIndex(x => x.Codigo).IsUnique();

        builder.HasIndex(x => x.CodigoISO2).IsUnique();

        builder.HasIndex(x => x.CodigoISO3).IsUnique();

        builder.Property(x => x.Codigo)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.CodigoISO2)
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.CodigoISO3)
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Nacionalidad)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.CodigoTelefonico)
            .HasMaxLength(10);

        builder.Property(x => x.DominioInternet)
            .HasMaxLength(10);

        builder.Property(x => x.Activo)
            .HasDefaultValue(true);
    }
}