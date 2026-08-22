namespace JLSC.Framework.Application.Core.Archivos.Queries.ObtenerArchivosPorEntidad;

public sealed class ObtenerArchivosPorEntidadResponse
{
    public long ArchivoReferenciaId { get; init; }

    public long ArchivoId { get; init; }

    public long ModuloId { get; init; }

    public long EntidadId { get; init; }

    public long TipoUsoId { get; init; }

    public string Nombre { get; init; } = string.Empty;

    public string NombreOriginal { get; init; } = string.Empty;

    public string Extension { get; init; } = string.Empty;

    public string MimeType { get; init; } = string.Empty;

    public long TamanoBytes { get; init; }

    public string Hash { get; init; } = string.Empty;

    public bool EsPublico { get; init; }

    public int Orden { get; init; }

    public bool EsPrincipal { get; init; }

    public bool Activo { get; init; }

    public string? Observacion { get; init; }
}