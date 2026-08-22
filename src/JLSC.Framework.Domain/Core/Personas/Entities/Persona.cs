using JLSC.Framework.Domain.Core.Personas.Constants;
using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Common.Helpers;

using JLSC.Framework.Domain.Core.Catalogos.Entities;

namespace JLSC.Framework.Domain.Core.Personas.Entities;

/// <summary>
/// Representa una persona natural o jurídica del sistema.
/// </summary>
public class Persona : AuditableEntity
{
    protected Persona()
    {
    }

    // ==========================
    // Constantes
    // ==========================

    public const int MaxNumeroDocumentoLength = 30;
    public const int MaxComplementoLength = 20;
    public const int MaxNitLength = 30;

    public const int MaxNombresLength = 150;
    public const int MaxPrimerApellidoLength = 100;
    public const int MaxSegundoApellidoLength = 100;

    public const int MaxRazonSocialLength = 250;
    public const int MaxNombreComercialLength = 250;
    public const int MaxSiglaLength = 50;

    public const int MaxObservacionLength = 1000;

    // ==========================
    // Propiedades Calculadas
    // ==========================



    // ==========================
    // Identificación
    // ==========================

    public long TipoPersonaId { get; private set; }

    public long TipoDocumentoId { get; private set; }

    public string NumeroDocumento { get; private set; } = string.Empty;

    public string? Complemento { get; private set; }

    public string? Nit { get; private set; }

    // ==========================
    // Persona Natural
    // ==========================

    public string? Nombres { get; private set; }

    public string? PrimerApellido { get; private set; }

    public string? SegundoApellido { get; private set; }

    public DateOnly? FechaNacimiento { get; private set; }

    public long? GeneroId { get; private set; }

    public long? EstadoCivilId { get; private set; }

    // ==========================
    // Persona Jurídica
    // ==========================

    public string? RazonSocial { get; private set; }

    public string? NombreComercial { get; private set; }

    public string? Sigla { get; private set; }

    // ==========================
    // Información General
    // ==========================

    public string? Observacion { get; private set; }

    // ==========================
    // Navegación
    // ==========================

    public virtual CatalogoItem TipoPersona { get; private set; } = null!;

    public virtual CatalogoItem TipoDocumento { get; private set; } = null!;

    public virtual CatalogoItem? Genero { get; private set; }

    public virtual CatalogoItem? EstadoCivil { get; private set; }

    // Se habilitarán cuando existan las entidades correspondientes.

    // public virtual ICollection<PersonaContacto> Contactos { get; private set; }
    //     = new List<PersonaContacto>();

    // public virtual ICollection<PersonaDireccion> Direcciones { get; private set; }
    //     = new List<PersonaDireccion>();

    // public virtual ICollection<PersonaRol> Roles { get; private set; }
    //     = new List<PersonaRol>();

    // ==========================
    // Fábrica
    // ==========================

    public static Persona Create(

    long tipoPersonaId,
    long tipoDocumentoId,
    string numeroDocumento)
    {
        var persona = new Persona();

        persona.CambiarTipoPersona(tipoPersonaId);

        persona.CambiarDocumento(
            tipoDocumentoId,
            numeroDocumento);

        persona.Activar();

        return persona;
    }
    // -----------------------------------------------------------------
    // Comportamiento
    // -----------------------------------------------------------------

    public void CambiarTipoPersona(long tipoPersonaId)
    {
        DomainValidator.RequiredId(
            tipoPersonaId,
            PersonaMessages.TipoPersonaObligatorio);

        TipoPersonaId = tipoPersonaId;
    }

    public void CambiarDocumento(
        long tipoDocumentoId,
        string numeroDocumento)
    {
        DomainValidator.RequiredId(
            tipoDocumentoId,
            PersonaMessages.TipoDocumentoObligatorio);

        numeroDocumento = TextNormalizer.NormalizeRequired(
            numeroDocumento,
            PersonaMessages.NumeroDocumentoObligatorio);

        DomainValidator.MaxLength(
            numeroDocumento,
            MaxNumeroDocumentoLength,
            PersonaMessages.NumeroDocumentoLongitudMaxima);

        TipoDocumentoId = tipoDocumentoId;
        NumeroDocumento = numeroDocumento;
    }

    public void CambiarComplemento(string? complemento)
    {
        complemento = TextNormalizer.NormalizeOptional(complemento);

        DomainValidator.MaxLength(
            complemento,
            MaxComplementoLength,
            PersonaMessages.ComplementoLongitudMaxima);

        Complemento = complemento;
    }

    public void CambiarNit(string? nit)
    {
        nit = TextNormalizer.NormalizeOptional(nit);

        DomainValidator.MaxLength(
            nit,
            MaxNitLength,
            PersonaMessages.NitLongitudMaxima);

        Nit = nit;
    }

    public void CambiarDatosNaturales(
        string nombres,
        string primerApellido,
        string? segundoApellido,
        DateOnly? fechaNacimiento,
        long? generoId,
        long? estadoCivilId)
    {
        nombres = TextNormalizer.NormalizeRequired(
            nombres,
            PersonaMessages.NombresObligatorios);

        primerApellido = TextNormalizer.NormalizeRequired(
            primerApellido,
            PersonaMessages.PrimerApellidoObligatorio);

        segundoApellido = TextNormalizer.NormalizeOptional(segundoApellido);

        DomainValidator.MaxLength(
            nombres,
            MaxNombresLength,
            PersonaMessages.NombresLongitudMaxima);

        DomainValidator.MaxLength(
            primerApellido,
            MaxPrimerApellidoLength,
            PersonaMessages.PrimerApellidoLongitudMaxima);

        DomainValidator.MaxLength(
            segundoApellido,
            MaxSegundoApellidoLength,
            PersonaMessages.SegundoApellidoLongitudMaxima);

        DomainValidator.BirthDate(
            fechaNacimiento
            );

        DomainValidator.OptionalId(
            generoId,
            PersonaMessages.GeneroInvalido);

        DomainValidator.OptionalId(
            estadoCivilId,
            PersonaMessages.EstadoCivilInvalido);

        LimpiarDatosPersonaJuridica();

        Nombres = nombres;
        PrimerApellido = primerApellido;
        SegundoApellido = segundoApellido;
        FechaNacimiento = fechaNacimiento;
        GeneroId = generoId;
        EstadoCivilId = estadoCivilId;
    }

    public void CambiarDatosJuridicos(
        string razonSocial,
        string? nombreComercial,
        string? sigla)
    {
        razonSocial = TextNormalizer.NormalizeRequired(
            razonSocial,
            PersonaMessages.RazonSocialObligatoria);

        nombreComercial = TextNormalizer.NormalizeOptional(nombreComercial);

        sigla = TextNormalizer.NormalizeOptional(sigla);

        DomainValidator.MaxLength(
            razonSocial,
            MaxRazonSocialLength,
            PersonaMessages.RazonSocialLongitudMaxima);

        DomainValidator.MaxLength(
            nombreComercial,
            MaxNombreComercialLength,
            PersonaMessages.NombreComercialLongitudMaxima);

        DomainValidator.MaxLength(
            sigla,
            MaxSiglaLength,
            PersonaMessages.SiglaLongitudMaxima);

        LimpiarDatosPersonaNatural();

        RazonSocial = razonSocial;
        NombreComercial = nombreComercial;
        Sigla = sigla;
    }

    public void CambiarObservacion(string? observacion)
    {
        observacion = TextNormalizer.NormalizeOptional(observacion);

        DomainValidator.MaxLength(
            observacion,
            MaxObservacionLength,
            PersonaMessages.ObservacionLongitudMaxima);

        Observacion = observacion;
    }

    // ==========================
    // Métodos Privados
    // ==========================

    private void LimpiarDatosPersonaNatural()
    {
        Nombres = null;
        PrimerApellido = null;
        SegundoApellido = null;
        FechaNacimiento = null;
        GeneroId = null;
        EstadoCivilId = null;
    }

    private void LimpiarDatosPersonaJuridica()
    {
        RazonSocial = null;
        NombreComercial = null;
        Sigla = null;
    }

    // ==========================
    // Validaciones
    // ==========================

    private static void ValidarId(
        long id,
        string mensaje)
    {
        if (id <= 0)
            throw new ArgumentException(mensaje);
    }

    private static void ValidarIdOpcional(
        long? id,
        string mensaje)
    {
        if (id.HasValue && id.Value <= 0)
            throw new ArgumentException(mensaje);
    }

    private static void ValidarLongitud(
        string? valor,
        int longitudMaxima,
        string mensaje)
    {
        if (!string.IsNullOrWhiteSpace(valor) &&
            valor.Length > longitudMaxima)
        {
            throw new ArgumentException(mensaje);
        }
    }

    private static void ValidarFechaNacimiento(
        DateOnly? fechaNacimiento)
    {
        if (!fechaNacimiento.HasValue)
            return;

        var hoy = DateOnly.FromDateTime(DateTime.Today);

        if (fechaNacimiento.Value > hoy)
            throw new ArgumentException(
                "La fecha de nacimiento no puede ser mayor a la fecha actual.");

        if (fechaNacimiento.Value < new DateOnly(1900, 1, 1))
            throw new ArgumentException(
                "La fecha de nacimiento no es válida.");
    }

    // ==========================
    // Normalización
    // ==========================

   
}