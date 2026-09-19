namespace JLSC.Framework.Application.Core.Seguridad.Commands.CrearRol;

public sealed class CrearRolResponse
{
    public long RolId { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;
}