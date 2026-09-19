using JLSC.Framework.Domain.Core.Archivos.Entities;

namespace JLSC.Framework.Domain.Core.Portal.Entities;

public sealed class ProyectoDestacado
{
    public const int MaxTituloLength = 200;
    public const int MaxDescripcionLength = 1000;
    public const int MaxUbicacionLength = 200;
    public const int MaxEnlaceLength = 500;

    public long Id { get; private set; }
    public Guid PublicId { get; private set; }

    public string Titulo { get; private set; } = null!;
    public string? Descripcion { get; private set; }
    public string? Ubicacion { get; private set; }

    public long? ArchivoId { get; private set; }
    public string? Enlace { get; private set; }

    public int Orden { get; private set; }
    public bool Activo { get; private set; }

    public DateTime FechaCreacion { get; private set; }
    public DateTime? FechaModificacion { get; private set; }

    public Archivo? Archivo { get; private set; }

    private ProyectoDestacado()
    {
    }

    public ProyectoDestacado(
        string titulo,
        string? descripcion = null,
        string? ubicacion = null,
        long? archivoId = null,
        string? enlace = null,
        int orden = 0)
    {
        Titulo = NormalizarRequerido(
            titulo,
            "El título del proyecto destacado es obligatorio.");

        Descripcion = NormalizarOpcional(descripcion);
        Ubicacion = NormalizarOpcional(ubicacion);
        Enlace = NormalizarOpcional(enlace);

        ValidarLongitudes();

        if (orden < 0)
        {
            throw new ArgumentException(
                "El orden del proyecto destacado no puede ser negativo.");
        }

        Id = 0;
        PublicId = Guid.NewGuid();

        ArchivoId = archivoId;
        Orden = orden;
        Activo = true;

        FechaCreacion = DateTime.UtcNow;
    }

    public void Actualizar(
        string titulo,
        string? descripcion,
        string? ubicacion,
        long? archivoId,
        string? enlace,
        int orden)
    {
        Titulo = NormalizarRequerido(
            titulo,
            "El título del proyecto destacado es obligatorio.");

        Descripcion = NormalizarOpcional(descripcion);
        Ubicacion = NormalizarOpcional(ubicacion);
        Enlace = NormalizarOpcional(enlace);

        ValidarLongitudes();

        if (orden < 0)
        {
            throw new ArgumentException(
                "El orden del proyecto destacado no puede ser negativo.");
        }

        ArchivoId = archivoId;
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

        if (Ubicacion?.Length > MaxUbicacionLength)
        {
            throw new ArgumentException(
                $"La ubicación no puede superar los {MaxUbicacionLength} caracteres.");
        }

        if (Enlace?.Length > MaxEnlaceLength)
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