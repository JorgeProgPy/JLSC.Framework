using JLSC.Framework.Domain.Core.Archivos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JLSC.Framework.Domain.Core.Personas.Entities;

namespace JLSC.Framework.Persistence.Core.Configurations.Archivos;

public class ArchivoConfiguration : IEntityTypeConfiguration<Archivo>
{
    public void Configure(EntityTypeBuilder<Archivo> builder)
    {
        builder.ToTable("Archivo", "core");

        // ==========================
        // Clave primaria
        // ==========================

        builder.HasKey(x => x.Id);

        // ==========================
        // Índices
        // ==========================

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => x.Codigo);

        builder.HasIndex(x => x.ClaveAlmacenamiento)
            .IsUnique();

        builder.HasIndex(x => x.Hash);

        builder.HasIndex(x => x.CarpetaArchivoId);

        builder.HasIndex(x => x.TipoArchivoId);

        builder.HasIndex(x => x.EstadoArchivoId);

        builder.HasIndex(x => x.ProveedorAlmacenamientoId);

        builder.HasIndex(x => x.ArchivoAnteriorId);

        // ==========================
        // Identificación
        // ==========================

        builder.Property(x => x.Codigo)
            .HasMaxLength(50);

        builder.Property(x => x.Nombre)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.NombreOriginal)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.NombreAlmacenado)
            .HasMaxLength(255)
            .IsRequired();

        // ==========================
        // Almacenamiento
        // ==========================

        builder.Property(x => x.ClaveAlmacenamiento)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Extension)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.MimeType)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.TamanoBytes)
            .IsRequired();

        builder.Property(x => x.Hash)
            .HasMaxLength(64)
            .IsRequired();

        // ==========================
        // Configuración
        // ==========================

        builder.Property(x => x.EsPublico)
            .HasDefaultValue(false);

        builder.Property(x => x.Descripcion)
            .HasMaxLength(1000);

        builder.Property(x => x.Activo)
            .HasDefaultValue(true);

        // ==========================
        // Relación con CarpetaArchivo
        // ==========================

        builder.HasOne(x => x.CarpetaArchivo)
            .WithMany(x => x.Archivos)
            .HasForeignKey(x => x.CarpetaArchivoId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==========================
        // Relación con TipoArchivo
        // ==========================

        builder.HasOne(x => x.TipoArchivo)
            .WithMany()
            .HasForeignKey(x => x.TipoArchivoId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==========================
        // Relación con EstadoArchivo
        // ==========================

        builder.HasOne(x => x.EstadoArchivo)
            .WithMany()
            .HasForeignKey(x => x.EstadoArchivoId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==========================
        // Relación con ProveedorAlmacenamiento
        // ==========================

        builder.HasOne(x => x.ProveedorAlmacenamiento)
            .WithMany()
            .HasForeignKey(x => x.ProveedorAlmacenamientoId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==========================
        // Propietario
        // ==========================

        builder.HasOne<Persona>()
            .WithMany()
            .HasForeignKey(x => x.PropietarioPersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==========================
        // Versionado
        // ==========================

        builder.HasOne(x => x.ArchivoAnterior)
            .WithMany(x => x.Versiones)
            .HasForeignKey(x => x.ArchivoAnteriorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}