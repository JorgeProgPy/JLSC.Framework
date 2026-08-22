namespace JLSC.Framework.Application.Core.Personas.Queries.ObtenerPersona;

public sealed class ObtenerPersonaResponse
{
    public long PersonaId { get; init; }

    public string TipoPersona { get; init; } = null!;

    public string TipoDocumento { get; init; } = null!;

    public string NumeroDocumento { get; init; } = null!;

    public string? Complemento { get; init; }

    public string? Nit { get; init; }

    public string? Nombres { get; init; }

    public string? PrimerApellido { get; init; }

    public string? SegundoApellido { get; init; }

    public DateOnly? FechaNacimiento { get; init; }

    public string? Genero { get; init; }

    public string? EstadoCivil { get; init; }

    public string? RazonSocial { get; init; }

    public string? NombreComercial { get; init; }

    public string? Sigla { get; init; }

    public string? Observacion { get; init; }

    public bool Activo { get; init; }
}