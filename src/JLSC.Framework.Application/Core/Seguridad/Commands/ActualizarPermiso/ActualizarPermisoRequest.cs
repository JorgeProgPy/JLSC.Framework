namespace JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarPermiso;

public sealed class ActualizarPermisoRequest
{
    public long Id { get; init; }

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public int Orden { get; init; }

    public bool Activo { get; init; }
}