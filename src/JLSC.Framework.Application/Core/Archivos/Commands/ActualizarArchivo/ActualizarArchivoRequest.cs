namespace JLSC.Framework.Application.Core.Archivos.Commands.ActualizarArchivo;

public sealed class ActualizarArchivoRequest
{
    public long ArchivoId { get; init; }

    public string CodigoCarpeta { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public bool EsPublico { get; init; }

   
}