using JLSC.Framework.Domain.Core.Portal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Portal;

public sealed class IndicadorInstitucionalConfiguration
    : IEntityTypeConfiguration<IndicadorInstitucional>
{
    public void Configure(
        EntityTypeBuilder<IndicadorInstitucional> builder)
    {
        builder.ToTable(
            "IndicadorInstitucional",
            "Portal");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.Property(x => x.Titulo)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Valor)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(500);

        builder.Property(x => x.Icono)
            .HasMaxLength(100);

        builder.Property(x => x.Orden)
            .IsRequired();

        builder.Property(x => x.Activo)
            .IsRequired();

        builder.Property(x => x.FechaCreacion)
            .IsRequired();

        builder.Property(x => x.FechaModificacion);

        builder.HasIndex(x => new
        {
            x.Activo,
            x.Orden
        });
    }
}