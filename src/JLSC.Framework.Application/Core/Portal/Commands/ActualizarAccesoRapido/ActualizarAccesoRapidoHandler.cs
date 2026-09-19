using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarAccesoRapido;

public sealed class ActualizarAccesoRapidoHandler
    : IUseCase<ActualizarAccesoRapidoRequest, ActualizarAccesoRapidoResponse>
{
    private readonly IAccesoRapidoRepository _repository;

    public ActualizarAccesoRapidoHandler(
        IAccesoRapidoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<ActualizarAccesoRapidoResponse>> ExecuteAsync(
        ActualizarAccesoRapidoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var acceso = await _repository.GetByIdAsync(
            request.AccesoRapidoId,
            cancellationToken);

        if (acceso is null)
        {
            return OperationResult<ActualizarAccesoRapidoResponse>.Failure(
                "El acceso rápido no existe.");
        }

        try
        {
            acceso.Actualizar(
                request.Titulo,
                request.Enlace,
                request.Descripcion,
                request.Icono,
                request.Orden);
        }
        catch (ArgumentException ex)
        {
            return OperationResult<ActualizarAccesoRapidoResponse>.Failure(
                ex.Message);
        }

        _repository.Update(acceso);

        await _repository.GuardarCambiosAsync(cancellationToken);

        return OperationResult<ActualizarAccesoRapidoResponse>.SuccessResult(
            new ActualizarAccesoRapidoResponse
            {
                AccesoRapidoId = acceso.Id,
                PublicId = acceso.PublicId,
                Titulo = acceso.Titulo,
                Enlace = acceso.Enlace
            });
    }
}