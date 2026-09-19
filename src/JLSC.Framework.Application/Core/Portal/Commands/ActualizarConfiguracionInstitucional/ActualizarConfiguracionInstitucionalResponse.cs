namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarConfiguracionInstitucional;

public sealed class ActualizarConfiguracionInstitucionalResponse
{
    public long ConfiguracionInstitucionalId { get; init; }
    public Guid PublicId { get; init; }
    public string NombreInstitucion { get; init; } = string.Empty;
}