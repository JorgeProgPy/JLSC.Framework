namespace JLSC.Framework.Application.Core.Seguridad.Commands.EliminarModulo;

public sealed class EliminarModuloResponse
{
    public long Id { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public bool Activo { get; init; }
}