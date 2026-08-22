namespace JLSC.Framework.Application.Core.Archivos.Queries.ObtenerArchivo;

public sealed class ObtenerArchivoResponse
{
    public long ArchivoId { get; init; }

    public string? Codigo { get; init; }

    public string Nombre { get; init; } = null!;

    public string NombreOriginal { get; init; } = null!;

    public string Extension { get; init; } = null!;

    public string MimeType { get; init; } = null!;

    public long TamanoBytes { get; init; }

    public string? Descripcion { get; init; }

    public bool EsPublico { get; init; }

    public string Carpeta { get; init; } = null!;

    public string TipoArchivo { get; init; } = null!;

    public string EstadoArchivo { get; init; } = null!;

    public string ProveedorAlmacenamiento { get; init; } = null!;
}