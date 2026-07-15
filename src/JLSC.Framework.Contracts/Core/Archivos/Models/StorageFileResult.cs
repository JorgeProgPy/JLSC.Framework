namespace JLSC.Framework.Contracts.Core.Archivos.Models;

public sealed class StorageFileResult
{
    public string NombreOriginal { get; init; } = null!;

    public string NombreAlmacenado { get; init; } = null!;

    public string ClaveAlmacenamiento { get; init; } = null!;

    public string Extension { get; init; } = null!;

    public string MimeType { get; init; } = null!;

    public long TamanoBytes { get; init; }

    public string Hash { get; init; } = null!;
}