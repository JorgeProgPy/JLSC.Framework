namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerProyectoDestacado;

public sealed class ObtenerProyectoDestacadoResponse
{
    public long ProyectoDestacadoId { get; init; }
    public Guid PublicId { get; init; }

    public string Titulo { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Ubicacion { get; init; }

    public long? ArchivoId { get; init; }
    public string? Enlace { get; init; }

    public int Orden { get; init; }
    public bool Activo { get; init; }
}