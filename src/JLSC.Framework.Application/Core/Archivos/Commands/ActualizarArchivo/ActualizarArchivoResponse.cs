namespace JLSC.Framework.Application.Core.Archivos.Commands.ActualizarArchivo;

public sealed class ActualizarArchivoResponse
{
    public long ArchivoId { get; init; }

    public string Mensaje { get; init; } = string.Empty;
}