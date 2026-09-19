namespace JLSC.Framework.Application.Core.Portal.Commands.CrearAccesoRapido;

public sealed class CrearAccesoRapidoRequest
{
    public string Titulo { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Icono { get; init; }
    public string Enlace { get; init; } = string.Empty;
    public int Orden { get; init; }
}