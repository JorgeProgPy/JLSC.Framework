namespace JLSC.Framework.Application.Core.Portal.Commands.DesactivarProyectoDestacado;

public sealed class DesactivarProyectoDestacadoResponse
{
    public long ProyectoDestacadoId { get; init; }
    public bool Activo { get; init; }
}