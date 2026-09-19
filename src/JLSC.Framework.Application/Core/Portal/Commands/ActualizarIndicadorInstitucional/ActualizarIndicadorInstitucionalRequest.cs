namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarIndicadorInstitucional;

public sealed class ActualizarIndicadorInstitucionalRequest
{
    public long IndicadorId { get; set; }
    public string Titulo { get; init; } = string.Empty;
    public string Valor { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Icono { get; init; }
    public int Orden { get; init; }
}