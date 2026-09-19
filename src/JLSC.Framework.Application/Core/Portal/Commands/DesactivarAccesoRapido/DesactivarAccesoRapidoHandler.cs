using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.DesactivarAccesoRapido;

public sealed class DesactivarAccesoRapidoHandler
    : IUseCase<DesactivarAccesoRapidoRequest, DesactivarAccesoRapidoResponse>
{
    private readonly IAccesoRapidoRepository _repository;

    public DesactivarAccesoRapidoHandler(
        IAccesoRapidoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<DesactivarAccesoRapidoResponse>> ExecuteAsync(
        DesactivarAccesoRapidoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var acceso = await _repository.GetByIdAsync(
            request.AccesoRapidoId,
            cancellationToken);

        if (acceso is null)
        {
            return OperationResult<DesactivarAccesoRapidoResponse>.Failure(
                "El acceso rápido no existe.");
        }

        acceso.Desactivar();

        _repository.Update(acceso);

        await _repository.GuardarCambiosAsync(cancellationToken);

        return OperationResult<DesactivarAccesoRapidoResponse>.SuccessResult(
            new DesactivarAccesoRapidoResponse
            {
                AccesoRapidoId = acceso.Id,
                Activo = acceso.Activo
            });
    }
}