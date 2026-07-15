using System.Security.Cryptography;
using JLSC.Framework.Contracts.Core.Archivos.Interfaces;
using JLSC.Framework.Contracts.Core.Archivos.Models;
using JLSC.Framework.Infrastructure.Core.Archivos.MimeTypes;
using JLSC.Framework.Infrastructure.Core.Archivos.Storage.Local.Options;
using Microsoft.Extensions.Options;


namespace JLSC.Framework.Infrastructure.Core.Archivos.Storage.Local.Services;

public sealed class LocalStorageService : IStorageService
{
    private readonly string _rutaBase;

    public LocalStorageService(
        IOptions<LocalStorageOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        var rutaBase = options.Value.RutaBase;

        if (string.IsNullOrWhiteSpace(rutaBase))
        {
            throw new InvalidOperationException(
                "La ruta base del almacenamiento local no está configurada.");
        }

        _rutaBase = Path.GetFullPath(rutaBase);
    }

    public async Task<StorageFileResult> GuardarAsync(
        StorageFileRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Contenido);

        if (string.IsNullOrWhiteSpace(request.NombreOriginal))
        {
            throw new ArgumentException(
                "El nombre original del archivo es obligatorio.",
                nameof(request));
        }

        var extension = Path
            .GetExtension(request.NombreOriginal)
            .ToLowerInvariant();

        var nombreAlmacenado =
            $"{Guid.NewGuid():N}{extension}";

        var carpetaNormalizada =
            NormalizarCarpeta(request.Carpeta);

        var claveAlmacenamiento =
            string.IsNullOrEmpty(carpetaNormalizada)
                ? nombreAlmacenado
                : $"{carpetaNormalizada}/{nombreAlmacenado}";

        var rutaDestino =
            ObtenerRutaFisica(claveAlmacenamiento);

        var directorioDestino =
            Path.GetDirectoryName(rutaDestino);

        if (!string.IsNullOrWhiteSpace(directorioDestino))
        {
            Directory.CreateDirectory(directorioDestino);
        }

        try
        {
            await using var archivoDestino = new FileStream(
                rutaDestino,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 81920,
                useAsync: true);

            using var sha256 = SHA256.Create();

            await using var cryptoStream = new CryptoStream(
                archivoDestino,
                sha256,
                CryptoStreamMode.Write,
                leaveOpen: true);

            await request.Contenido.CopyToAsync(
                cryptoStream,
                cancellationToken);

            await cryptoStream.FlushFinalBlockAsync(
                cancellationToken);

            var tamanoBytes = archivoDestino.Length;

            var hash = Convert
                .ToHexString(sha256.Hash!)
                .ToLowerInvariant();

            var mimeType =
                MimeTypeResolver.ObtenerPorNombreArchivo(
                    request.NombreOriginal);

            return new StorageFileResult
            {
                NombreOriginal = request.NombreOriginal,
                NombreAlmacenado = nombreAlmacenado,
                ClaveAlmacenamiento = claveAlmacenamiento,
                Extension = extension,
                MimeType = mimeType,
                TamanoBytes = tamanoBytes,
                Hash = hash
            };
        }
        catch
        {
            if (File.Exists(rutaDestino))
            {
                File.Delete(rutaDestino);
            }

            throw;
        }
    }

    public Task<Stream> ObtenerAsync(
        string claveAlmacenamiento,
        CancellationToken cancellationToken = default)
    {
        var rutaFisica =
            ObtenerRutaFisica(claveAlmacenamiento);

        if (!File.Exists(rutaFisica))
        {
            throw new FileNotFoundException(
                "El archivo solicitado no existe.",
                claveAlmacenamiento);
        }

        Stream stream = new FileStream(
            rutaFisica,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 81920,
            useAsync: true);

        return Task.FromResult(stream);
    }

    public Task<bool> ExisteAsync(
        string claveAlmacenamiento,
        CancellationToken cancellationToken = default)
    {
        var rutaFisica =
            ObtenerRutaFisica(claveAlmacenamiento);

        return Task.FromResult(
            File.Exists(rutaFisica));
    }

    public Task EliminarAsync(
        string claveAlmacenamiento,
        CancellationToken cancellationToken = default)
    {
        var rutaFisica =
            ObtenerRutaFisica(claveAlmacenamiento);

        if (File.Exists(rutaFisica))
        {
            File.Delete(rutaFisica);
        }

        return Task.CompletedTask;
    }

    private string ObtenerRutaFisica(
        string claveAlmacenamiento)
    {
        if (string.IsNullOrWhiteSpace(claveAlmacenamiento))
        {
            throw new ArgumentException(
                "La clave de almacenamiento es obligatoria.",
                nameof(claveAlmacenamiento));
        }

        var claveNormalizada = claveAlmacenamiento
            .Replace(
                '/',
                Path.DirectorySeparatorChar)
            .Replace(
                '\\',
                Path.DirectorySeparatorChar);

        var rutaFisica = Path.GetFullPath(
            Path.Combine(
                _rutaBase,
                claveNormalizada));

        var rutaBaseConSeparador =
            _rutaBase.EndsWith(
                Path.DirectorySeparatorChar)
                ? _rutaBase
                : _rutaBase +
                  Path.DirectorySeparatorChar;

        if (!rutaFisica.StartsWith(
                rutaBaseConSeparador,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "La clave de almacenamiento contiene una ruta no permitida.");
        }

        return rutaFisica;
    }

    private static string NormalizarCarpeta(
        string carpeta)
    {
        if (string.IsNullOrWhiteSpace(carpeta))
        {
            return string.Empty;
        }

        var segmentos = carpeta
            .Replace('\\', '/')
            .Split(
                '/',
                StringSplitOptions.RemoveEmptyEntries |
                StringSplitOptions.TrimEntries);

        if (segmentos.Any(
                x => x is "." or ".."))
        {
            throw new InvalidOperationException(
                "La carpeta contiene segmentos de ruta no permitidos.");
        }

        return string.Join('/', segmentos);
    }
}