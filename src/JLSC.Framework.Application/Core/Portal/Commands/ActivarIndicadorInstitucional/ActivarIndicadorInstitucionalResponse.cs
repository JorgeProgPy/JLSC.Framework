namespace JLSC.Framework.Application.Core.Portal.Commands.ActivarIndicadorInstitucional;

public sealed class ActivarIndicadorInstitucionalResponse
{
    public long IndicadorId { get; init; }

    public Guid PublicId { get; init; }

    public string Titulo { get; init; } = string.Empty;

    public bool Activo { get; init; }
}