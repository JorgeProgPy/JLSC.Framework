namespace JLSC.Framework.Application.Core.Personas.Queries.ObtenerPersonas;

public sealed class PersonaResumenResponse
{
    public long PersonaId { get; init; }

    public string TipoPersona { get; init; } = null!;

    public string TipoDocumento { get; init; } = null!;

    public string NumeroDocumento { get; init; } = null!;

    public string Nombre { get; init; } = null!;

    public bool Activo { get; init; }
}