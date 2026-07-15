namespace JLSC.Framework.Domain.Core.Archivos.ValueObjects;

public sealed class ArchivoStorageInfo
{
    public ArchivoStorageInfo(
        string nombreOriginal,
        string nombreAlmacenado,
        string claveAlmacenamiento,
        string extension,
        string mimeType,
        long tamanoBytes,
        string hash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nombreOriginal);
        ArgumentException.ThrowIfNullOrWhiteSpace(nombreAlmacenado);
        ArgumentException.ThrowIfNullOrWhiteSpace(claveAlmacenamiento);
        ArgumentException.ThrowIfNullOrWhiteSpace(extension);
        ArgumentException.ThrowIfNullOrWhiteSpace(mimeType);
        ArgumentException.ThrowIfNullOrWhiteSpace(hash);

        NombreOriginal = nombreOriginal;
        NombreAlmacenado = nombreAlmacenado;
        ClaveAlmacenamiento = claveAlmacenamiento;
        Extension = extension;
        MimeType = mimeType;
        TamanoBytes = tamanoBytes;
        Hash = hash;
    }

    public string NombreOriginal { get; }

    public string NombreAlmacenado { get; }

    public string ClaveAlmacenamiento { get; }

    public string Extension { get; }

    public string MimeType { get; }

    public long TamanoBytes { get; }

    public string Hash { get; }
}