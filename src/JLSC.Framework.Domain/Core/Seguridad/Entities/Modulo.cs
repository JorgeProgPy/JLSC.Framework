using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Common.Helpers;
using JLSC.Framework.Domain.Common.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Constants;

namespace JLSC.Framework.Domain.Core.Seguridad.Entities;

/// <summary>
/// Representa un módulo funcional del sistema.
/// </summary>
public class Modulo : AuditableEntity, IAggregateRoot
{
    // =====================================================
    // Constantes
    // =====================================================

    public const int MaxNombreLength = 150;

    public const int MaxDescripcionLength = 500;

    public const int MaxIconoLength = 100;

    public const int MaxColorLength = 20;

    // =====================================================
    // Constructor
    // =====================================================

    protected Modulo()
    {
    }

    // =====================================================
    // Propiedades
    // =====================================================

    /// <summary>
    /// Nombre del módulo.
    /// </summary>
    public string Nombre { get; private set; } = string.Empty;

    /// <summary>
    /// Descripción del módulo.
    /// </summary>
    public string? Descripcion { get; private set; }

    /// <summary>
    /// Icono asociado al módulo.
    /// </summary>
    public string? Icono { get; private set; }

    /// <summary>
    /// Color representativo del módulo.
    /// </summary>
    public string? Color { get; private set; }

    /// <summary>
    /// Orden de visualización.
    /// </summary>
    public int Orden { get; private set; }

    /// <summary>
    /// Indica si el módulo es visible.
    /// </summary>
    public bool Visible { get; private set; }

    // =====================================================
    // Factory
    // =====================================================

    public static Modulo Crear(
        string codigo,
        string nombre,
        string? descripcion = null,
        string? icono = null,
        string? color = null,
        int orden = 0,
        bool visible = true)
    {
        var modulo = new Modulo();

        modulo.AsignarCodigo(codigo);

        modulo.AsignarNombre(nombre);

        modulo.AsignarDescripcion(descripcion);

        modulo.AsignarIcono(icono);

        modulo.AsignarColor(color);

        modulo.AsignarOrden(orden);

        modulo.Visible = visible;

        return modulo;
    }

    // =====================================================
    // Comportamiento
    // =====================================================

    /// <summary>
    /// Cambia el nombre del módulo.
    /// </summary>
    /// <param name="nombre">Nuevo nombre.</param>
    public void CambiarNombre(string nombre)
    {
        AsignarNombre(nombre);
    }

    /// <summary>
    /// Cambia la descripción del módulo.
    /// </summary>
    /// <param name="descripcion">Nueva descripción.</param>
    public void CambiarDescripcion(string? descripcion)
    {
        AsignarDescripcion(descripcion);
    }

    /// <summary>
    /// Cambia el icono del módulo.
    /// </summary>
    /// <param name="icono">Nuevo icono.</param>
    public void CambiarIcono(string? icono)
    {
        AsignarIcono(icono);
    }

    /// <summary>
    /// Cambia el color del módulo.
    /// </summary>
    /// <param name="color">Nuevo color.</param>
    public void CambiarColor(string? color)
    {
        AsignarColor(color);
    }

    /// <summary>
    /// Cambia el orden de visualización.
    /// </summary>
    /// <param name="orden">Nuevo orden.</param>
    public void CambiarOrden(int orden)
    {
        AsignarOrden(orden);
    }

    /// <summary>
    /// Hace visible el módulo.
    /// </summary>
    public void Mostrar()
    {
        Visible = true;
    }

    /// <summary>
    /// Oculta el módulo.
    /// </summary>
    public void Ocultar()
    {
        Visible = false;
    }

    // =====================================================
    // Métodos privados
    // =====================================================

    /// <summary>
    /// Asigna el nombre del módulo.
    /// </summary>
    /// <param name="nombre">Nombre del módulo.</param>
    private void AsignarNombre(string nombre)
    {
        nombre = TextNormalizer.NormalizeRequired(
            nombre,
            ModuloMessages.NombreObligatorio);

        DomainValidator.MaxLength(
            nombre,
            MaxNombreLength,
            ModuloMessages.NombreLongitudMaxima);

        Nombre = nombre;
    }

    /// <summary>
    /// Asigna la descripción del módulo.
    /// </summary>
    /// <param name="descripcion">Descripción del módulo.</param>
    private void AsignarDescripcion(string? descripcion)
    {
        descripcion = TextNormalizer.NormalizeOptional(descripcion);

        DomainValidator.MaxLength(
            descripcion,
            MaxDescripcionLength,
            ModuloMessages.DescripcionLongitudMaxima);

        Descripcion = descripcion;
    }

    /// <summary>
    /// Asigna el icono del módulo.
    /// </summary>
    /// <param name="icono">Icono del módulo.</param>
    private void AsignarIcono(string? icono)
    {
        icono = TextNormalizer.NormalizeOptional(icono);

        DomainValidator.MaxLength(
            icono,
            MaxIconoLength,
            ModuloMessages.IconoLongitudMaxima);

        Icono = icono;
    }

    /// <summary>
    /// Asigna el color del módulo.
    /// </summary>
    /// <param name="color">Color del módulo.</param>
    private void AsignarColor(string? color)
    {
        color = TextNormalizer.NormalizeOptional(color);

        DomainValidator.MaxLength(
            color,
            MaxColorLength,
            ModuloMessages.ColorLongitudMaxima);

        Color = color;
    }

    /// <summary>
    /// Asigna el orden del módulo.
    /// </summary>
    /// <param name="orden">Orden de visualización.</param>
    private void AsignarOrden(int orden)
    {
        DomainValidator.MinValue(
            orden,
            0,
            ModuloMessages.OrdenInvalido);

        Orden = orden;
    }
}