namespace JLSC.Framework.Application.Core.Portal.Commands.CrearConfiguracionInstitucional;

public sealed class CrearConfiguracionInstitucionalResponse
{
    public long ConfiguracionInstitucionalId { get; init; }
    public Guid PublicId { get; init; }
    public string NombreInstitucion { get; init; } = string.Empty;
}