using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Constants;
using JLSC.Framework.Domain.Core.Seguridad.Entities;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.CrearModulo;

public sealed class CrearModuloHandler
    : IUseCase<CrearModuloRequest, CrearModuloResponse>
{
    private readonly IModuloRepository _moduloRepository;

    public CrearModuloHandler(
        IModuloRepository moduloRepository)
    {
        ArgumentNullException.ThrowIfNull(moduloRepository);

        _moduloRepository = moduloRepository;
    }

    public async Task<OperationResult<CrearModuloResponse>> ExecuteAsync(
        CrearModuloRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Codigo))
        {
            return OperationResult<CrearModuloResponse>.Failure(
                ModuloMessages.CodigoObligatorio);
        }

        if (string.IsNullOrWhiteSpace(request.Nombre))
        {
            return OperationResult<CrearModuloResponse>.Failure(
                ModuloMessages.NombreObligatorio);
        }

        if (await _moduloRepository.ExistsCodigoAsync(
                request.Codigo,
                cancellationToken))
        {
            return OperationResult<CrearModuloResponse>.Failure(
                "El código del módulo ya está registrado.");
        }

        var modulo = Modulo.Crear(
            request.Codigo,
            request.Nombre,
            request.Descripcion,
            request.Icono,
            request.Color,
            request.Orden,
            request.Visible);

        await _moduloRepository.AddAsync(
            modulo,
            cancellationToken);

        await _moduloRepository.GuardarCambiosAsync(
            cancellationToken);

        var response = new CrearModuloResponse
        {
            ModuloId = modulo.Id,
            Codigo = modulo.Codigo,
            Nombre = modulo.Nombre
        };

        return OperationResult<CrearModuloResponse>
            .SuccessResult(response);
    }
}