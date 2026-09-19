using JLSC.Framework.Domain.Core.Archivos.Entities;

namespace JLSC.Framework.Domain.Core.Portal.Entities;

public sealed class ConfiguracionInstitucional
{
    public const int MaxNombreInstitucionLength = 200;
    public const int MaxNombreCortoLength = 100;
    public const int MaxDescripcionLength = 1000;
    public const int MaxEsloganLength = 300;
    public const int MaxTelefonoLength = 100;
    public const int MaxCorreoLength = 200;
    public const int MaxDireccionLength = 300;

    public long Id { get; private set; }
    public Guid PublicId { get; private set; }

    public string NombreInstitucion { get; private set; } = null!;
    public string? NombreCorto { get; private set; }
    public string? Descripcion { get; private set; }
    public string? Eslogan { get; private set; }

    public long? LogoPrincipalArchivoId { get; private set; }
    public long? LogoSecundarioArchivoId { get; private set; }
    public long? FaviconArchivoId { get; private set; }

    public string? Telefono { get; private set; }
    public string? Correo { get; private set; }
    public string? Direccion { get; private set; }

    public bool Activo { get; private set; }

    public DateTime FechaCreacion { get; private set; }
    public DateTime? FechaModificacion { get; private set; }

    public Archivo? LogoPrincipalArchivo { get; private set; }
    public Archivo? LogoSecundarioArchivo { get; private set; }
    public Archivo? FaviconArchivo { get; private set; }

    public ICollection<RedSocialInstitucional> RedesSociales { get; private set; }
        = new List<RedSocialInstitucional>();

    private ConfiguracionInstitucional()
    {
    }

    public ConfiguracionInstitucional(
        string nombreInstitucion,
        string? nombreCorto = null,
        string? descripcion = null,
        string? eslogan = null,
        long? logoPrincipalArchivoId = null,
        long? logoSecundarioArchivoId = null,
        long? faviconArchivoId = null,
        string? telefono = null,
        string? correo = null,
        string? direccion = null)
    {
        NombreInstitucion = NormalizarRequerido(
            nombreInstitucion,
            "El nombre de la institución es obligatorio.");

        NombreCorto = NormalizarOpcional(nombreCorto);
        Descripcion = NormalizarOpcional(descripcion);
        Eslogan = NormalizarOpcional(eslogan);

        Telefono = NormalizarOpcional(telefono);
        Correo = NormalizarOpcional(correo);
        Direccion = NormalizarOpcional(direccion);

        ValidarLongitudes();

        Id = 0;
        PublicId = Guid.NewGuid();

        LogoPrincipalArchivoId = logoPrincipalArchivoId;
        LogoSecundarioArchivoId = logoSecundarioArchivoId;
        FaviconArchivoId = faviconArchivoId;

        Activo = true;
        FechaCreacion = DateTime.UtcNow;
    }

    public void Actualizar(
        string nombreInstitucion,
        string? nombreCorto,
        string? descripcion,
        string? eslogan,
        long? logoPrincipalArchivoId,
        long? logoSecundarioArchivoId,
        long? faviconArchivoId,
        string? telefono,
        string? correo,
        string? direccion)
    {
        NombreInstitucion = NormalizarRequerido(
            nombreInstitucion,
            "El nombre de la institución es obligatorio.");

        NombreCorto = NormalizarOpcional(nombreCorto);
        Descripcion = NormalizarOpcional(descripcion);
        Eslogan = NormalizarOpcional(eslogan);

        Telefono = NormalizarOpcional(telefono);
        Correo = NormalizarOpcional(correo);
        Direccion = NormalizarOpcional(direccion);

        ValidarLongitudes();

        LogoPrincipalArchivoId = logoPrincipalArchivoId;
        LogoSecundarioArchivoId = logoSecundarioArchivoId;
        FaviconArchivoId = faviconArchivoId;

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
        if (NombreInstitucion.Length > MaxNombreInstitucionLength)
            throw new ArgumentException(
                $"El nombre de la institución no puede superar los {MaxNombreInstitucionLength} caracteres.");

        if (NombreCorto?.Length > MaxNombreCortoLength)
            throw new ArgumentException(
                $"El nombre corto no puede superar los {MaxNombreCortoLength} caracteres.");

        if (Descripcion?.Length > MaxDescripcionLength)
            throw new ArgumentException(
                $"La descripción no puede superar los {MaxDescripcionLength} caracteres.");

        if (Eslogan?.Length > MaxEsloganLength)
            throw new ArgumentException(
                $"El eslogan no puede superar los {MaxEsloganLength} caracteres.");

        if (Telefono?.Length > MaxTelefonoLength)
            throw new ArgumentException(
                $"El teléfono no puede superar los {MaxTelefonoLength} caracteres.");

        if (Correo?.Length > MaxCorreoLength)
            throw new ArgumentException(
                $"El correo no puede superar los {MaxCorreoLength} caracteres.");

        if (Direccion?.Length > MaxDireccionLength)
            throw new ArgumentException(
                $"La dirección no puede superar los {MaxDireccionLength} caracteres.");
    }

    private static string NormalizarRequerido(
        string valor,
        string mensaje)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException(mensaje);

        return valor.Trim();
    }

    private static string? NormalizarOpcional(string? valor)
    {
        return string.IsNullOrWhiteSpace(valor)
            ? null
            : valor.Trim();
    }
}