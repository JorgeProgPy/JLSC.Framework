using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarConfiguracionInstitucional;

public sealed class ActualizarConfiguracionInstitucionalHandler
    : IUseCase<
        ActualizarConfiguracionInstitucionalRequest,
        ActualizarConfiguracionInstitucionalResponse>
{
    private readonly IConfiguracionInstitucionalRepository _repository;

    public ActualizarConfiguracionInstitucionalHandler(
        IConfiguracionInstitucionalRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<
        OperationResult<ActualizarConfiguracionInstitucionalResponse>> ExecuteAsync(
        ActualizarConfiguracionInstitucionalRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.ConfiguracionInstitucionalId <= 0)
        {
            return OperationResult<ActualizarConfiguracionInstitucionalResponse>.Failure(
                "El identificador de la configuración institucional no es válido.");
        }

        var configuracion = await _repository.GetByIdAsync(
            request.ConfiguracionInstitucionalId,
            cancellationToken);

        if (configuracion is null)
        {
            return OperationResult<ActualizarConfiguracionInstitucionalResponse>.Failure(
                "No se encontró la configuración institucional.");
        }

        try
        {
            configuracion.Actualizar(
                request.NombreInstitucion,
                request.NombreCorto,
                request.Descripcion,
                request.Eslogan,
                request.LogoPrincipalArchivoId,
                request.LogoSecundarioArchivoId,
                request.FaviconArchivoId,
                request.Telefono,
                request.Correo,
                request.Direccion);
        }
        catch (ArgumentException ex)
        {
            return OperationResult<ActualizarConfiguracionInstitucionalResponse>.Failure(
                ex.Message);
        }

        _repository.Update(configuracion);

        await _repository.GuardarCambiosAsync(cancellationToken);

        return OperationResult<ActualizarConfiguracionInstitucionalResponse>.SuccessResult(
            new ActualizarConfiguracionInstitucionalResponse
            {
                ConfiguracionInstitucionalId = configuracion.Id,
                PublicId = configuracion.PublicId,
                NombreInstitucion = configuracion.NombreInstitucion
            });
    }
}