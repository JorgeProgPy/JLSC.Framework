using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerModuloPorId;

public sealed class ObtenerModuloPorIdHandler
    : IUseCase<ObtenerModuloPorIdRequest, ObtenerModuloPorIdResponse>
{
    private readonly IModuloRepository _moduloRepository;

    public ObtenerModuloPorIdHandler(
        IModuloRepository moduloRepository)
    {
        ArgumentNullException.ThrowIfNull(moduloRepository);

        _moduloRepository = moduloRepository;
    }

    public async Task<OperationResult<ObtenerModuloPorIdResponse>> ExecuteAsync(
        ObtenerModuloPorIdRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var modulo = await _moduloRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (modulo is null)
        {
            return OperationResult<ObtenerModuloPorIdResponse>.Failure(
                "El módulo solicitado no existe.");
        }

        var response = new ObtenerModuloPorIdResponse
        {
            Id = modulo.Id,
            Codigo = modulo.Codigo,
            Nombre = modulo.Nombre,
            Descripcion = modulo.Descripcion,
            Icono = modulo.Icono,
            Color = modulo.Color,
            Orden = modulo.Orden,
            Visible = modulo.Visible,
            Activo = modulo.Activo
        };

        return OperationResult<ObtenerModuloPorIdResponse>
            .SuccessResult(response);
    }
}