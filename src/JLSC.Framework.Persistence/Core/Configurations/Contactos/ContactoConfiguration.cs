using JLSC.Framework.Domain.Core.Contactos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations.Contactos;

public class ContactoConfiguration : IEntityTypeConfiguration<Contacto>
{
    public void Configure(EntityTypeBuilder<Contacto> builder)
    {
        builder.ToTable("Contacto", "core");

        // ==========================
        // Clave primaria
        // ==========================

        builder.HasKey(x => x.Id);

        // ==========================
        // Índices
        // ==========================

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        // ==========================
        // Sistema
        // ==========================

        builder.Property(x => x.Activo)
            .HasDefaultValue(true);

        // ==========================
        // Relaciones
        // ==========================

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Contacto)
            .HasForeignKey(x => x.ContactoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}