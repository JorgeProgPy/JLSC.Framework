namespace JLSC.Framework.Application.Core.Seguridad.Commands.EliminarPermiso;

public sealed class EliminarPermisoResponse
{
    public long PermisoId { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public bool Activo { get; init; }
}