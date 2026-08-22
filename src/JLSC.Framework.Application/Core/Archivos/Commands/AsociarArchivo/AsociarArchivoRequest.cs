namespace JLSC.Framework.Application.Core.Archivos.Commands.AsociarArchivo;

public sealed class AsociarArchivoRequest
{
    public long ArchivoId { get; init; }

    public long ModuloId { get; init; }

    public long EntidadId { get; init; }

    public long TipoUsoId { get; init; }

    public int Orden { get; init; } = 1;

    public bool EsPrincipal { get; init; }

    public string? Observacion { get; init; }
}