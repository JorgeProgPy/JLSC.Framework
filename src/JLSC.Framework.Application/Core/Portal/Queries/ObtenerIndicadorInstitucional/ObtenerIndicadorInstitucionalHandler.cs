using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerIndicadorInstitucional;

public sealed class ObtenerIndicadorInstitucionalHandler
    : IUseCase<
        ObtenerIndicadorInstitucionalRequest,
        ObtenerIndicadorInstitucionalResponse>
{
    private readonly IIndicadorInstitucionalRepository _repository;

    public ObtenerIndicadorInstitucionalHandler(
        IIndicadorInstitucionalRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task<
        OperationResult<ObtenerIndicadorInstitucionalResponse>>
        ExecuteAsync(
            ObtenerIndicadorInstitucionalRequest request,
            CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.IndicadorId <= 0)
        {
            return OperationResult<
                ObtenerIndicadorInstitucionalResponse>.Failure(
                "El identificador del indicador no es válido.");
        }

        var indicador = await _repository.GetByIdAsync(
            request.IndicadorId,
            cancellationToken);

        if (indicador is null)
        {
            return OperationResult<
                ObtenerIndicadorInstitucionalResponse>.Failure(
                "El indicador indicado no existe.");
        }

        return OperationResult<
            ObtenerIndicadorInstitucionalResponse>.SuccessResult(
            new ObtenerIndicadorInstitucionalResponse
            {
                IndicadorId = indicador.Id,
                PublicId = indicador.PublicId,
                Titulo = indicador.Titulo,
                Valor = indicador.Valor,
                Descripcion = indicador.Descripcion,
                Icono = indicador.Icono,
                Orden = indicador.Orden,
                Activo = indicador.Activo
            });
    }
}