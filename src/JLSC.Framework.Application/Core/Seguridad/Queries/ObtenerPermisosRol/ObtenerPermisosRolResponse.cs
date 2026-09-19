namespace JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerPermisosRol;

public sealed class ObtenerPermisosRolResponse
{
    public long PermisoId { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public long ModuloId { get; init; }

    public bool Activo { get; init; }
}
