namespace JLSC.Framework.Domain.Core.Seguridad.Constants;

/// <summary>
/// Mensajes de validación del permiso.
/// </summary>
public static class PermisoMessages
{
    public const string ModuloObligatorio =
        "El módulo es obligatorio.";

    public const string NombreObligatorio =
        "El nombre del permiso es obligatorio.";

    public const string NombreLongitudMaxima =
        "El nombre del permiso no puede superar la longitud máxima permitida.";

 

    public const string DescripcionLongitudMaxima =
        "La descripción del permiso no puede superar la longitud máxima permitida.";

    public const string OrdenInvalido =
        "El orden debe ser mayor o igual a cero.";
}