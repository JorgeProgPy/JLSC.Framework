namespace JLSC.Framework.Application.Core.Archivos.Commands.AsociarArchivo;

public sealed class AsociarArchivoResponse
{
    public long ArchivoReferenciaId { get; init; }

    public long ArchivoId { get; init; }

    public long ModuloId { get; init; }

    public long EntidadId { get; init; }

    public long TipoUsoId { get; init; }

    public int Orden { get; init; }

    public bool EsPrincipal { get; init; }

    public bool Activo { get; init; }

    public string Mensaje { get; init; } = string.Empty;
}