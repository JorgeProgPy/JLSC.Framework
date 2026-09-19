using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.DesactivarIndicadorInstitucional;

public sealed class DesactivarIndicadorInstitucionalHandler
    : IUseCase<
        DesactivarIndicadorInstitucionalRequest,
        DesactivarIndicadorInstitucionalResponse>
{
    private readonly IIndicadorInstitucionalRepository _repository;

    public DesactivarIndicadorInstitucionalHandler(
        IIndicadorInstitucionalRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task<
        OperationResult<DesactivarIndicadorInstitucionalResponse>>
        ExecuteAsync(
            DesactivarIndicadorInstitucionalRequest request,
            CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.IndicadorId <= 0)
        {
            return OperationResult<
                DesactivarIndicadorInstitucionalResponse>.Failure(
                "El identificador del indicador no es válido.");
        }

        var indicador = await _repository.GetByIdAsync(
            request.IndicadorId,
            cancellationToken);

        if (indicador is null)
        {
            return OperationResult<
                DesactivarIndicadorInstitucionalResponse>.Failure(
                "El indicador indicado no existe.");
        }

        indicador.Desactivar();

        _repository.Update(indicador);

        await _repository.GuardarCambiosAsync(
            cancellationToken);

        return OperationResult<
            DesactivarIndicadorInstitucionalResponse>.SuccessResult(
            new DesactivarIndicadorInstitucionalResponse
            {
                IndicadorId = indicador.Id,
                PublicId = indicador.PublicId,
                Titulo = indicador.Titulo,
                Activo = indicador.Activo
            });
    }
}