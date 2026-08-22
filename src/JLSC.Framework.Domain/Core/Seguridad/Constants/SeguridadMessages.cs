namespace JLSC.Framework.Domain.Core.Seguridad.Constants;

/// <summary>
/// Contiene los mensajes utilizados por el módulo de Seguridad.
/// </summary>
public static class SeguridadMessages
{
    // -----------------------------------------------------------------
    // Menú
    // -----------------------------------------------------------------

    /// <summary>
    /// El nombre del menú es obligatorio.
    /// </summary>
    public const string MenuNombreObligatorio =
        "El nombre del menú es obligatorio.";

    /// <summary>
    /// La longitud máxima del nombre del menú es de 150 caracteres.
    /// </summary>
    public const string MenuNombreLongitudMaxima =
        "La longitud máxima del nombre del menú es de 150 caracteres.";

    /// <summary>
    /// La ruta del menú es obligatoria.
    /// </summary>
    public const string MenuRutaObligatoria =
        "La ruta del menú es obligatoria.";

    /// <summary>
    /// La longitud máxima de la ruta es de 250 caracteres.
    /// </summary>
    public const string MenuRutaLongitudMaxima =
        "La longitud máxima de la ruta es de 250 caracteres.";

    /// <summary>
    /// El orden del menú es inválido.
    /// </summary>
    public const string MenuOrdenInvalido =
        "El orden del menú debe ser mayor o igual a cero.";

    /// <summary>
    /// El menú ya pertenece al módulo.
    /// </summary>
    public const string MenuDuplicado =
        "El menú ya existe dentro del módulo.";

    // -----------------------------------------------------------------
    // Permisos
    // -----------------------------------------------------------------

    /// <summary>
    /// El nombre del permiso es obligatorio.
    /// </summary>
    public const string PermisoNombreObligatorio =
        "El nombre del permiso es obligatorio.";

    /// <summary>
    /// El código del permiso es obligatorio.
    /// </summary>
    public const string PermisoCodigoObligatorio =
        "El código del permiso es obligatorio.";

    /// <summary>
    /// Ya existe un permiso con el mismo código.
    /// </summary>
    public const string PermisoDuplicado =
        "Ya existe un permiso registrado con el mismo código.";

    // -----------------------------------------------------------------
    // Módulo
    // -----------------------------------------------------------------

    /// <summary>
    /// El nombre del módulo es obligatorio.
    /// </summary>
    public const string ModuloNombreObligatorio =
        "El nombre del módulo es obligatorio.";

    /// <summary>
    /// El código del módulo es obligatorio.
    /// </summary>
    public const string ModuloCodigoObligatorio =
        "El código del módulo es obligatorio.";
}