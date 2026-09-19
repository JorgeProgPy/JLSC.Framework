using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;

namespace JLSC.Framework.Application.Core.Portal.Commands.CrearIndicadorInstitucional;

public sealed class CrearIndicadorInstitucionalHandler
    : IUseCase<
        CrearIndicadorInstitucionalRequest,
        CrearIndicadorInstitucionalResponse>
{
    private readonly IIndicadorInstitucionalRepository _repository;

    public CrearIndicadorInstitucionalHandler(
        IIndicadorInstitucionalRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        _repository = repository;
    }

    public async Task<
        OperationResult<CrearIndicadorInstitucionalResponse>>
        ExecuteAsync(
            CrearIndicadorInstitucionalRequest request,
            CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Orden < 0)
        {
            return OperationResult<
                CrearIndicadorInstitucionalResponse>.Failure(
                "El orden del indicador no puede ser negativo.");
        }

        IndicadorInstitucional indicador;

        try
        {
            indicador = new IndicadorInstitucional(
                request.Titulo,
                request.Valor,
                request.Descripcion,
                request.Icono,
                request.Orden);
        }
        catch (ArgumentException ex)
        {
            return OperationResult<
                CrearIndicadorInstitucionalResponse>.Failure(
                ex.Message);
        }

        _repository.Add(indicador);

        await _repository.GuardarCambiosAsync(
            cancellationToken);

        var response = new CrearIndicadorInstitucionalResponse
        {
            IndicadorId = indicador.Id,
            PublicId = indicador.PublicId,
            Titulo = indicador.Titulo,
            Valor = indicador.Valor,
            Descripcion = indicador.Descripcion,
            Icono = indicador.Icono,
            Orden = indicador.Orden,
            Activo = indicador.Activo
        };

        return OperationResult<
            CrearIndicadorInstitucionalResponse>.SuccessResult(
            response);
    }
}