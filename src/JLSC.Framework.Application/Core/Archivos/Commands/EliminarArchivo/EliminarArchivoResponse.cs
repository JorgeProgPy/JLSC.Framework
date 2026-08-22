namespace JLSC.Framework.Application.Core.Archivos.Commands.EliminarArchivo;

public sealed class EliminarArchivoResponse
{
    public long ArchivoId { get; init; }

    public string Mensaje { get; init; } = string.Empty;
}