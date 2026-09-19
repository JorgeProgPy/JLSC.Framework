namespace JLSC.Framework.Application.Core.Portal.Commands.CrearProyectoDestacado;

public sealed class CrearProyectoDestacadoRequest
{
    public string Titulo { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Ubicacion { get; init; }
    public long? ArchivoId { get; init; }
    public string? Enlace { get; init; }
    public int Orden { get; init; }
}