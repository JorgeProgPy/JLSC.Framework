using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Commands.ActualizarProyectoDestacado;

public sealed class ActualizarProyectoDestacadoHandler
    : IUseCase<
        ActualizarProyectoDestacadoRequest,
        ActualizarProyectoDestacadoResponse>
{
    private readonly IProyectoDestacadoRepository _repository;

    public ActualizarProyectoDestacadoHandler(
        IProyectoDestacadoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<ActualizarProyectoDestacadoResponse>> ExecuteAsync(
        ActualizarProyectoDestacadoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var proyecto = await _repository.GetByIdAsync(
            request.ProyectoDestacadoId,
            cancellationToken);

        if (proyecto is null)
        {
            return OperationResult<ActualizarProyectoDestacadoResponse>.Failure(
                "El proyecto destacado no existe.");
        }

        try
        {
            proyecto.Actualizar(
                request.Titulo,
                request.Descripcion,
                request.Ubicacion,
                request.ArchivoId,
                request.Enlace,
                request.Orden);
        }
        catch (ArgumentException ex)
        {
            return OperationResult<ActualizarProyectoDestacadoResponse>.Failure(
                ex.Message);
        }

        _repository.Update(proyecto);

        await _repository.GuardarCambiosAsync(cancellationToken);

        return OperationResult<ActualizarProyectoDestacadoResponse>.SuccessResult(
            new ActualizarProyectoDestacadoResponse
            {
                ProyectoDestacadoId = proyecto.Id,
                PublicId = proyecto.PublicId,
                Titulo = proyecto.Titulo
            });
    }
}