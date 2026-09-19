using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerAccesoRapido;

public sealed class ObtenerAccesoRapidoHandler
    : IUseCase<ObtenerAccesoRapidoRequest, ObtenerAccesoRapidoResponse>
{
    private readonly IAccesoRapidoRepository _repository;

    public ObtenerAccesoRapidoHandler(
        IAccesoRapidoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<OperationResult<ObtenerAccesoRapidoResponse>> ExecuteAsync(
        ObtenerAccesoRapidoRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var acceso = await _repository.GetByIdAsync(
            request.AccesoRapidoId,
            cancellationToken);

        if (acceso is null)
        {
            return OperationResult<ObtenerAccesoRapidoResponse>.Failure(
                "El acceso rápido no existe.");
        }

        return OperationResult<ObtenerAccesoRapidoResponse>.SuccessResult(
            new ObtenerAccesoRapidoResponse
            {
                AccesoRapidoId = acceso.Id,
                PublicId = acceso.PublicId,
                Titulo = acceso.Titulo,
                Descripcion = acceso.Descripcion,
                Icono = acceso.Icono,
                Enlace = acceso.Enlace,
                Orden = acceso.Orden,
                Activo = acceso.Activo
            });
    }
}