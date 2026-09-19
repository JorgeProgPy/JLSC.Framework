namespace JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarModulo;

public sealed class ActualizarModuloResponse
{
    public long Id { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;
}