namespace JLSC.Framework.Application.Core.Seguridad.Comands.ActualizarRol;

public sealed class ActualizarRolRequest
{
    public long Id { get; init; }

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public bool Activo { get; init; }
}