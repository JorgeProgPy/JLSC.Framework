namespace JLSC.Framework.Application.Core.Seguridad.Commands.CrearRol;

public sealed class CrearRolRequest
{
    public string Codigo { get; init; } = string.Empty;

    public string Nombre { get; init; } = string.Empty;

    public string? Descripcion { get; init; }
}