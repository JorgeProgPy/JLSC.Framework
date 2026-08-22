namespace JLSC.Framework.Domain.Core.Seguridad.Constants;

/// <summary>
/// Mensajes de validación del rol.
/// </summary>
public static class RolMessages
{
    public const string NombreObligatorio =
        "El nombre del rol es obligatorio.";

    public const string NombreLongitudMaxima =
        "El nombre del rol no puede superar la longitud máxima permitida.";

    public const string DescripcionLongitudMaxima =
        "La descripción del rol no puede superar la longitud máxima permitida.";
}