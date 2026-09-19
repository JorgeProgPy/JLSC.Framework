using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;

namespace JLSC.Framework.Application.Core.Portal.Queries.ObtenerIndicadoresInstitucionales;

public sealed class ObtenerIndicadoresInstitucionalesHandler
    : IUseCase<
        ObtenerIndicadoresInstitucionalesRequest,
        List<ObtenerIndicadoresInstitucionalesResponse>>
{
    private readonly IIndicadorInstitucionalRepository _repository;

    public ObtenerIndicadoresInstitucionalesHandler(
        IIndicadorInstitucionalRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task<
        OperationResult<List<ObtenerIndicadoresInstitucionalesResponse>>>
        ExecuteAsync(
            ObtenerIndicadoresInstitucionalesRequest request,
            CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var indicadores = await _repository.GetAllAsync(
            request.IncluirInactivos,
            cancellationToken);

        var response = indicadores
            .Select(indicador => new ObtenerIndicadoresInstitucionalesResponse
            {
                IndicadorId = indicador.Id,
                PublicId = indicador.PublicId,
                Titulo = indicador.Titulo,
                Valor = indicador.Valor,
                Descripcion = indicador.Descripcion,
                Icono = indicador.Icono,
                Orden = indicador.Orden,
                Activo = indicador.Activo
            })
            .ToList();

        return OperationResult<
            List<ObtenerIndicadoresInstitucionalesResponse>>
            .SuccessResult(response);
    }
}