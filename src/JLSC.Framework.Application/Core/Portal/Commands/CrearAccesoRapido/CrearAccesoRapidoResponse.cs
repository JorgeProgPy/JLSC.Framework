namespace JLSC.Framework.Application.Core.Portal.Commands.CrearAccesoRapido;

public sealed class CrearAccesoRapidoResponse
{
    public long AccesoRapidoId { get; init; }
    public Guid PublicId { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public string Enlace { get; init; } = string.Empty;
}