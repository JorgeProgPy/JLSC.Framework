namespace JLSC.Framework.Domain.Core.Seguridad.Exceptions;

using JLSC.Framework.Domain.Common.Exceptions;

public sealed class PermisoException : BusinessException
{
    public PermisoException(string message)
        : base(message)
    {
    }
}