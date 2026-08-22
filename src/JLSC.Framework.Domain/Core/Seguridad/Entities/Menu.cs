using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Common.Exceptions;
using JLSC.Framework.Domain.Common.Helpers;
using JLSC.Framework.Domain.Core.Seguridad.Constants;

namespace JLSC.Framework.Domain.Core.Seguridad.Entities;

/// <summary>
/// Representa un menú del sistema.
/// </summary>
public class Menu : AuditableEntity
{
    // =====================================================
    // Constantes
    // =====================================================

    public const int MaxNombreLength = 150;

    public const int MaxRutaLength = 250;

    public const int MaxIconoLength = 100;

    // =====================================================
    // Constructor
    // =====================================================

    protected Menu()
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
    /// Menú padre.
    /// </summary>
    public long? MenuPadreId { get; private set; }

    /// <summary>
    /// Nombre del menú.
    /// </summary>
    public string Nombre { get; private set; } = string.Empty;

    /// <summary>
    /// Ruta del menú.
    /// </summary>
    public string Ruta { get; private set; } = string.Empty;

    /// <summary>
    /// Icono.
    /// </summary>
    public string? Icono { get; private set; }

    /// <summary>
    /// Orden.
    /// </summary>
    public int Orden { get; private set; }

    /// <summary>
    /// Indica si debe mostrarse en el menú.
    /// </summary>
    public bool MostrarEnMenu { get; private set; }

    // =====================================================
    // Navegación
    // =====================================================

    public Modulo Modulo { get; private set; } = null!;

    public Menu? MenuPadre { get; private set; }

    // =====================================================
    // Factory
    // =====================================================

    public static Menu Crear(
        long moduloId,
        string codigo,
        string nombre,
        string ruta,
        string? icono = null,
        long? menuPadreId = null,
        int orden = 0,
        bool mostrarEnMenu = true)
    {
        var menu = new Menu();

        DomainValidator.RequiredId(
            moduloId,
            MenuMessages.ModuloObligatorio);

        menu.ModuloId = moduloId;

        menu.AsignarCodigo(codigo);

        menu.AsignarNombre(nombre);

        menu.AsignarRuta(ruta);

        menu.AsignarIcono(icono);

        menu.AsignarMenuPadre(menuPadreId);

        menu.AsignarOrden(orden);

        menu.MostrarEnMenu = mostrarEnMenu;

        return menu;
    }

    // =====================================================
    // Comportamiento
    // =====================================================

    /// <summary>
    /// Cambia el nombre del menú.
    /// </summary>
    public void CambiarNombre(string nombre)
    {
        AsignarNombre(nombre);
    }

    /// <summary>
    /// Cambia la ruta del menú.
    /// </summary>
    public void CambiarRuta(string ruta)
    {
        AsignarRuta(ruta);
    }

    /// <summary>
    /// Cambia el icono del menú.
    /// </summary>
    public void CambiarIcono(string? icono)
    {
        AsignarIcono(icono);
    }

    /// <summary>
    /// Cambia el orden del menú.
    /// </summary>
    public void CambiarOrden(int orden)
    {
        AsignarOrden(orden);
    }

    /// <summary>
    /// Cambia el menú padre.
    /// </summary>
    public void CambiarMenuPadre(long? menuPadreId)
    {
        AsignarMenuPadre(menuPadreId);
    }

    /// <summary>
    /// Muestra el menú.
    /// </summary>
    public void Mostrar()
    {
        MostrarEnMenu = true;
    }

    /// <summary>
    /// Oculta el menú.
    /// </summary>
    public void Ocultar()
    {
        MostrarEnMenu = false;
    }

    // =====================================================
    // Métodos privados
    // =====================================================

    /// <summary>
    /// Asigna el nombre del menú.
    /// </summary>
    private void AsignarNombre(string nombre)
    {
        nombre = TextNormalizer.NormalizeRequired(
            nombre,
            MenuMessages.NombreObligatorio);

        DomainValidator.MaxLength(
            nombre,
            MaxNombreLength,
            MenuMessages.NombreLongitudMaxima);

        Nombre = nombre;
    }

    /// <summary>
    /// Asigna la ruta del menú.
    /// </summary>
    private void AsignarRuta(string ruta)
    {
        ruta = TextNormalizer.NormalizeRequired(
            ruta,
            MenuMessages.RutaObligatoria);

        DomainValidator.MaxLength(
            ruta,
            MaxRutaLength,
            MenuMessages.RutaLongitudMaxima);

        Ruta = ruta;
    }

    /// <summary>
    /// Asigna el icono del menú.
    /// </summary>
    private void AsignarIcono(string? icono)
    {
        icono = TextNormalizer.NormalizeOptional(icono);

        DomainValidator.MaxLength(
            icono,
            MaxIconoLength,
            MenuMessages.IconoLongitudMaxima);

        Icono = icono;
    }

    /// <summary>
    /// Asigna el orden del menú.
    /// </summary>
    private void AsignarOrden(int orden)
    {
        DomainValidator.MinValue(
            orden,
            0,
            MenuMessages.OrdenInvalido);

        Orden = orden;
    }

    /// <summary>
    /// Asigna el menú padre.
    /// </summary>
    private void AsignarMenuPadre(long? menuPadreId)
    {
        DomainValidator.OptionalId(
            menuPadreId,
            MenuMessages.MenuPadreInvalido);

        if (menuPadreId.HasValue && menuPadreId.Value == Id)
        {
            throw new DomainValidationException(
                MenuMessages.MenuPadreInvalido);
        }

        MenuPadreId = menuPadreId;
    }
}