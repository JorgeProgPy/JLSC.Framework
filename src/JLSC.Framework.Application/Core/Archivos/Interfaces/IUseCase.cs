using JLSC.Framework.Application.Common.Results;

namespace JLSC.Framework.Application.Common.Interfaces;

public interface IUseCase<in TRequest, TResponse>
{
    Task<OperationResult<TResponse>> ExecuteAsync(
        TRequest request,
        CancellationToken cancellationToken = default);
}