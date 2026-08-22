namespace JLSC.Framework.Application.Core.Personas.Commands.CrearPersona;

public sealed class CrearPersonaResponse
{
    public long PersonaId { get; init; }

    public string NumeroDocumento { get; init; } = string.Empty;

    public string? Nombres { get; init; }

    public string? PrimerApellido { get; init; }

    public string? SegundoApellido { get; init; }

    public string? RazonSocial { get; init; }
}