using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Core.Catalogos.Entities;

namespace JLSC.Framework.Domain.Core.Archivos.Entities;

public class ArchivoReferencia : AuditableEntity
{
    // ==========================
    // Relación
    // ==========================

    public long ArchivoId { get; private set; }

    // ==========================
    // Destino
    // ==========================

    public long ModuloId { get; private set; }

    public long EntidadId { get; private set; }

    public long TipoUsoId { get; private set; }

    // ==========================
    // Configuración
    // ==========================

    public int Orden { get; private set; }

    public bool EsPrincipal { get; private set; }

    public string? Observacion { get; private set; }

    // ==========================
    // Navegación
    // ==========================

    public virtual Archivo Archivo { get; private set; } = null!;

    public virtual CatalogoItem Modulo { get; private set; } = null!;

    public virtual CatalogoItem TipoUso { get; private set; } = null!;

    // ==========================
    // Fábrica
    // ==========================

    public static ArchivoReferencia Create(
        long archivoId,
        long moduloId,
        long entidadId,
        long tipoUsoId,
        int orden = 1,
        bool esPrincipal = false,
        string? observacion = null)
    {
        if (archivoId <= 0)
            throw new ArgumentOutOfRangeException(nameof(archivoId));

        if (moduloId <= 0)
            throw new ArgumentOutOfRangeException(nameof(moduloId));

        if (entidadId <= 0)
            throw new ArgumentOutOfRangeException(nameof(entidadId));

        if (tipoUsoId <= 0)
            throw new ArgumentOutOfRangeException(nameof(tipoUsoId));

        if (orden <= 0)
            throw new ArgumentOutOfRangeException(nameof(orden));

        var referencia = new ArchivoReferencia
        {
            ArchivoId = archivoId,
            ModuloId = moduloId,
            EntidadId = entidadId,
            TipoUsoId = tipoUsoId,
            Orden = orden,
            Activo = true,
            Observacion = string.IsNullOrWhiteSpace(observacion)
                ? null
                : observacion.Trim()
        };

        if (esPrincipal)
        {
            referencia.MarcarComoPrincipal();
        }

        return referencia;
    }

    // ==========================
    // Comportamiento
    // ==========================

    public void MarcarComoPrincipal()
    {
        EsPrincipal = true;
    }

    public void QuitarComoPrincipal()
    {
        EsPrincipal = false;
    }

    public void Reordenar(int orden)
    {
        if (orden <= 0)
            throw new ArgumentOutOfRangeException(nameof(orden));

        Orden = orden;
    }

 
    public void CambiarObservacion(string? observacion)
    {
        Observacion = string.IsNullOrWhiteSpace(observacion)
            ? null
            : observacion.Trim();
    }
}