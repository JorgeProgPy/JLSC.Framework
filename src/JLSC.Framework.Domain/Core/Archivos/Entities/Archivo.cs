using JLSC.Framework.Domain.Common.Helpers;
using JLSC.Framework.Domain.Common.Exceptions;
using JLSC.Framework.Domain.Core.Archivos.Constants;

using JLSC.Framework.Domain.Common.Entities;
using JLSC.Framework.Domain.Core.Catalogos.Entities;
using JLSC.Framework.Domain.Core.Archivos.ValueObjects;
using System.IO;

namespace JLSC.Framework.Domain.Core.Archivos.Entities;

public class Archivo : AuditableEntity
{
    // ==========================
    // Identificación
    // ==========================



    public string Nombre { get; set; } = null!;

    public string NombreOriginal { get; set; } = null!;

    public string NombreAlmacenado { get; set; } = null!;

    // ==========================
    // Organización
    // ==========================

    public long CarpetaArchivoId { get; set; }

    // ==========================
    // Almacenamiento
    // ==========================

    /// <summary>
    /// Identificador único dentro del proveedor de almacenamiento.
    /// Ejemplos:
    /// logos/8f3c9d2a.png
    /// documentos/contrato.pdf
    /// bucket/portal/banner.jpg
    /// </summary>
    public string ClaveAlmacenamiento { get; set; } = null!;

    public string Extension { get; set; } = null!;

    public string MimeType { get; set; } = null!;

    public long TamanoBytes { get; set; }

    /// <summary>
    /// Hash SHA-256 del archivo para validar integridad y detectar duplicados.
    /// </summary>
    public string Hash { get; set; } = null!;

    // ==========================
    // Catálogos
    // ==========================

    public long TipoArchivoId { get; set; }

    public long EstadoArchivoId { get; set; }

    public long ProveedorAlmacenamientoId { get; set; }

    public long? PropietarioPersonaId { get; private set; }

    // ==========================
    // Versionado
    // ==========================

    public long? ArchivoAnteriorId { get; set; }

    // ==========================
    // Configuración
    // ==========================

    public bool EsPublico { get; set; }

    public string? Descripcion { get; set; }

    // ==========================
    // Navegación
    // ==========================

    public virtual CarpetaArchivo CarpetaArchivo { get; set; } = null!;

    public virtual CatalogoItem TipoArchivo { get; set; } = null!;

    public virtual CatalogoItem EstadoArchivo { get; set; } = null!;

    public virtual CatalogoItem ProveedorAlmacenamiento { get; set; } = null!;

    public virtual Archivo? ArchivoAnterior { get; set; }

    public virtual ICollection<Archivo> Versiones { get; set; }
        = new List<Archivo>();

    // ==========================
    // Fábrica
    // ==========================

    public static Archivo Create(
        ArchivoStorageInfo storageInfo,
        long carpetaArchivoId,
        long tipoArchivoId,
        long estadoArchivoId,
        long proveedorAlmacenamientoId,
        bool esPublico,
        long? propietarioPersonaId = null,
        string? descripcion = null)
    {
        ArgumentNullException.ThrowIfNull(storageInfo);

        return new Archivo
        {
            Nombre = Path.GetFileNameWithoutExtension(
                storageInfo.NombreOriginal),

            NombreOriginal = storageInfo.NombreOriginal,

            NombreAlmacenado = storageInfo.NombreAlmacenado,

            CarpetaArchivoId = carpetaArchivoId,

            ClaveAlmacenamiento = storageInfo.ClaveAlmacenamiento,

            Extension = storageInfo.Extension,

            MimeType = storageInfo.MimeType,

            TamanoBytes = storageInfo.TamanoBytes,

            Hash = storageInfo.Hash,

            TipoArchivoId = tipoArchivoId,

            EstadoArchivoId = estadoArchivoId,

            ProveedorAlmacenamientoId = proveedorAlmacenamientoId,

            PropietarioPersonaId = propietarioPersonaId,

            EsPublico = esPublico,

            Descripcion = descripcion
        };
    }

    // ==========================
    // Comportamiento
    // ==========================

    public void CambiarEstado(long estadoArchivoId)
    {
        if (estadoArchivoId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(estadoArchivoId));
        }

        EstadoArchivoId = estadoArchivoId;
    }

    public void CambiarDescripcion(string? descripcion)
    {
        Descripcion = string.IsNullOrWhiteSpace(descripcion)
            ? null
            : descripcion.Trim();
    }

    public void CambiarVisibilidad(bool esPublico)
    {
        EsPublico = esPublico;
    }

    public void MoverACarpeta(long carpetaArchivoId)
    {
        if (carpetaArchivoId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(carpetaArchivoId));
        }

        CarpetaArchivoId = carpetaArchivoId;
    }

    public void AsignarPropietarioPersona(long? propietarioPersonaId)
    {
        if (propietarioPersonaId.HasValue &&
            propietarioPersonaId.Value <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(propietarioPersonaId));
        }

        PropietarioPersonaId = propietarioPersonaId;
    }
}