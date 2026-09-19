namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerAccesosRapidos;

public sealed class ObtenerAccesosRapidosResponse
{
    public long AccesoRapidoId { get; init; }
    public Guid PublicId { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Icono { get; init; }
    public string Enlace { get; init; } = string.Empty;
    public int Orden { get; init; }
    public bool Activo { get; init; }
}