namespace JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerModuloPorId;

public sealed class ObtenerModuloPorIdResponse
{
    public long Id { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public string? Icono { get; init; }

    public string? Color { get; init; }

    public int Orden { get; init; }

    public bool Visible { get; init; }

    public bool Activo { get; init; }
}