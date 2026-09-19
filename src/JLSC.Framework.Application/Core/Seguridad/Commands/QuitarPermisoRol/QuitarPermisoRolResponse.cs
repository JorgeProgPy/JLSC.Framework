namespace JLSC.Framework.Application.Core.Seguridad.Commands.QuitarPermisoRol;

public sealed class QuitarPermisoRolResponse
{
    public long RolId { get; init; }

    public long PermisoId { get; init; }

    public bool Activo { get; init; }
}