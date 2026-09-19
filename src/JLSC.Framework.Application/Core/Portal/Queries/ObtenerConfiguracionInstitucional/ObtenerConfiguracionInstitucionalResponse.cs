namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerConfiguracionInstitucional;

public sealed class ObtenerConfiguracionInstitucionalResponse
{
    public long ConfiguracionInstitucionalId { get; init; }
    public Guid PublicId { get; init; }

    public string NombreInstitucion { get; init; } = string.Empty;
    public string? NombreCorto { get; init; }
    public string? Descripcion { get; init; }
    public string? Eslogan { get; init; }

    public long? LogoPrincipalArchivoId { get; init; }
    public long? LogoSecundarioArchivoId { get; init; }
    public long? FaviconArchivoId { get; init; }

    public string? Telefono { get; init; }
    public string? Correo { get; init; }
    public string? Direccion { get; init; }

    public bool Activo { get; init; }
}