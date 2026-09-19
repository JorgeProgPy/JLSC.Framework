using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;

namespace JLSC.Framework.Application.Core.Portal.Commands.CrearProyectoDestacado;

public sealed class CrearProyectoDestacadoHandler
    : IUseCase<CrearProyectoDestacadoRequest, CrearProyectoDestacadoResponse>
{
    private readonly IProyectoDestacadoRepository _repository;

    public CrearProyectoDestacadoHandler(
        IProyectoDestacadoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<CrearProyectoDestacadoResponse>> ExecuteAsync(
        CrearProyectoDestacadoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        ProyectoDestacado proyecto;

        try
        {
            proyecto = new ProyectoDestacado(
                request.Titulo,
                request.Descripcion,
                request.Ubicacion,
                request.ArchivoId,
                request.Enlace,
                request.Orden);
        }
        catch (ArgumentException ex)
        {
            return OperationResult<CrearProyectoDestacadoResponse>.Failure(
                ex.Message);
        }

        _repository.Add(proyecto);

        await _repository.GuardarCambiosAsync(cancellationToken);

        return OperationResult<CrearProyectoDestacadoResponse>.SuccessResult(
            new CrearProyectoDestacadoResponse
            {
                ProyectoDestacadoId = proyecto.Id,
                PublicId = proyecto.PublicId,
                Titulo = proyecto.Titulo
            });
    }
}