namespace JLSC.Framework.Domain.Core.Seguridad.Constants;

/// <summary>
/// Mensajes de validación del usuario.
/// </summary>
public static class UsuarioMessages
{
    public const string PersonaObligatoria =
        "La persona es obligatoria.";

    public const string NombreUsuarioObligatorio =
        "El nombre de usuario es obligatorio.";

    public const string NombreUsuarioLongitudMaxima =
        "El nombre de usuario no puede superar la longitud máxima permitida.";

    public const string PasswordHashObligatorio =
        "La contraseña es obligatoria.";

    public const string PasswordHashLongitudMaxima =
        "La contraseña no puede superar la longitud máxima permitida.";
}