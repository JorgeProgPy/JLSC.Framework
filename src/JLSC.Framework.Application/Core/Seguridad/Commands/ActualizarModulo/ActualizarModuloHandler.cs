using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Seguridad.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Constants;

namespace JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarModulo;

public sealed class ActualizarModuloHandler
    : IUseCase<ActualizarModuloRequest, ActualizarModuloResponse>
{
    private readonly IModuloRepository _moduloRepository;

    public ActualizarModuloHandler(
        IModuloRepository moduloRepository)
    {
        ArgumentNullException.ThrowIfNull(moduloRepository);

        _moduloRepository = moduloRepository;
    }

    public async Task<OperationResult<ActualizarModuloResponse>> ExecuteAsync(
        ActualizarModuloRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Id <= 0)
        {
            return OperationResult<ActualizarModuloResponse>.Failure(
                "El identificador del módulo no es válido.");
        }

        if (string.IsNullOrWhiteSpace(request.Nombre))
        {
            return OperationResult<ActualizarModuloResponse>.Failure(
                ModuloMessages.NombreObligatorio);
        }

        var modulo = await _moduloRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (modulo is null)
        {
            return OperationResult<ActualizarModuloResponse>.Failure(
                "El módulo solicitado no existe.");
        }

        modulo.CambiarNombre(request.Nombre);

        modulo.CambiarDescripcion(request.Descripcion);

        modulo.CambiarIcono(request.Icono);

        modulo.CambiarColor(request.Color);

        modulo.CambiarOrden(request.Orden);

        if (request.Visible)
        {
            modulo.Mostrar();
        }
        else
        {
            modulo.Ocultar();
        }

        if (request.Activo)
        {
            modulo.Activar();
        }
        else
        {
            modulo.Desactivar();
        }


        _moduloRepository.Update(modulo);

        await _moduloRepository.GuardarCambiosAsync(
            cancellationToken);

        var response = new ActualizarModuloResponse
        {
            Id = modulo.Id,
            Codigo = modulo.Codigo,
            Nombre = modulo.Nombre
        };

        return OperationResult<ActualizarModuloResponse>
            .SuccessResult(response);
    }
}