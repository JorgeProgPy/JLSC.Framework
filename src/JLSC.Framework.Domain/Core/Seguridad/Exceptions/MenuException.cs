namespace JLSC.Framework.Domain.Core.Seguridad.Exceptions;

using JLSC.Framework.Domain.Common.Exceptions;

public sealed class MenuException : BusinessException
{
    public MenuException(string message)
        : base(message)
    {
    }
}