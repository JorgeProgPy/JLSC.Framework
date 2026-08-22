using System.Text.RegularExpressions;
using JLSC.Framework.Domain.Common.Exceptions;

namespace JLSC.Framework.Domain.Common.Helpers;

/// <summary>
/// Proporciona métodos para normalizar cadenas de texto del dominio.
/// </summary>
public static class TextNormalizer
{
    // -----------------------------------------------------------------
    // Normalizar texto obligatorio
    // -----------------------------------------------------------------

    public static string NormalizeRequired(
        string? value,
        string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException(errorMessage);

        return Normalize(value);
    }

    // -----------------------------------------------------------------
    // Normalizar texto opcional
    // -----------------------------------------------------------------

    public static string? NormalizeOptional(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return Normalize(value);
    }

    // -----------------------------------------------------------------
    // Normalizar texto
    // -----------------------------------------------------------------

    public static string Normalize(
        string value)
    {
        return Regex.Replace(
            value.Trim(),
            @"\s+",
            " ");
    }

    public static string NormalizeCode(string value)
    {
        value = NormalizeRequired(
            value,
            "El código es obligatorio.");

        value = value
            .Trim()
            .Replace(' ', '_')
            .ToUpperInvariant();

        return value;
    }

}