using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Common.Helpers;
using JLSC.Framework.Domain.Core.Seguridad.Constants;


namespace JLSC.Framework.Domain.Core.Seguridad.Entities;

/// <summary>
/// Representa la asignación de un rol a un usuario.
/// </summary>
public class UsuarioRol : AuditableEntity
{
    // =====================================================
    // Constructor
    // =====================================================

    protected UsuarioRol()
    {
    }

    // =====================================================
    // Propiedades
    // =====================================================

    /// <summary>
    /// Usuario.
    /// </summary>
    public long UsuarioId { get; private set; }

    /// <summary>
    /// Rol.
    /// </summary>
    public long RolId { get; private set; }

    // =====================================================
    // Navegación
    // =====================================================

    public Usuario Usuario { get; private set; } = null!;

    public Rol Rol { get; private set; } = null!;

    // =====================================================
    // Factory
    // =====================================================

    public static UsuarioRol Crear(
        long usuarioId,
        long rolId)
    {
        var usuarioRol = new UsuarioRol();

        usuarioRol.AsignarUsuario(usuarioId);

        usuarioRol.AsignarRol(rolId);

        return usuarioRol;
    }

    // =====================================================
    // Métodos privados
    // =====================================================

    /// <summary>
    /// Asigna el usuario.
    /// </summary>
    private void AsignarUsuario(long usuarioId)
    {
        DomainValidator.RequiredId(
            usuarioId,
            UsuarioRolMessages.UsuarioObligatorio);

        UsuarioId = usuarioId;
    }

    /// <summary>
    /// Asigna el rol.
    /// </summary>
    private void AsignarRol(long rolId)
    {
        DomainValidator.RequiredId(
            rolId,
            UsuarioRolMessages.RolObligatorio);

        RolId = rolId;
    }
}