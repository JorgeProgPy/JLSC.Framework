namespace JLSC.Framework.Application.Core.Personas.Commands.CrearPersona;

// ==========================
// Solicitud Crear Persona
// ==========================

public sealed class CrearPersonaRequest
{
    // ==========================
    // Identificación
    // ==========================

    public string TipoPersonaCodigo { get; init; } = string.Empty;

    public string TipoDocumentoCodigo { get; init; } = string.Empty;

    public string NumeroDocumento { get; init; } = string.Empty;

    public string? Complemento { get; init; }

    public string? Nit { get; init; }

    // ==========================
    // Persona Natural
    // ==========================

    public string? Nombres { get; init; }

    public string? PrimerApellido { get; init; }

    public string? SegundoApellido { get; init; }

    public DateOnly? FechaNacimiento { get; init; }

    public string? GeneroCodigo { get; init; }

    public string? EstadoCivilCodigo { get; init; }

    // ==========================
    // Persona Jurídica
    // ==========================

    public string? RazonSocial { get; init; }

    public string? NombreComercial { get; init; }

    public string? Sigla { get; init; }

    // ==========================
    // Información General
    // ==========================

    public string? Observacion { get; init; }
}