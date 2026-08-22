namespace JLSC.Framework.Domain.Core.Seguridad.Constants;

/// <summary>
/// Mensajes de validación del módulo.
/// </summary>
public static class ModuloMessages
{
    // ==========================
    // Validaciones
    // ==========================

    public const string CodigoObligatorio =
        "El código del módulo es obligatorio.";

    public const string NombreObligatorio =
        "El nombre del módulo es obligatorio.";

    public const string CodigoLongitudMaxima =
        "El código del módulo no puede superar la longitud máxima permitida.";

    public const string NombreLongitudMaxima =
        "El nombre del módulo no puede superar la longitud máxima permitida.";

    public const string DescripcionLongitudMaxima =
        "La descripción del módulo no puede superar la longitud máxima permitida.";

    public const string IconoLongitudMaxima =
        "El icono del módulo no puede superar la longitud máxima permitida.";

    public const string ColorLongitudMaxima =
        "El color del módulo no puede superar la longitud máxima permitida.";

    public const string OrdenInvalido =
        "El orden del módulo debe ser mayor o igual a cero.";

    // ==========================
    // Dominio
    // ==========================

    public const string MenuDuplicado =
        "El menú ya pertenece al módulo.";

    public const string MenuNoExiste =
        "El menú no pertenece al módulo.";

    public const string PermisoDuplicado =
        "El permiso ya pertenece al módulo.";

    public const string PermisoNoExiste =
        "El permiso no pertenece al módulo.";
}