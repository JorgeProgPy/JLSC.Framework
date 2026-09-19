using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.ActivarIndicadorInstitucional;

public sealed class ActivarIndicadorInstitucionalHandler
    : IUseCase<
        ActivarIndicadorInstitucionalRequest,
        ActivarIndicadorInstitucionalResponse>
{
    private readonly IIndicadorInstitucionalRepository _repository;

    public ActivarIndicadorInstitucionalHandler(
        IIndicadorInstitucionalRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task<
        OperationResult<ActivarIndicadorInstitucionalResponse>>
        ExecuteAsync(
            ActivarIndicadorInstitucionalRequest request,
            CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.IndicadorId <= 0)
        {
            return OperationResult<
                ActivarIndicadorInstitucionalResponse>.Failure(
                "El identificador del indicador no es válido.");
        }

        var indicador = await _repository.GetByIdAsync(
            request.IndicadorId,
            cancellationToken);

        if (indicador is null)
        {
            return OperationResult<
                ActivarIndicadorInstitucionalResponse>.Failure(
                "El indicador indicado no existe.");
        }

        indicador.Activar();

        _repository.Update(indicador);

        await _repository.GuardarCambiosAsync(
            cancellationToken);

        return OperationResult<
            ActivarIndicadorInstitucionalResponse>.SuccessResult(
            new ActivarIndicadorInstitucionalResponse
            {
                IndicadorId = indicador.Id,
                PublicId = indicador.PublicId,
                Titulo = indicador.Titulo,
                Activo = indicador.Activo
            });
    }
}