using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;

namespace JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerModulos;

public sealed class ObtenerModulosHandler
    : IUseCase<ObtenerModulosRequest, IReadOnlyCollection<ObtenerModulosResponse>>
{
    private readonly IModuloRepository _moduloRepository;

    public ObtenerModulosHandler(
        IModuloRepository moduloRepository)
    {
        ArgumentNullException.ThrowIfNull(moduloRepository);

        _moduloRepository = moduloRepository;
    }

    public async Task<OperationResult<IReadOnlyCollection<ObtenerModulosResponse>>> ExecuteAsync(
        ObtenerModulosRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var modulos = await _moduloRepository.GetAllAsync(
            request.IncluirInactivos,
            cancellationToken);

        var response = modulos
            .Select(x => new ObtenerModulosResponse
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Icono = x.Icono,
                Color = x.Color,
                Orden = x.Orden,
                Visible = x.Visible,
                Activo = x.Activo
            })
            .ToList();

        return OperationResult<IReadOnlyCollection<ObtenerModulosResponse>>
            .SuccessResult(response);
    }
}