using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerConfiguracionInstitucional;

public sealed class ObtenerConfiguracionInstitucionalHandler
    : IUseCase<
        ObtenerConfiguracionInstitucionalRequest,
        ObtenerConfiguracionInstitucionalResponse>
{
    private readonly IConfiguracionInstitucionalRepository _repository;

    public ObtenerConfiguracionInstitucionalHandler(
        IConfiguracionInstitucionalRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<
        OperationResult<ObtenerConfiguracionInstitucionalResponse>> ExecuteAsync(
        ObtenerConfiguracionInstitucionalRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var configuracion = request.ConfiguracionInstitucionalId.HasValue
            ? await _repository.GetByIdAsync(
                request.ConfiguracionInstitucionalId.Value,
                cancellationToken)
            : await _repository.GetActivaAsync(cancellationToken);

        if (configuracion is null)
        {
            return OperationResult<ObtenerConfiguracionInstitucionalResponse>.Failure(
                "No se encontró la configuración institucional.");
        }

        return OperationResult<ObtenerConfiguracionInstitucionalResponse>.SuccessResult(
            new ObtenerConfiguracionInstitucionalResponse
            {
                ConfiguracionInstitucionalId = configuracion.Id,
                PublicId = configuracion.PublicId,
                NombreInstitucion = configuracion.NombreInstitucion,
                NombreCorto = configuracion.NombreCorto,
                Descripcion = configuracion.Descripcion,
                Eslogan = configuracion.Eslogan,
                LogoPrincipalArchivoId = configuracion.LogoPrincipalArchivoId,
                LogoSecundarioArchivoId = configuracion.LogoSecundarioArchivoId,
                FaviconArchivoId = configuracion.FaviconArchivoId,
                Telefono = configuracion.Telefono,
                Correo = configuracion.Correo,
                Direccion = configuracion.Direccion,
                Activo = configuracion.Activo
            });
    }
}