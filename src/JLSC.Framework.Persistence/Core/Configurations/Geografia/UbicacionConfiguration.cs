using JLSC.Framework.Domain.Core.Geografia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Geografia;

public class UbicacionConfiguration : IEntityTypeConfiguration<Ubicacion>
{
    public void Configure(EntityTypeBuilder<Ubicacion> builder)
    {
        builder.ToTable("Ubicacion", "core");

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
            x.PaisId,
            x.DepartamentoId,
            x.ProvinciaId,
            x.CiudadId
        });

        // ==========================
        // Dirección
        // ==========================

        builder.Property(x => x.Urbanizacion)
            .HasMaxLength(150);

        builder.Property(x => x.Barrio)
            .HasMaxLength(150);

        builder.Property(x => x.Zona)
            .HasMaxLength(150);

        builder.Property(x => x.Avenida)
            .HasMaxLength(150);

        builder.Property(x => x.Calle)
            .HasMaxLength(150);

        builder.Property(x => x.NumeroPuerta)
            .HasMaxLength(30);

        builder.Property(x => x.Edificio)
            .HasMaxLength(150);

        builder.Property(x => x.Piso)
            .HasMaxLength(20);

        builder.Property(x => x.Oficina)
            .HasMaxLength(20);

        builder.Property(x => x.CodigoPostal)
            .HasMaxLength(20);

        builder.Property(x => x.Referencia)
            .HasMaxLength(500);

        // ==========================
        // Geolocalización
        // ==========================

        builder.Property(x => x.Latitud);

        builder.Property(x => x.Longitud);

        // ==========================
        // Sistema
        // ==========================

        builder.Property(x => x.Activo)
            .HasDefaultValue(true);

        // ==========================
        // Relaciones
        // ==========================

        builder.HasOne(x => x.Pais)
            .WithMany()
            .HasForeignKey(x => x.PaisId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Departamento)
            .WithMany()
            .HasForeignKey(x => x.DepartamentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Provincia)
            .WithMany()
            .HasForeignKey(x => x.ProvinciaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Ciudad)
            .WithMany()
            .HasForeignKey(x => x.CiudadId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}