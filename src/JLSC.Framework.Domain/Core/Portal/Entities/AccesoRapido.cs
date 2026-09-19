namespace JLSC.Framework.Domain.Core.Portal.Entities;

public sealed class AccesoRapido
{
    public const int MaxTituloLength = 150;
    public const int MaxDescripcionLength = 300;
    public const int MaxIconoLength = 100;
    public const int MaxEnlaceLength = 500;

    public long Id { get; private set; }
    public Guid PublicId { get; private set; }

    public string Titulo { get; private set; } = null!;
    public string? Descripcion { get; private set; }
    public string? Icono { get; private set; }
    public string Enlace { get; private set; } = null!;

    public int Orden { get; private set; }
    public bool Activo { get; private set; }

    public DateTime FechaCreacion { get; private set; }
    public DateTime? FechaModificacion { get; private set; }

    private AccesoRapido()
    {
    }

    public AccesoRapido(
        string titulo,
        string enlace,
        string? descripcion = null,
        string? icono = null,
        int orden = 0)
    {
        Titulo = NormalizarRequerido(
            titulo,
            "El título del acceso rápido es obligatorio.");

        Enlace = NormalizarRequerido(
            enlace,
            "El enlace del acceso rápido es obligatorio.");

        Descripcion = NormalizarOpcional(descripcion);
        Icono = NormalizarOpcional(icono);

        ValidarLongitudes();

        if (orden < 0)
        {
            throw new ArgumentException(
                "El orden del acceso rápido no puede ser negativo.");
        }

        Id = 0;
        PublicId = Guid.NewGuid();
        Orden = orden;
        Activo = true;
        FechaCreacion = DateTime.UtcNow;
    }

    public void Actualizar(
        string titulo,
        string enlace,
        string? descripcion,
        string? icono,
        int orden)
    {
        Titulo = NormalizarRequerido(
            titulo,
            "El título del acceso rápido es obligatorio.");

        Enlace = NormalizarRequerido(
            enlace,
            "El enlace del acceso rápido es obligatorio.");

        Descripcion = NormalizarOpcional(descripcion);
        Icono = NormalizarOpcional(icono);

        ValidarLongitudes();

        if (orden < 0)
        {
            throw new ArgumentException(
                "El orden del acceso rápido no puede ser negativo.");
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

    private void ValidarLongitudes()
    {
        if (Titulo.Length > MaxTituloLength)
        {
            throw new ArgumentException(
                $"El título no puede superar los {MaxTituloLength} caracteres.");
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

        if (Enlace.Length > MaxEnlaceLength)
        {
            throw new ArgumentException(
                $"El enlace no puede superar los {MaxEnlaceLength} caracteres.");
        }
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