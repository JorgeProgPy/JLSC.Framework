using System.Security.Cryptography;
using System.Text;
using JLSC.Framework.Contracts.Core.Archivos.Models;
using JLSC.Framework.Infrastructure.Core.Archivos.Storage.Local.Options;
using JLSC.Framework.Infrastructure.Core.Archivos.Storage.Local.Services;
using Microsoft.Extensions.Options;

namespace JLSC.Framework.Infrastructure.Tests.Core.Archivos.Storage.Local;

public sealed class LocalStorageServiceTests : IDisposable
{
    private readonly string _rutaBase;
    private readonly LocalStorageService _storageService;

    public LocalStorageServiceTests()
    {
        _rutaBase = Path.Combine(
            Path.GetTempPath(),
            "JLSC.Framework.Tests",
            Guid.NewGuid().ToString("N"));

        var options = Options.Create(
            new LocalStorageOptions
            {
                RutaBase = _rutaBase
            });

        _storageService = new LocalStorageService(options);
    }

    [Fact]
    public async Task CicloCompletoAsync_DebeGuardarObtenerYEliminarArchivo()
    {
        // ==========================
        // Arrange
        // ==========================

        const string contenidoOriginal =
            "Primera prueba real del almacenamiento JLSC Framework.";

        var bytesOriginales =
            Encoding.UTF8.GetBytes(contenidoOriginal);

        await using var contenido = new MemoryStream(
            bytesOriginales);

        var request = new StorageFileRequest
        {
            Contenido = contenido,
            NombreOriginal = "documento-prueba.txt",
            Carpeta = "pruebas/documentos"
        };

        var hashEsperado = Convert
            .ToHexString(
                SHA256.HashData(bytesOriginales))
            .ToLowerInvariant();

        // ==========================
        // Act - Guardar
        // ==========================

        var resultado = await _storageService.GuardarAsync(
            request);

        // ==========================
        // Assert - Resultado
        // ==========================

        Assert.Equal(
            "documento-prueba.txt",
            resultado.NombreOriginal);

        Assert.EndsWith(
            ".txt",
            resultado.NombreAlmacenado);

        Assert.StartsWith(
            "pruebas/documentos/",
            resultado.ClaveAlmacenamiento);

        Assert.Equal(
            ".txt",
            resultado.Extension);

        Assert.Equal(
            "text/plain",
            resultado.MimeType);

        Assert.Equal(
            bytesOriginales.LongLength,
            resultado.TamanoBytes);

        Assert.Equal(
            hashEsperado,
            resultado.Hash);

        // ==========================
        // Act y Assert - Existe
        // ==========================

        var existeDespuesDeGuardar =
            await _storageService.ExisteAsync(
                resultado.ClaveAlmacenamiento);

        Assert.True(existeDespuesDeGuardar);

        // ==========================
        // Act y Assert - Obtener
        // ==========================

        string contenidoObtenido;

        await using (var archivoObtenido =
            await _storageService.ObtenerAsync(
                resultado.ClaveAlmacenamiento))
        {
            using var reader = new StreamReader(
                archivoObtenido,
                Encoding.UTF8);

            contenidoObtenido =
                await reader.ReadToEndAsync();
        }

        Assert.Equal(
            contenidoOriginal,
            contenidoObtenido);

        // ==========================
        // Act - Eliminar
        // ==========================

        await _storageService.EliminarAsync(
            resultado.ClaveAlmacenamiento);

        // ==========================
        // Assert - Eliminación
        // ==========================

        var existeDespuesDeEliminar =
            await _storageService.ExisteAsync(
                resultado.ClaveAlmacenamiento);

        Assert.False(existeDespuesDeEliminar);
    }

    [Theory]
    [InlineData("../fuera")]
    [InlineData("../../fuera")]
    [InlineData("documentos/../fuera")]
    [InlineData(@"..\fuera")]
    [InlineData(@"documentos\..\fuera")]
    public async Task GuardarAsync_ConPathTraversalEnCarpeta_DebeRechazarOperacion(
        string carpeta)
    {
        // ==========================
        // Arrange
        // ==========================

        await using var contenido = new MemoryStream(
            Encoding.UTF8.GetBytes("contenido de prueba"));

        var request = new StorageFileRequest
        {
            Contenido = contenido,
            NombreOriginal = "archivo.txt",
            Carpeta = carpeta
        };

        // ==========================
        // Act
        // ==========================

        var excepcion = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _storageService.GuardarAsync(request));

        // ==========================
        // Assert
        // ==========================

        Assert.Equal(
            "La carpeta contiene segmentos de ruta no permitidos.",
            excepcion.Message);
    }

    [Theory]
    [InlineData("../archivo.txt")]
    [InlineData("../../archivo.txt")]
    [InlineData("documentos/../../../archivo.txt")]
    [InlineData(@"..\archivo.txt")]
    [InlineData(@"documentos\..\..\archivo.txt")]
    public async Task OperacionesConClavePathTraversal_DebenRechazarOperacion(
        string claveAlmacenamiento)
    {
        // ==========================
        // Act y Assert - Existe
        // ==========================

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _storageService.ExisteAsync(
                claveAlmacenamiento));

        // ==========================
        // Act y Assert - Obtener
        // ==========================

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _storageService.ObtenerAsync(
                claveAlmacenamiento));

        // ==========================
        // Act y Assert - Eliminar
        // ==========================

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _storageService.EliminarAsync(
                claveAlmacenamiento));
    }

    [Fact]
    public async Task ObtenerAsync_ArchivoInexistente_DebeLanzarFileNotFoundException()
    {
        // ==========================
        // Arrange
        // ==========================

        const string claveAlmacenamiento =
            "documentos/archivo-inexistente.txt";

        // ==========================
        // Act
        // ==========================

        var excepcion =
            await Assert.ThrowsAsync<FileNotFoundException>(
                () => _storageService.ObtenerAsync(
                    claveAlmacenamiento));

        // ==========================
        // Assert
        // ==========================

        Assert.Equal(
            "El archivo solicitado no existe.",
            excepcion.Message);

        Assert.Equal(
            claveAlmacenamiento,
            excepcion.FileName);
    }

    [Fact]
    public async Task EliminarAsync_ArchivoInexistente_NoDebeLanzarExcepcion()
    {
        // ==========================
        // Arrange
        // ==========================

        const string claveAlmacenamiento =
            "documentos/archivo-inexistente.txt";

        // ==========================
        // Act
        // ==========================

        var excepcion = await Record.ExceptionAsync(
            () => _storageService.EliminarAsync(
                claveAlmacenamiento));

        // ==========================
        // Assert
        // ==========================

        Assert.Null(excepcion);
    }

    [Fact]
    public async Task GuardarAsync_ExtensionDesconocida_DebeUsarMimeTypePorDefecto()
    {
        // ==========================
        // Arrange
        // ==========================

        await using var contenido = new MemoryStream(
            Encoding.UTF8.GetBytes(
                "contenido con extensión desconocida"));

        var request = new StorageFileRequest
        {
            Contenido = contenido,
            NombreOriginal = "archivo.xyzdesconocido",
            Carpeta = "pruebas/mime"
        };

        // ==========================
        // Act
        // ==========================

        var resultado =
            await _storageService.GuardarAsync(request);

        // ==========================
        // Assert
        // ==========================

        Assert.Equal(
            ".xyzdesconocido",
            resultado.Extension);

        Assert.Equal(
            "application/octet-stream",
            resultado.MimeType);

        Assert.True(
            await _storageService.ExisteAsync(
                resultado.ClaveAlmacenamiento));
    }

    [Fact]
    public async Task GuardarAsync_SiFallaLaEscritura_DebeEliminarArchivoParcial()
    {
        // ==========================
        // Arrange
        // ==========================

        var bytes = Encoding.UTF8.GetBytes(
            "Este contenido provocará una interrupción simulada.");

        await using var contenido =
            new StreamConFalloSimulado(
                bytes,
                bytesAntesDelFallo: 10);

        var request = new StorageFileRequest
        {
            Contenido = contenido,
            NombreOriginal = "archivo-interrumpido.txt",
            Carpeta = "pruebas/fallos"
        };

        // ==========================
        // Act
        // ==========================

        await Assert.ThrowsAsync<IOException>(
            () => _storageService.GuardarAsync(request));

        // ==========================
        // Assert
        // ==========================

        var carpetaDestino = Path.Combine(
            _rutaBase,
            "pruebas",
            "fallos");

        if (Directory.Exists(carpetaDestino))
        {
            Assert.Empty(
                Directory.GetFiles(
                    carpetaDestino,
                    "*",
                    SearchOption.AllDirectories));
        }
    }

    public void Dispose()
    {
        if (Directory.Exists(_rutaBase))
        {
            Directory.Delete(
                _rutaBase,
                recursive: true);
        }
    }
}

internal sealed class StreamConFalloSimulado : Stream
{
    private readonly MemoryStream _streamInterno;
    private readonly int _bytesAntesDelFallo;
    private int _bytesLeidos;

    public StreamConFalloSimulado(
        byte[] contenido,
        int bytesAntesDelFallo)
    {
        _streamInterno = new MemoryStream(contenido);
        _bytesAntesDelFallo = bytesAntesDelFallo;
    }

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override long Length =>
        _streamInterno.Length;

    public override long Position
    {
        get => _streamInterno.Position;
        set => throw new NotSupportedException();
    }

    public override void Flush()
    {
    }

    public override int Read(
        byte[] buffer,
        int offset,
        int count)
    {
        if (_bytesLeidos >= _bytesAntesDelFallo)
        {
            throw new IOException(
                "Fallo de lectura simulado.");
        }

        var bytesPermitidos = Math.Min(
            count,
            _bytesAntesDelFallo - _bytesLeidos);

        var leidos = _streamInterno.Read(
            buffer,
            offset,
            bytesPermitidos);

        _bytesLeidos += leidos;

        return leidos;
    }

    public override async ValueTask<int> ReadAsync(
        Memory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        if (_bytesLeidos >= _bytesAntesDelFallo)
        {
            throw new IOException(
                "Fallo de lectura simulado.");
        }

        var bytesPermitidos = Math.Min(
            buffer.Length,
            _bytesAntesDelFallo - _bytesLeidos);

        var leidos = await _streamInterno.ReadAsync(
            buffer[..bytesPermitidos],
            cancellationToken);

        _bytesLeidos += leidos;

        return leidos;
    }

    public override long Seek(
        long offset,
        SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    public override void SetLength(
        long value)
    {
        throw new NotSupportedException();
    }

    public override void Write(
        byte[] buffer,
        int offset,
        int count)
    {
        throw new NotSupportedException();
    }

    protected override void Dispose(
        bool disposing)
    {
        if (disposing)
        {
            _streamInterno.Dispose();
        }

        base.Dispose(disposing);
    }

    public override async ValueTask DisposeAsync()
    {
        await _streamInterno.DisposeAsync();

        GC.SuppressFinalize(this);
    }
}