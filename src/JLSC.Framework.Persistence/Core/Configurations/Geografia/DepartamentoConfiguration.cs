using JLSC.Framework.Domain.Core.Geografia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Geografia;

public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder.ToTable("Departamento", "core");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.PaisId,
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

        builder.HasOne(x => x.Pais)
            .WithMany(x => x.Departamentos)
            .HasForeignKey(x => x.PaisId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}