namespace JLSC.Framework.Application.Core.Portal.Commands.CrearProyectoDestacado;

public sealed class CrearProyectoDestacadoResponse
{
    public long ProyectoDestacadoId { get; init; }
    public Guid PublicId { get; init; }
    public string Titulo { get; init; } = string.Empty;
}