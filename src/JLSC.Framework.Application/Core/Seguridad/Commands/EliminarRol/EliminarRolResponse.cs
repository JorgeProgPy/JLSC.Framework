namespace JLSC.Framework.Application.Core.Seguridad.Commands.EliminarRol;

public sealed class EliminarRolResponse
{
    public long RolId { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public bool Activo { get; init; }
}