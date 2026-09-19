using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;

namespace JLSC.Framework.Application.Core.Portal.Commands.CrearConfiguracionInstitucional;

public sealed class CrearConfiguracionInstitucionalHandler
    : IUseCase<
        CrearConfiguracionInstitucionalRequest,
        CrearConfiguracionInstitucionalResponse>
{
    private readonly IConfiguracionInstitucionalRepository _repository;

    public CrearConfiguracionInstitucionalHandler(
        IConfiguracionInstitucionalRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<
        OperationResult<CrearConfiguracionInstitucionalResponse>> ExecuteAsync(
        CrearConfiguracionInstitucionalRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var existente = await _repository.GetActivaAsync(cancellationToken);

        if (existente is not null)
        {
            return OperationResult<CrearConfiguracionInstitucionalResponse>.Failure(
                "Ya existe una configuración institucional activa.");
        }

        ConfiguracionInstitucional configuracion;

        try
        {
            configuracion = new ConfiguracionInstitucional(
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
            return OperationResult<CrearConfiguracionInstitucionalResponse>.Failure(
                ex.Message);
        }

        _repository.Add(configuracion);

        await _repository.GuardarCambiosAsync(cancellationToken);

        return OperationResult<CrearConfiguracionInstitucionalResponse>.SuccessResult(
            new CrearConfiguracionInstitucionalResponse
            {
                ConfiguracionInstitucionalId = configuracion.Id,
                PublicId = configuracion.PublicId,
                NombreInstitucion = configuracion.NombreInstitucion
            });
    }
}