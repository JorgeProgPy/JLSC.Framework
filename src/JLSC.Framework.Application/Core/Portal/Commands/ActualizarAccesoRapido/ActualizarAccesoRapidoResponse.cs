namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarAccesoRapido;

public sealed class ActualizarAccesoRapidoResponse
{
    public long AccesoRapidoId { get; init; }
    public Guid PublicId { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public string Enlace { get; init; } = string.Empty;
}