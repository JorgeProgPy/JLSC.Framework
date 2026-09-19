namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarProyectoDestacado;

public sealed class ActualizarProyectoDestacadoRequest
{
    public long ProyectoDestacadoId { get; set; }

    public string Titulo { get; init; } = string.Empty;
    public string? Descripcion { get; init; }
    public string? Ubicacion { get; init; }
    public long? ArchivoId { get; init; }
    public string? Enlace { get; init; }
    public int Orden { get; init; }
}