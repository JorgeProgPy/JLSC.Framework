namespace JLSC.Framework.Domain.Core.Seguridad.Exceptions;

using JLSC.Framework.Domain.Common.Exceptions;

public sealed class RolException : BusinessException
{
    public RolException(string message)
        : base(message)
    {
    }
}