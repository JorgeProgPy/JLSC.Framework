using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Common.Helpers;
using JLSC.Framework.Domain.Core.Seguridad.Constants;

namespace JLSC.Framework.Domain.Core.Seguridad.Entities;

/// <summary>
/// Representa la asignación de un permiso a un rol.
/// </summary>
public class RolPermiso : AuditableEntity
{
    // =====================================================
    // Constructor
    // =====================================================

    protected RolPermiso()
    {
    }

    // =====================================================
    // Propiedades
    // =====================================================

    /// <summary>
    /// Rol.
    /// </summary>
    public long RolId { get; private set; }

    /// <summary>
    /// Permiso.
    /// </summary>
    public long PermisoId { get; private set; }

    // =====================================================
    // Navegación
    // =====================================================

    public Rol Rol { get; private set; } = null!;

    public Permiso Permiso { get; private set; } = null!;

    // =====================================================
    // Factory
    // =====================================================

    public static RolPermiso Crear(
        long rolId,
        long permisoId)
    {
        var rolPermiso = new RolPermiso();

        rolPermiso.AsignarRol(rolId);

        rolPermiso.AsignarPermiso(permisoId);

        return rolPermiso;
    }

    // =====================================================
    // Métodos privados
    // =====================================================

    /// <summary>
    /// Asigna el rol.
    /// </summary>
    private void AsignarRol(long rolId)
    {
        DomainValidator.RequiredId(
            rolId,
            RolPermisoMessages.RolObligatorio);

        RolId = rolId;
    }

    /// <summary>
    /// Asigna el permiso.
    /// </summary>
    private void AsignarPermiso(long permisoId)
    {
        DomainValidator.RequiredId(
            permisoId,
            RolPermisoMessages.PermisoObligatorio);

        PermisoId = permisoId;
    }
}

