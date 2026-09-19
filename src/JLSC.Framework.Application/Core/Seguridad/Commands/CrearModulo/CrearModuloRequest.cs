namespace JLSC.Framework.Application.Core.Seguridad.Commands.CrearModulo;

public sealed class CrearModuloRequest
{
    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }

    public string? Icono { get; init; }

    public string? Color { get; init; }

    public int Orden { get; init; }

    public bool Visible { get; init; } = true;
}