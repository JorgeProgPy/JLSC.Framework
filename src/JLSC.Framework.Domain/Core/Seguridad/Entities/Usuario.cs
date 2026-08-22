using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Common.Helpers;
using JLSC.Framework.Domain.Common.Interfaces;
using JLSC.Framework.Domain.Core.Personas.Entities;
using JLSC.Framework.Domain.Core.Seguridad.Constants;

namespace JLSC.Framework.Domain.Core.Seguridad.Entities;

/// <summary>
/// Representa una cuenta de acceso al sistema.
/// </summary>
public class Usuario : AuditableEntity, IAggregateRoot
{
    // =====================================================
    // Constantes
    // =====================================================

    public const int MaxNombreUsuarioLength = 100;

    public const int MaxPasswordHashLength = 500;

    // =====================================================
    // Constructor
    // =====================================================

    protected Usuario()
    {
    }

    // =====================================================
    // Propiedades
    // =====================================================

    /// <summary>
    /// Persona asociada.
    /// </summary>
    public long PersonaId { get; private set; }

    /// <summary>
    /// Nombre de usuario.
    /// </summary>
    public string NombreUsuario { get; private set; } = string.Empty;

    /// <summary>
    /// Hash de la contraseña.
    /// </summary>
    public string PasswordHash { get; private set; } = string.Empty;

    /// <summary>
    /// Último acceso.
    /// </summary>
    public DateTime? UltimoAcceso { get; private set; }

    /// <summary>
    /// Indica si debe cambiar su contraseña.
    /// </summary>
    public bool DebeCambiarPassword { get; private set; }

    /// <summary>
    /// Indica si el usuario está bloqueado.
    /// </summary>
    public bool Bloqueado { get; private set; }

    // =====================================================
    // Navegación
    // =====================================================

    public Persona Persona { get; private set; } = null!;

    // =====================================================
    // Factory
    // =====================================================

    public static Usuario Crear(
        long personaId,
        string codigo,
        string nombreUsuario,
        string passwordHash)
    {
        var usuario = new Usuario();

        DomainValidator.RequiredId(
            personaId,
            UsuarioMessages.PersonaObligatoria);

        usuario.PersonaId = personaId;

        usuario.AsignarCodigo(codigo);

        usuario.AsignarNombreUsuario(nombreUsuario);

        usuario.AsignarPasswordHash(passwordHash);

        usuario.DebeCambiarPassword = true;

        usuario.Bloqueado = false;

        return usuario;
    }

    // =====================================================
    // Comportamiento
    // =====================================================

    /// <summary>
    /// Cambia el nombre de usuario.
    /// </summary>
    public void CambiarNombreUsuario(string nombreUsuario)
    {
        AsignarNombreUsuario(nombreUsuario);
    }

    /// <summary>
    /// Cambia la contraseña del usuario.
    /// </summary>
    public void CambiarPassword(string passwordHash)
    {
        AsignarPasswordHash(passwordHash);

        DebeCambiarPassword = false;
    }

    /// <summary>
    /// Obliga al usuario a cambiar su contraseña.
    /// </summary>
    public void ObligarCambioPassword()
    {
        DebeCambiarPassword = true;
    }

    /// <summary>
    /// Bloquea el usuario.
    /// </summary>
    public void Bloquear()
    {
        Bloqueado = true;
    }

    /// <summary>
    /// Desbloquea el usuario.
    /// </summary>
    public void Desbloquear()
    {
        Bloqueado = false;
    }

    /// <summary>
    /// Registra el último acceso del usuario.
    /// </summary>
    public void RegistrarAcceso()
    {
        UltimoAcceso = DateTime.UtcNow;
    }

    // =====================================================
    // Métodos privados
    // =====================================================

    /// <summary>
    /// Asigna el nombre de usuario.
    /// </summary>
    private void AsignarNombreUsuario(string nombreUsuario)
    {
        nombreUsuario = TextNormalizer.NormalizeRequired(
            nombreUsuario,
            UsuarioMessages.NombreUsuarioObligatorio);

        DomainValidator.MaxLength(
            nombreUsuario,
            MaxNombreUsuarioLength,
            UsuarioMessages.NombreUsuarioLongitudMaxima);

        NombreUsuario = nombreUsuario;
    }

    /// <summary>
    /// Asigna el hash de la contraseña.
    /// </summary>
    private void AsignarPasswordHash(string passwordHash)
    {
        passwordHash = TextNormalizer.NormalizeRequired(
            passwordHash,
            UsuarioMessages.PasswordHashObligatorio);

        DomainValidator.MaxLength(
            passwordHash,
            MaxPasswordHashLength,
            UsuarioMessages.PasswordHashLongitudMaxima);

        PasswordHash = passwordHash;
    }
}