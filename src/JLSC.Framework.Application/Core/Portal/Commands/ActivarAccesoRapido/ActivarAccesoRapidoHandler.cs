using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.ActivarAccesoRapido;

public sealed class ActivarAccesoRapidoHandler
    : IUseCase<ActivarAccesoRapidoRequest, ActivarAccesoRapidoResponse>
{
    private readonly IAccesoRapidoRepository _repository;

    public ActivarAccesoRapidoHandler(
        IAccesoRapidoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<ActivarAccesoRapidoResponse>> ExecuteAsync(
        ActivarAccesoRapidoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var acceso = await _repository.GetByIdAsync(
            request.AccesoRapidoId,
            cancellationToken);

        if (acceso is null)
        {
            return OperationResult<ActivarAccesoRapidoResponse>.Failure(
                "El acceso rápido no existe.");
        }

        acceso.Activar();

        _repository.Update(acceso);

        await _repository.GuardarCambiosAsync(cancellationToken);

        return OperationResult<ActivarAccesoRapidoResponse>.SuccessResult(
            new ActivarAccesoRapidoResponse
            {
                AccesoRapidoId = acceso.Id,
                Activo = acceso.Activo
            });
    }
}