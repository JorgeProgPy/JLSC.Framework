using JLSC.Framework.Domain.Core.Portal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Portal;

public sealed class ConfiguracionInstitucionalConfiguration
    : IEntityTypeConfiguration<ConfiguracionInstitucional>
{
    public void Configure(
        EntityTypeBuilder<ConfiguracionInstitucional> builder)
    {
        builder.ToTable(
            "ConfiguracionInstitucional",
            "Portal");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.Property(x => x.NombreInstitucion)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.NombreCorto)
            .HasMaxLength(100);

        builder.Property(x => x.Descripcion)
            .HasMaxLength(1000);

        builder.Property(x => x.Eslogan)
            .HasMaxLength(300);

        builder.Property(x => x.LogoPrincipalArchivoId);

        builder.Property(x => x.LogoSecundarioArchivoId);

        builder.Property(x => x.FaviconArchivoId);

        builder.Property(x => x.Telefono)
            .HasMaxLength(100);

        builder.Property(x => x.Correo)
            .HasMaxLength(200);

        builder.Property(x => x.Direccion)
            .HasMaxLength(300);

        builder.Property(x => x.Activo)
            .IsRequired();

        builder.Property(x => x.FechaCreacion)
            .IsRequired();

        builder.Property(x => x.FechaModificacion);

        builder.HasOne(x => x.LogoPrincipalArchivo)
            .WithMany()
            .HasForeignKey(x => x.LogoPrincipalArchivoId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.LogoSecundarioArchivo)
            .WithMany()
            .HasForeignKey(x => x.LogoSecundarioArchivoId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.FaviconArchivo)
            .WithMany()
            .HasForeignKey(x => x.FaviconArchivoId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.Activo);
    }
}