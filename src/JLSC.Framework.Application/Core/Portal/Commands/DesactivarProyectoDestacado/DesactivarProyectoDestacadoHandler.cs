using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.DesactivarProyectoDestacado;

public sealed class DesactivarProyectoDestacadoHandler
    : IUseCase<
        DesactivarProyectoDestacadoRequest,
        DesactivarProyectoDestacadoResponse>
{
    private readonly IProyectoDestacadoRepository _repository;

    public DesactivarProyectoDestacadoHandler(
        IProyectoDestacadoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<DesactivarProyectoDestacadoResponse>> ExecuteAsync(
        DesactivarProyectoDestacadoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var proyecto = await _repository.GetByIdAsync(
            request.ProyectoDestacadoId,
            cancellationToken);

        if (proyecto is null)
        {
            return OperationResult<DesactivarProyectoDestacadoResponse>.Failure(
                "El proyecto destacado no existe.");
        }

        proyecto.Desactivar();

        _repository.Update(proyecto);

        await _repository.GuardarCambiosAsync(cancellationToken);

        return OperationResult<DesactivarProyectoDestacadoResponse>.SuccessResult(
            new DesactivarProyectoDestacadoResponse
            {
                ProyectoDestacadoId = proyecto.Id,
                Activo = false
            });
    }
}