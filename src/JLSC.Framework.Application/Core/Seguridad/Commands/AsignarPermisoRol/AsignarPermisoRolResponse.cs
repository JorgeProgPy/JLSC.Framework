namespace JLSC.Framework.Application.Core.Seguridad.Commands.AsignarPermisoRol;

public sealed class AsignarPermisoRolResponse
{
    public long RolId { get; init; }

    public long PermisoId { get; init; }

    public bool Activo { get; init; }
}