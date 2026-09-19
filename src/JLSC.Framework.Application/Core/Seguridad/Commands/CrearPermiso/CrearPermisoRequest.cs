namespace JLSC.Framework.Application.Core.Seguridad.Commands.CrearPermiso;

public sealed class CrearPermisoRequest
{
    public long ModuloId { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public int Orden { get; init; }
}