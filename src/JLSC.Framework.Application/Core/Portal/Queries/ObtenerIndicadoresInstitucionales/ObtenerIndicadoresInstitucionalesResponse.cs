namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerIndicadoresInstitucionales;

public sealed class ObtenerIndicadoresInstitucionalesResponse
{
    public long IndicadorId { get; init; }

    public Guid PublicId { get; init; }

    public string Titulo { get; init; } = string.Empty;

    public string Valor { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public string? Icono { get; init; }

    public int Orden { get; init; }

    public bool Activo { get; init; }
}