using JLSC.Framework.Domain.Core.Portal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Portal;

public sealed class ProyectoDestacadoConfiguration
    : IEntityTypeConfiguration<ProyectoDestacado>
{
    public void Configure(
        EntityTypeBuilder<ProyectoDestacado> builder)
    {
        builder.ToTable("ProyectoDestacado", "Portal");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.Property(x => x.Titulo)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(1000);

        builder.Property(x => x.Ubicacion)
            .HasMaxLength(200);

        builder.Property(x => x.ArchivoId);

        builder.Property(x => x.Enlace)
            .HasMaxLength(500);

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

        builder.HasOne(x => x.Archivo)
            .WithMany()
            .HasForeignKey(x => x.ArchivoId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}