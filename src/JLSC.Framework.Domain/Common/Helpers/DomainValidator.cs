using JLSC.Framework.Domain.Common.Exceptions;

namespace JLSC.Framework.Domain.Common.Helpers;

/// <summary>
/// Contiene métodos reutilizables para validar reglas comunes del dominio.
/// </summary>
public static class DomainValidator
{
    // -----------------------------------------------------------------
    // Validar identificador obligatorio
    // -----------------------------------------------------------------

    public static void RequiredId(
        long id,
        string errorMessage)
    {
        if (id <= 0)
            throw new DomainValidationException(errorMessage);
    }

    // -----------------------------------------------------------------
    // Validar identificador opcional
    // -----------------------------------------------------------------

    public static void OptionalId(
        long? id,
        string errorMessage)
    {
        if (id.HasValue && id.Value <= 0)
            throw new DomainValidationException(errorMessage);
    }

    // -----------------------------------------------------------------
    // Validar longitud máxima
    // -----------------------------------------------------------------

    public static void MaxLength(
        string? value,
        int maxLength,
        string errorMessage)
    {
        if (!string.IsNullOrWhiteSpace(value) &&
            value.Length > maxLength)
        {
            throw new DomainValidationException(errorMessage);
        }
    }

    // -----------------------------------------------------------------
    // Validar fecha
    // -----------------------------------------------------------------

    public static void BirthDate(
        DateOnly? value)
    {
        if (!value.HasValue)
            return;

        var today = DateOnly.FromDateTime(DateTime.Today);

        if (value.Value > today)
            throw new DomainValidationException(
                "La fecha de nacimiento no puede ser mayor a la fecha actual.");

        if (value.Value < new DateOnly(1900, 1, 1))
            throw new DomainValidationException(
                "La fecha de nacimiento no es válida.");
    }

    public static void Required(
    string? value,
    string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException(errorMessage);
    }

    public static void MinValue(
        int value,
        int minimum,
        string errorMessage)
    {
        if (value < minimum)
            throw new DomainValidationException(errorMessage);
    }

    public static void MaxValue(
    int value,
    int maximum,
    string errorMessage)
    {
        if (value > maximum)
            throw new DomainValidationException(errorMessage);
    }

    public static void Range(
    int value,
    int minimum,
    int maximum,
    string errorMessage)
    {
        if (value < minimum || value > maximum)
            throw new DomainValidationException(errorMessage);
    }

}