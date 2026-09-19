namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarProyectoDestacado;

public sealed class ActualizarProyectoDestacadoResponse
{
    public long ProyectoDestacadoId { get; init; }
    public Guid PublicId { get; init; }
    public string Titulo { get; init; } = string.Empty;
}