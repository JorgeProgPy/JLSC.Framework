namespace  JLSC.Framework.Domain.Core.Archivos.Constants;

public static class ArchivoMessages
{
    // ==========================
    // Validaciones
    // ==========================

    public const string ContenidoObligatorio =
        "El contenido del archivo es obligatorio.";

    public const string NombreObligatorio =
        "El nombre del archivo es obligatorio.";

    public const string CarpetaObligatoria =
        "Debe especificar la carpeta destino.";

    // ==========================
    // Carpetas
    // ==========================

    public static string CarpetaNoExiste(
        string codigoCarpeta)
        => $"No existe la carpeta '{codigoCarpeta}'.";

    public static string CarpetaInactiva(
        string codigoCarpeta)
        => $"La carpeta '{codigoCarpeta}' se encuentra inactiva.";

    // ==========================
    // Catálogos
    // ==========================

    public const string TipoArchivoNoConfigurado =
        "No se encontró el tipo de archivo configurado.";

    public const string EstadoArchivoNoConfigurado =
        "No se encontró el estado del archivo.";

    public const string ProveedorNoConfigurado =
        "No se encontró el proveedor de almacenamiento.";

    // ==========================
    // Archivo
    // ==========================

    public const string ArchivoInvalido =
    "Debe especificar un archivo válido.";

    public const string ArchivoNoExiste =
        "El archivo especificado no existe.";

    public const string ArchivoGuardado =
        "Archivo guardado correctamente.";

    public const string ArchivoActualizado =
        "Archivo actualizado correctamente.";

    public const string ArchivoYaEliminado =
   "El archivo ya se encuentra eliminado.";

    public const string ArchivoEliminado =
        "Archivo eliminado correctamente.";
    
}