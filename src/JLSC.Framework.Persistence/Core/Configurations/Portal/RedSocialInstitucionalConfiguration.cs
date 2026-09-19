using JLSC.Framework.Domain.Core.Portal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Portal;

public sealed class RedSocialInstitucionalConfiguration
    : IEntityTypeConfiguration<RedSocialInstitucional>
{
    public void Configure(
        EntityTypeBuilder<RedSocialInstitucional> builder)
    {
        builder.ToTable(
            "RedSocialInstitucional",
            "Portal");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.Property(x => x.ConfiguracionInstitucionalId)
            .IsRequired();

        builder.Property(x => x.CatalogoItemId)
            .IsRequired();

        builder.Property(x => x.Url)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Orden)
            .IsRequired();

        builder.Property(x => x.Activo)
            .IsRequired();

        builder.Property(x => x.FechaCreacion)
            .IsRequired();

        builder.Property(x => x.FechaModificacion);

        builder.HasIndex(x => new
        {
            x.ConfiguracionInstitucionalId,
            x.Orden
        });

        builder.HasIndex(x => new
        {
            x.ConfiguracionInstitucionalId,
            x.CatalogoItemId
        })
        .IsUnique();

        builder.HasOne(x => x.ConfiguracionInstitucional)
            .WithMany(x => x.RedesSociales)
            .HasForeignKey(x => x.ConfiguracionInstitucionalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CatalogoItem)
            .WithMany()
            .HasForeignKey(x => x.CatalogoItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}