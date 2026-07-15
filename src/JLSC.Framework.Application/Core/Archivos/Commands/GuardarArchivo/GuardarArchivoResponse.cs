namespace JLSC.Framework.Application.Core.Archivos.Commands.GuardarArchivo;

public sealed class GuardarArchivoResponse
{
    public long ArchivoId { get; init; }

    public string Nombre { get; init; } = string.Empty;

    public string NombreOriginal { get; init; } = string.Empty;

    public string Extension { get; init; } = string.Empty;

    public string MimeType { get; init; } = string.Empty;

    public long TamanoBytes { get; init; }

    public string Hash { get; init; } = string.Empty;

    public bool EsPublico { get; init; }
}