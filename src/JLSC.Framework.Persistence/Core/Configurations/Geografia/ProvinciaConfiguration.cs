using JLSC.Framework.Domain.Core.Geografia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Geografia;

public class ProvinciaConfiguration : IEntityTypeConfiguration<Provincia>
{
    public void Configure(EntityTypeBuilder<Provincia> builder)
    {
        builder.ToTable("Provincia", "core");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.DepartamentoId,
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

        builder.HasOne(x => x.Departamento)
            .WithMany(x => x.Provincias)
            .HasForeignKey(x => x.DepartamentoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}