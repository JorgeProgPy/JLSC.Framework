using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;

namespace JLSC.Framework.Application.Core.Portal.Commands.CrearAccesoRapido;

public sealed class CrearAccesoRapidoHandler
    : IUseCase<CrearAccesoRapidoRequest, CrearAccesoRapidoResponse>
{
    private readonly IAccesoRapidoRepository _repository;

    public CrearAccesoRapidoHandler(
        IAccesoRapidoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<CrearAccesoRapidoResponse>> ExecuteAsync(
        CrearAccesoRapidoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        AccesoRapido acceso;

        try
        {
            acceso = new AccesoRapido(
                request.Titulo,
                request.Enlace,
                request.Descripcion,
                request.Icono,
                request.Orden);
        }
        catch (ArgumentException ex)
        {
            return OperationResult<CrearAccesoRapidoResponse>.Failure(
                ex.Message);
        }

        _repository.Add(acceso);

        await _repository.GuardarCambiosAsync(cancellationToken);

        return OperationResult<CrearAccesoRapidoResponse>.SuccessResult(
            new CrearAccesoRapidoResponse
            {
                AccesoRapidoId = acceso.Id,
                PublicId = acceso.PublicId,
                Titulo = acceso.Titulo,
                Enlace = acceso.Enlace
            });
    }
}