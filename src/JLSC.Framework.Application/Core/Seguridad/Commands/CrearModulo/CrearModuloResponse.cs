namespace JLSC.Framework.Application.Core.Seguridad.Commands.CrearModulo;

public sealed class CrearModuloResponse
{
    public long ModuloId { get; init; }

    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;
}