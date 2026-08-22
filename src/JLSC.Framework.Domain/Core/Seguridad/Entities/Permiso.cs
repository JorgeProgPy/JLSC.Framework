using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Common.Helpers;
using JLSC.Framework.Domain.Core.Seguridad.Constants;

namespace JLSC.Framework.Domain.Core.Seguridad.Entities;

/// <summary>
/// Representa un permiso del sistema.
/// </summary>
public class Permiso : AuditableEntity
{
    // =====================================================
    // Constantes
    // =====================================================

    public const int MaxNombreLength = 150;



    public const int MaxDescripcionLength = 500;

    // =====================================================
    // Constructor
    // =====================================================

    protected Permiso()
    {
    }

    // =====================================================
    // Propiedades
    // =====================================================

    /// <summary>
    /// Módulo al que pertenece.
    /// </summary>
    public long ModuloId { get; private set; }

    /// <summary>
    /// Nombre del permiso.
    /// </summary>
    public string Nombre { get; private set; } = string.Empty;

    
   

    /// <summary>
    /// Descripción del permiso.
    /// </summary>
    public string? Descripcion { get; private set; }

    /// <summary>
    /// Orden de visualización.
    /// </summary>
    public int Orden { get; private set; }

    // =====================================================
    // Navegación
    // =====================================================

    public Modulo Modulo { get; private set; } = null!;

    // =====================================================
    // Factory
    // =====================================================

    public static Permiso Crear(
        long moduloId,
        string codigo,
        string nombre,
       
        string? descripcion = null,
        int orden = 0)
    {
        var permiso = new Permiso();

        DomainValidator.RequiredId(
            moduloId,
            PermisoMessages.ModuloObligatorio);

        permiso.ModuloId = moduloId;

        permiso.AsignarCodigo(codigo);

        permiso.AsignarNombre(nombre);

        permiso.AsignarDescripcion(descripcion);

        permiso.AsignarOrden(orden);

        return permiso;
    }

    // =====================================================
    // Comportamiento
    // =====================================================

    /// <summary>
    /// Cambia el nombre del permiso.
    /// </summary>
    public void CambiarNombre(string nombre)
    {
        AsignarNombre(nombre);
    }


    /// <summary>
    /// Cambia la descripción del permiso.
    /// </summary>
    public void CambiarDescripcion(string? descripcion)
    {
        AsignarDescripcion(descripcion);
    }

    /// <summary>
    /// Cambia el orden del permiso.
    /// </summary>
    public void CambiarOrden(int orden)
    {
        AsignarOrden(orden);
    }

    // =====================================================
    // Métodos privados
    // =====================================================

    /// <summary>
    /// Asigna el nombre del permiso.
    /// </summary>
    private void AsignarNombre(string nombre)
    {
        nombre = TextNormalizer.NormalizeRequired(
            nombre,
            PermisoMessages.NombreObligatorio);

        DomainValidator.MaxLength(
            nombre,
            MaxNombreLength,
            PermisoMessages.NombreLongitudMaxima);

        Nombre = nombre;
    }

   
    /// <summary>
    /// Asigna la descripción del permiso.
    /// </summary>
    private void AsignarDescripcion(string? descripcion)
    {
        descripcion = TextNormalizer.NormalizeOptional(descripcion);

        DomainValidator.MaxLength(
            descripcion,
            MaxDescripcionLength,
            PermisoMessages.DescripcionLongitudMaxima);

        Descripcion = descripcion;
    }

    /// <summary>
    /// Asigna el orden del permiso.
    /// </summary>
    private void AsignarOrden(int orden)
    {
        DomainValidator.MinValue(
            orden,
            0,
            PermisoMessages.OrdenInvalido);

        Orden = orden;
    }
}