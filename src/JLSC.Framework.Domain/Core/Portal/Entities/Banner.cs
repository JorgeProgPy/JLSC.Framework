using JLSC.Framework.Domain.Core.Archivos.Entities;

namespace JLSC.Framework.Domain.Core.Portal.Entities;

public sealed class Banner
{
    public long Id { get; private set; }

    public Guid PublicId { get; private set; }

    public string Titulo { get; private set; } = null!;

    public string? Subtitulo { get; private set; }

    public long? ArchivoId { get; private set; }

    public string? Enlace { get; private set; }

    public int Orden { get; private set; }

    public DateTime? FechaInicio { get; private set; }

    public DateTime? FechaFin { get; private set; }

    public bool Activo { get; private set; }

    public DateTime FechaCreacion { get; private set; }

    public DateTime? FechaModificacion { get; private set; }

    public Archivo? Archivo { get; private set; }

    private Banner()
    {
    }

    public Banner(
        string titulo,
        string? subtitulo = null,
        long? archivoId = null,
        string? enlace = null,
        int orden = 0,
        DateTime? fechaInicio = null,
        DateTime? fechaFin = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(titulo);

        Id = 0;
        PublicId = Guid.NewGuid();
        Titulo = titulo.Trim();
        Subtitulo = subtitulo?.Trim();
        ArchivoId = archivoId;
        Enlace = enlace?.Trim();
        Orden = orden;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        Activo = true;
        FechaCreacion = DateTime.UtcNow;
    }

    public void Actualizar(
        string titulo,
        string? subtitulo,
        long? archivoId,
        string? enlace,
        int orden,
        DateTime? fechaInicio,
        DateTime? fechaFin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(titulo);

        Titulo = titulo.Trim();
        Subtitulo = subtitulo?.Trim();
        ArchivoId = archivoId;
        Enlace = enlace?.Trim();
        Orden = orden;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
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
}