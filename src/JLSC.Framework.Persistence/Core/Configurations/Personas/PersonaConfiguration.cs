using JLSC.Framework.Domain.Core.Personas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLSC.Framework.Persistence.Core.Configurations;

public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        // ==========================
        // Tabla
        // ==========================

        builder.ToTable("Personas");

        // ==========================
        // Clave Primaria
        // ==========================

        builder.HasKey(x => x.Id);

        // ==========================
        // Propiedades Base
        // ==========================

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.PublicId)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Activo)
            .IsRequired();

        builder.Property(x => x.FechaCreacion)
            .IsRequired();

        builder.Property(x => x.FechaModificacion);

        builder.Property(x => x.UsuarioCreacionId);

        builder.Property(x => x.UsuarioModificacionId);

        // ==========================
        // Identificación
        // ==========================

        builder.Property(x => x.TipoPersonaId)
            .IsRequired();

        builder.Property(x => x.TipoDocumentoId)
            .IsRequired();

        builder.Property(x => x.NumeroDocumento)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Complemento)
            .HasMaxLength(20);

        builder.Property(x => x.Nit)
            .HasMaxLength(30);


        // ==========================
        // Persona Natural
        // ==========================

        builder.Property(x => x.Nombres)
            .HasMaxLength(Persona.MaxNombresLength);

        builder.Property(x => x.PrimerApellido)
            .HasMaxLength(Persona.MaxPrimerApellidoLength);

        builder.Property(x => x.SegundoApellido)
            .HasMaxLength(Persona.MaxSegundoApellidoLength);

        builder.Property(x => x.FechaNacimiento);

        builder.Property(x => x.GeneroId);

        builder.Property(x => x.EstadoCivilId);

        // ==========================
        // Persona Jurídica
        // ==========================

        builder.Property(x => x.RazonSocial)
            .HasMaxLength(Persona.MaxRazonSocialLength);

        builder.Property(x => x.NombreComercial)
            .HasMaxLength(Persona.MaxNombreComercialLength);

        builder.Property(x => x.Sigla)
            .HasMaxLength(Persona.MaxSiglaLength);

        // ==========================
        // Información General
        // ==========================

        builder.Property(x => x.Observacion)
            .HasMaxLength(Persona.MaxObservacionLength);

        // ==========================
        // Índices
        // ==========================

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasIndex(x => x.PublicId)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.TipoDocumentoId,
            x.NumeroDocumento,
            x.Complemento
        })
        .IsUnique();

        builder.HasIndex(x => x.Nit);

        builder.HasIndex(x => x.TipoPersonaId);

        builder.HasIndex(x => x.GeneroId);

        builder.HasIndex(x => x.EstadoCivilId);

        // ==========================
        // Relaciones
        // ==========================

        builder.HasOne(x => x.TipoPersona)
            .WithMany()
            .HasForeignKey(x => x.TipoPersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TipoDocumento)
            .WithMany()
            .HasForeignKey(x => x.TipoDocumentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Genero)
            .WithMany()
            .HasForeignKey(x => x.GeneroId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EstadoCivil)
            .WithMany()
            .HasForeignKey(x => x.EstadoCivilId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==========================
        // Relaciones Futuras
        // ==========================

        /*
        builder.HasMany(x => x.Contactos)
            .WithOne(x => x.Persona)
            .HasForeignKey(x => x.PersonaId);

        builder.HasMany(x => x.Direcciones)
            .WithOne(x => x.Persona)
            .HasForeignKey(x => x.PersonaId);

        builder.HasMany(x => x.Roles)
            .WithOne(x => x.Persona)
            .HasForeignKey(x => x.PersonaId);
        */
    }
}