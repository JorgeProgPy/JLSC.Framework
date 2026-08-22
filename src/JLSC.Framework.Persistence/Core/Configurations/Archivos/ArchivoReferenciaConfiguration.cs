using JLSC.Framework.Domain.Core.Archivos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Archivos;

public sealed class ArchivoReferenciaConfiguration
    : IEntityTypeConfiguration<ArchivoReferencia>
{
    public void Configure(
        EntityTypeBuilder<ArchivoReferencia> builder)
    {
        builder.ToTable("archivo_referencias", "core");

        builder.HasKey(x => x.Id);

        // ==========================
        // Propiedades
        // ==========================

        builder.Property(x => x.ArchivoId)
            .IsRequired();

        builder.Property(x => x.ModuloId)
            .IsRequired();

        builder.Property(x => x.EntidadId)
            .IsRequired();

        builder.Property(x => x.TipoUsoId)
            .IsRequired();

        builder.Property(x => x.Orden)
            .IsRequired();

        builder.Property(x => x.EsPrincipal)
            .IsRequired()
            .HasDefaultValue(false);


        builder.Property(x => x.Observacion)
            .HasMaxLength(500);

        // ==========================
        // Relaciones
        // ==========================

        builder.HasOne(x => x.Archivo)
            .WithMany()
            .HasForeignKey(x => x.ArchivoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Modulo)
            .WithMany()
            .HasForeignKey(x => x.ModuloId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TipoUso)
            .WithMany()
            .HasForeignKey(x => x.TipoUsoId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==========================
        // Índices
        // ==========================

        builder.HasIndex(x => x.ArchivoId);

        builder.HasIndex(x => new
        {
            x.ModuloId,
            x.EntidadId
        });

        builder.HasIndex(x => new
        {
            x.ModuloId,
            x.EntidadId,
            x.Activo
        });

        builder.HasIndex(x => new
        {
            x.ModuloId,
            x.EntidadId,
            x.EsPrincipal
        });

        builder.HasIndex(x => new
        {
            x.ModuloId,
            x.EntidadId,
            x.TipoUsoId
        });
    }
}