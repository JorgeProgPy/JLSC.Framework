namespace JLSC.Framework.Application.Core.Personas.Queries.ObtenerPersonas;

public sealed class ObtenerPersonasResponse
{
    public IReadOnlyCollection<PersonaResumenResponse> Personas { get; init; }
        = [];

    public int Pagina { get; init; }

    public int TamanoPagina { get; init; }

    public int TotalRegistros { get; init; }

    public int TotalPaginas { get; init; }
}