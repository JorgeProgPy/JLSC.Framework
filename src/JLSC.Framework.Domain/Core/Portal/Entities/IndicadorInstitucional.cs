using JLSC.Framework.Domain.Common.Helpers;

namespace JLSC.Framework.Domain.Core.Portal.Entities;

public sealed class IndicadorInstitucional
{
    public const int MaxTituloLength = 150;

    public const int MaxValorLength = 100;

    public const int MaxDescripcionLength = 500;

    public const int MaxIconoLength = 100;

    public long Id { get; private set; }

    public Guid PublicId { get; private set; }

    public string Titulo { get; private set; } = null!;

    public string Valor { get; private set; } = null!;

    public string? Descripcion { get; private set; }

    public string? Icono { get; private set; }

    public int Orden { get; private set; }

    public bool Activo { get; private set; }

    public DateTime FechaCreacion { get; private set; }

    public DateTime? FechaModificacion { get; private set; }

    private IndicadorInstitucional()
    {
    }

    public IndicadorInstitucional(
        string titulo,
        string valor,
        string? descripcion = null,
        string? icono = null,
        int orden = 0)
    {
        Titulo = NormalizarRequerido(
            titulo,
            "El título del indicador es obligatorio.");

        Valor = NormalizarRequerido(
            valor,
            "El valor del indicador es obligatorio.");

        Descripcion = NormalizarOpcional(descripcion);

        Icono = NormalizarOpcional(icono);

        if (Titulo.Length > MaxTituloLength)
        {
            throw new ArgumentException(
                $"El título no puede superar los {MaxTituloLength} caracteres.");
        }

        if (Valor.Length > MaxValorLength)
        {
            throw new ArgumentException(
                $"El valor no puede superar los {MaxValorLength} caracteres.");
        }

        if (Descripcion?.Length > MaxDescripcionLength)
        {
            throw new ArgumentException(
                $"La descripción no puede superar los {MaxDescripcionLength} caracteres.");
        }

        if (Icono?.Length > MaxIconoLength)
        {
            throw new ArgumentException(
                $"El icono no puede superar los {MaxIconoLength} caracteres.");
        }

        if (orden < 0)
        {
            throw new ArgumentException(
                "El orden del indicador no puede ser negativo.");
        }

        Id = 0;
        PublicId = Guid.NewGuid();
        Orden = orden;
        Activo = true;
        FechaCreacion = DateTime.UtcNow;
    }

    public void Actualizar(
        string titulo,
        string valor,
        string? descripcion,
        string? icono,
        int orden)
    {
        Titulo = NormalizarRequerido(
            titulo,
            "El título del indicador es obligatorio.");

        Valor = NormalizarRequerido(
            valor,
            "El valor del indicador es obligatorio.");

        Descripcion = NormalizarOpcional(descripcion);

        Icono = NormalizarOpcional(icono);

        if (Titulo.Length > MaxTituloLength)
        {
            throw new ArgumentException(
                $"El título no puede superar los {MaxTituloLength} caracteres.");
        }

        if (Valor.Length > MaxValorLength)
        {
            throw new ArgumentException(
                $"El valor no puede superar los {MaxValorLength} caracteres.");
        }

        if (Descripcion?.Length > MaxDescripcionLength)
        {
            throw new ArgumentException(
                $"La descripción no puede superar los {MaxDescripcionLength} caracteres.");
        }

        if (Icono?.Length > MaxIconoLength)
        {
            throw new ArgumentException(
                $"El icono no puede superar los {MaxIconoLength} caracteres.");
        }

        if (orden < 0)
        {
            throw new ArgumentException(
                "El orden del indicador no puede ser negativo.");
        }

        Orden = orden;
        FechaModificacion = DateTime.UtcNow;
    }

    public void Activar()
    {
        Activo = true;
        FechaModificacion = DateTime.UtcNow;
    }

    public void Desactivar()
    {
        Activo = false;
        FechaModificacion = DateTime.UtcNow;
    }

    private static string NormalizarRequerido(
        string valor,
        string mensaje)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new ArgumentException(mensaje);
        }

        return valor.Trim();
    }

    private static string? NormalizarOpcional(string? valor)
    {
        return string.IsNullOrWhiteSpace(valor)
            ? null
            : valor.Trim();
    }
}