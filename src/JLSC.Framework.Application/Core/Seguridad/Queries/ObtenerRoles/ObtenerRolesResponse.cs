namespace JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerRoles;

public sealed class ObtenerRolesResponse
{
    public long Id { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public bool Activo { get; init; }
}