namespace JLSC.Framework.Application.Core.Archivos.Commands.GuardarArchivo;

public sealed class GuardarArchivoRequest
{
    public Stream Contenido { get; init; } = null!;

    public string NombreOriginal { get; init; } = null!;

    public string CodigoCarpeta { get; init; } = null!;

    public string? Descripcion { get; init; }

    public bool EsPublico { get; init; }
}