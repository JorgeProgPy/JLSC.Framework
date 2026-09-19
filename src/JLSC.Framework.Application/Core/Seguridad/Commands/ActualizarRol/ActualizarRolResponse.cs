namespace JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarRol;

public sealed class ActualizarRolResponse
{
    public long RolId { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public bool Activo { get; init; }
}