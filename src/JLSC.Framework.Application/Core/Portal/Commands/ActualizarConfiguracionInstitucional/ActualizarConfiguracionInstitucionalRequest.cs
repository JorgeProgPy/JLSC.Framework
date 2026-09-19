namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarConfiguracionInstitucional;

public sealed class ActualizarConfiguracionInstitucionalRequest
{
    public long ConfiguracionInstitucionalId { get; init; }

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
}