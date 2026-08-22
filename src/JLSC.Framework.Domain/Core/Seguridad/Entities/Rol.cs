using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Common.Helpers;
using JLSC.Framework.Domain.Common.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Constants;
namespace JLSC.Framework.Domain.Core.Seguridad.Entities;

/// <summary>
/// Representa un rol del sistema.
/// </summary>
public class Rol : AuditableEntity, IAggregateRoot
{
    // =====================================================
    // Constantes
    // =====================================================

    public const int MaxNombreLength = 150;

    public const int MaxDescripcionLength = 500;

    // =====================================================
    // Constructor
    // =====================================================

    protected Rol()
    {
    }

    // =====================================================
    // Propiedades
    // =====================================================

    /// <summary>
    /// Nombre del rol.
    /// </summary>
    public string Nombre { get; private set; } = string.Empty;

    /// <summary>
    /// Descripción del rol.
    /// </summary>
    public string? Descripcion { get; private set; }

    // =====================================================
    // Factory
    // =====================================================

    public static Rol Crear(
        string codigo,
        string nombre,
        string? descripcion = null)
    {
        var rol = new Rol();

        rol.AsignarCodigo(codigo);

        rol.AsignarNombre(nombre);

        rol.AsignarDescripcion(descripcion);

        return rol;
    }

    // =====================================================
    // Comportamiento
    // =====================================================

    /// <summary>
    /// Cambia el nombre del rol.
    /// </summary>
    public void CambiarNombre(string nombre)
    {
        AsignarNombre(nombre);
    }

    /// <summary>
    /// Cambia la descripción del rol.
    /// </summary>
    public void CambiarDescripcion(string? descripcion)
    {
        AsignarDescripcion(descripcion);
    }


    // =====================================================
    // Métodos privados
    // =====================================================

    /// <summary>
    /// Asigna el nombre del rol.
    /// </summary>
    private void AsignarNombre(string nombre)
    {
        nombre = TextNormalizer.NormalizeRequired(
            nombre,
            RolMessages.NombreObligatorio);

        DomainValidator.MaxLength(
            nombre,
            MaxNombreLength,
            RolMessages.NombreLongitudMaxima);

        Nombre = nombre;
    }

    /// <summary>
    /// Asigna la descripción del rol.
    /// </summary>
    private void AsignarDescripcion(string? descripcion)
    {
        descripcion = TextNormalizer.NormalizeOptional(descripcion);

        DomainValidator.MaxLength(
            descripcion,
            MaxDescripcionLength,
            RolMessages.DescripcionLongitudMaxima);

        Descripcion = descripcion;
    }
}
