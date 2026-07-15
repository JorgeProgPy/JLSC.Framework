using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;

namespace JLSC.Framework.Application.Core.Archivos.Commands.GuardarArchivo;

public sealed class GuardarArchivoHandler
    : IUseCase<GuardarArchivoRequest, GuardarArchivoResponse>
{
    public async Task<OperationResult<GuardarArchivoResponse>> ExecuteAsync(
        GuardarArchivoRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResult<GuardarArchivoResponse>> ExecuteAsync(
    GuardarArchivoRequest request,
    CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
}