namespace JLSC.Framework.Application.Core.Seguridad.Commands.AsignarPermisoRol;

public sealed class AsignarPermisoRolRequest
{
    public long RolId { get; init; }

    public long PermisoId { get; init; }
}