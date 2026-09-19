namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarIndicadorInstitucional;

public sealed class ActualizarIndicadorInstitucionalResponse
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