using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.ActivarProyectoDestacado;

public sealed class ActivarProyectoDestacadoHandler
    : IUseCase<
        ActivarProyectoDestacadoRequest,
        ActivarProyectoDestacadoResponse>
{
    private readonly IProyectoDestacadoRepository _repository;

    public ActivarProyectoDestacadoHandler(
        IProyectoDestacadoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<ActivarProyectoDestacadoResponse>> ExecuteAsync(
        ActivarProyectoDestacadoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var proyecto = await _repository.GetByIdAsync(
            request.ProyectoDestacadoId,
            cancellationToken);

        if (proyecto is null)
        {
            return OperationResult<ActivarProyectoDestacadoResponse>.Failure(
                "El proyecto destacado no existe.");
        }

        proyecto.Activar();

        _repository.Update(proyecto);

        await _repository.GuardarCambiosAsync(cancellationToken);

        return OperationResult<ActivarProyectoDestacadoResponse>.SuccessResult(
            new ActivarProyectoDestacadoResponse
            {
                ProyectoDestacadoId = proyecto.Id,
                Activo = true
            });
    }
}