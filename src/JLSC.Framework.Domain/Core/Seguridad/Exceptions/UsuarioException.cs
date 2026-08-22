namespace JLSC.Framework.Domain.Core.Seguridad.Exceptions;

using JLSC.Framework.Domain.Common.Exceptions;

public sealed class UsuarioException : BusinessException
{
    public UsuarioException(string message)
        : base(message)
    {
    }


}