using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarIndicadorInstitucional;

public sealed class ActualizarIndicadorInstitucionalHandler
    : IUseCase<
        ActualizarIndicadorInstitucionalRequest,
        ActualizarIndicadorInstitucionalResponse>
{
    private readonly IIndicadorInstitucionalRepository _repository;

    public ActualizarIndicadorInstitucionalHandler(
        IIndicadorInstitucionalRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task<
        OperationResult<ActualizarIndicadorInstitucionalResponse>>
        ExecuteAsync(
            ActualizarIndicadorInstitucionalRequest request,
            CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.IndicadorId <= 0)
        {
            return OperationResult<
                ActualizarIndicadorInstitucionalResponse>.Failure(
                "El identificador del indicador no es válido.");
        }

        if (request.Orden < 0)
        {
            return OperationResult<
                ActualizarIndicadorInstitucionalResponse>.Failure(
                "El orden del indicador no puede ser negativo.");
        }

        var indicador = await _repository.GetByIdAsync(
            request.IndicadorId,
            cancellationToken);

        if (indicador is null)
        {
            return OperationResult<
                ActualizarIndicadorInstitucionalResponse>.Failure(
                "El indicador indicado no existe.");
        }

        try
        {
            indicador.Actualizar(
                request.Titulo,
                request.Valor,
                request.Descripcion,
                request.Icono,
                request.Orden);
        }
        catch (ArgumentException ex)
        {
            return OperationResult<
                ActualizarIndicadorInstitucionalResponse>.Failure(
                ex.Message);
        }

        _repository.Update(indicador);

        await _repository.GuardarCambiosAsync(
            cancellationToken);

        var response = new ActualizarIndicadorInstitucionalResponse
        {
            IndicadorId = indicador.Id,
            PublicId = indicador.PublicId,
            Titulo = indicador.Titulo,
            Valor = indicador.Valor,
            Descripcion = indicador.Descripcion,
            Icono = indicador.Icono,
            Orden = indicador.Orden,
            Activo = indicador.Activo
        };

        return OperationResult<
            ActualizarIndicadorInstitucionalResponse>.SuccessResult(
            response);
    }
}