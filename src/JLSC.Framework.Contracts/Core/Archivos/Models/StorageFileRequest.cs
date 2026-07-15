namespace JLSC.Framework.Contracts.Core.Archivos.Models;

public sealed class StorageFileRequest
{
    public Stream Contenido { get; init; } = null!;

    public string NombreOriginal { get; init; } = null!;

    public string Carpeta { get; init; } = null!;
}