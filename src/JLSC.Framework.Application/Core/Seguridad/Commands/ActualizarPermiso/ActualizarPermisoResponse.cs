namespace JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarPermiso;

public sealed class ActualizarPermisoResponse
{
    public long PermisoId { get; init; }

    public long ModuloId { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public int Orden { get; init; }

    public bool Activo { get; init; }


}