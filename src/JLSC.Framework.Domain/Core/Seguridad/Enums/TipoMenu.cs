namespace JLSC.Framework.Domain.Core.Seguridad.Enums;

/// <summary>
/// Define los tipos de elementos que pueden formar parte del menú
/// de navegación del sistema.
/// </summary>
public enum TipoMenu
{
    // -----------------------------------------------------------------
    // Elemento navegable
    // -----------------------------------------------------------------

    /// <summary>
    /// Representa una opción de menú que permite navegar a una página.
    /// </summary>
    Menu = 1,

    // -----------------------------------------------------------------
    // Título
    // -----------------------------------------------------------------

    /// <summary>
    /// Representa un encabezado utilizado para agrupar opciones.
    /// </summary>
    Titulo = 2,

    // -----------------------------------------------------------------
    // Separador
    // -----------------------------------------------------------------

    /// <summary>
    /// Representa un separador visual entre grupos de opciones.
    /// </summary>
    Separador = 3
}