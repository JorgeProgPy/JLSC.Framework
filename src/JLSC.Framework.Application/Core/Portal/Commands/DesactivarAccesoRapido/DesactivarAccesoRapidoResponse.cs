namespace JLSC.Framework.Application.Core.Portal.Commands.DesactivarAccesoRapido;

public sealed class DesactivarAccesoRapidoResponse
{
    public long AccesoRapidoId { get; init; }
    public bool Activo { get; init; }
}