namespace JLSC.Framework.Infrastructure.Core.Archivos.MimeTypes;

public static class MimeTypeResolver
{
    private const string MimeTypePorDefecto =
        "application/octet-stream";

    private static readonly IReadOnlyDictionary<string, string> MimeTypes =
        new Dictionary<string, string>(
            StringComparer.OrdinalIgnoreCase)
        {
            // ==========================
            // Imágenes
            // ==========================

            [".jpg"] = "image/jpeg",
            [".jpeg"] = "image/jpeg",
            [".png"] = "image/png",
            [".gif"] = "image/gif",
            [".bmp"] = "image/bmp",
            [".webp"] = "image/webp",
            [".svg"] = "image/svg+xml",
            [".ico"] = "image/x-icon",

            // ==========================
            // Documentos
            // ==========================

            [".pdf"] = "application/pdf",
            [".txt"] = "text/plain",
            [".csv"] = "text/csv",
            [".rtf"] = "application/rtf",

            // ==========================
            // Microsoft Word
            // ==========================

            [".doc"] = "application/msword",
            [".docx"] =
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",

            // ==========================
            // Microsoft Excel
            // ==========================

            [".xls"] = "application/vnd.ms-excel",
            [".xlsx"] =
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",

            // ==========================
            // Microsoft PowerPoint
            // ==========================

            [".ppt"] = "application/vnd.ms-powerpoint",
            [".pptx"] =
                "application/vnd.openxmlformats-officedocument.presentationml.presentation",

            // ==========================
            // Audio
            // ==========================

            [".mp3"] = "audio/mpeg",
            [".wav"] = "audio/wav",
            [".ogg"] = "audio/ogg",
            [".m4a"] = "audio/mp4",

            // ==========================
            // Video
            // ==========================

            [".mp4"] = "video/mp4",
            [".avi"] = "video/x-msvideo",
            [".mov"] = "video/quicktime",
            [".wmv"] = "video/x-ms-wmv",
            [".webm"] = "video/webm",

            // ==========================
            // Comprimidos
            // ==========================

            [".zip"] = "application/zip",
            [".rar"] = "application/vnd.rar",
            [".7z"] = "application/x-7z-compressed",
            [".gz"] = "application/gzip",

            // ==========================
            // Web
            // ==========================

            [".html"] = "text/html",
            [".htm"] = "text/html",
            [".css"] = "text/css",
            [".js"] = "text/javascript",
            [".json"] = "application/json",
            [".xml"] = "application/xml"
        };

    public static string ObtenerPorNombreArchivo(
        string nombreArchivo)
    {
        if (string.IsNullOrWhiteSpace(nombreArchivo))
        {
            return MimeTypePorDefecto;
        }

        var extension = Path.GetExtension(nombreArchivo);

        return ObtenerPorExtension(extension);
    }

    public static string ObtenerPorExtension(
        string? extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
        {
            return MimeTypePorDefecto;
        }

        var extensionNormalizada = extension.StartsWith('.')
            ? extension
            : $".{extension}";

        return MimeTypes.TryGetValue(
            extensionNormalizada,
            out var mimeType)
                ? mimeType
                : MimeTypePorDefecto;
    }
}