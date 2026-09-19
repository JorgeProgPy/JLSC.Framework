namespace JLSC.Framework.Application.Core.Seguridad.Commands.QuitarPermisoRol;

public sealed class QuitarPermisoRolRequest
{
    public long RolId { get; init; }

    public long PermisoId { get; init; }
}