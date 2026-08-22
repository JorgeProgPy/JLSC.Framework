namespace JLSC.Framework.Application.Core.Archivos.Queries.ObtenerArchivosPorEntidad;

public sealed class ObtenerArchivosPorEntidadRequest
{
    public long ModuloId { get; init; }

    public long EntidadId { get; init; }
}