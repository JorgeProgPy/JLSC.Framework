using JLSC.Framework.Domain.Core.Catalogos.Entities;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Database.Seeders;

public static class CoreDataSeeder
{
    public static async Task SeedAsync(JLSCDbContext context)
    {
        // ==========================
        // Definiciones
        // ==========================

        var definiciones = ObtenerDefiniciones();

        var codigosCatalogo = definiciones
            .Select(x => x.Codigo)
            .ToArray();

        // ==========================
        // 1. Cargar catálogos existentes
        // ==========================

        var catalogosExistentes = await context.Catalogos
            .Where(x => codigosCatalogo.Contains(x.Codigo))
            .ToDictionaryAsync(x => x.Codigo);

        // ==========================
        // 2. Crear catálogos faltantes
        // ==========================

        foreach (var definicion in definiciones)
        {
            if (catalogosExistentes.ContainsKey(definicion.Codigo))
            {
                continue;
            }

            var catalogo = new Catalogo
            {
                Codigo = definicion.Codigo,
                Nombre = definicion.Nombre,
                EsSistema = true,
                Activo = true
            };

            context.Catalogos.Add(catalogo);

            catalogosExistentes.Add(
                definicion.Codigo,
                catalogo);
        }

        // Necesario para obtener los Id de los catálogos nuevos.
        await context.SaveChangesAsync();

        // ==========================
        // 3. Obtener Id de catálogos
        // ==========================

        var catalogoIds = catalogosExistentes
            .Values
            .Select(x => x.Id)
            .ToArray();

        // ==========================
        // 4. Cargar ítems existentes
        // ==========================

        var itemsExistentes = await context.CatalogoItems
            .Where(x => catalogoIds.Contains(x.CatalogoId))
            .Select(x => new
            {
                x.CatalogoId,
                x.Codigo
            })
            .ToListAsync();

        // ==========================
        // 5. Crear índice en memoria
        // ==========================

        var clavesExistentes = itemsExistentes
            .Select(x => (x.CatalogoId, x.Codigo))
            .ToHashSet();

        // ==========================
        // 6. Crear ítems faltantes
        // ==========================

        foreach (var definicionCatalogo in definiciones)
        {
            var catalogo = catalogosExistentes[
                definicionCatalogo.Codigo];

            foreach (var definicionItem in definicionCatalogo.Items)
            {
                var clave = (
                    catalogo.Id,
                    definicionItem.Codigo);

                if (clavesExistentes.Contains(clave))
                {
                    continue;
                }

                var item = new CatalogoItem
                {
                    CatalogoId = catalogo.Id,
                    Codigo = definicionItem.Codigo,
                    Nombre = definicionItem.Nombre,
                    Valor = definicionItem.Valor,
                    Descripcion = definicionItem.Descripcion,
                    Icono = definicionItem.Icono,
                    Orden = definicionItem.Orden,
                    Activo = true
                };

                context.CatalogoItems.Add(item);

                clavesExistentes.Add(clave);
            }
        }

        // ==========================
        // 7. Guardar ítems nuevos
        // ==========================

        await context.SaveChangesAsync();
    }

    // ============================================================
    // DEFINICIONES DE DATOS MAESTROS
    // ============================================================

    private static IReadOnlyCollection<CatalogoSeedDefinition>
        ObtenerDefiniciones()
    {
        return
        [
            new CatalogoSeedDefinition(
                "TIPO_ARCHIVO",
                "Tipo de archivo",
                [
                    new CatalogoItemSeedDefinition(
                        "IMAGEN",
                        "Imagen",
                        "IMAGE",
                        "Archivos de imagen.",
                        "bi bi-image",
                        1),

                    new CatalogoItemSeedDefinition(
                        "DOCUMENTO",
                        "Documento",
                        "DOCUMENT",
                        "Documentos de propósito general.",
                        "bi bi-file-earmark-text",
                        2),

                    new CatalogoItemSeedDefinition(
                        "PDF",
                        "PDF",
                        "PDF",
                        "Documentos en formato PDF.",
                        "bi bi-file-earmark-pdf",
                        3),

                    new CatalogoItemSeedDefinition(
                        "VIDEO",
                        "Video",
                        "VIDEO",
                        "Archivos de video.",
                        "bi bi-camera-video",
                        4),

                    new CatalogoItemSeedDefinition(
                        "AUDIO",
                        "Audio",
                        "AUDIO",
                        "Archivos de audio.",
                        "bi bi-file-earmark-music",
                        5),

                    new CatalogoItemSeedDefinition(
                        "HOJA_CALCULO",
                        "Hoja de cálculo",
                        "SPREADSHEET",
                        "Archivos de hojas de cálculo.",
                        "bi bi-file-earmark-spreadsheet",
                        6),

                    new CatalogoItemSeedDefinition(
                        "COMPRIMIDO",
                        "Archivo comprimido",
                        "COMPRESSED",
                        "Archivos comprimidos.",
                        "bi bi-file-earmark-zip",
                        7),

                    new CatalogoItemSeedDefinition(
                        "OTRO",
                        "Otro",
                        "OTHER",
                        "Archivos que no pertenecen a una categoría específica.",
                        "bi bi-file-earmark",
                        99)
                ]),

            new CatalogoSeedDefinition(
                "ESTADO_ARCHIVO",
                "Estado de archivo",
                [
                    new CatalogoItemSeedDefinition(
                        "ACTIVO",
                        "Activo",
                        "ACTIVE",
                        "Archivo disponible para su uso.",
                        "bi bi-check-circle",
                        1),

                    new CatalogoItemSeedDefinition(
                        "PENDIENTE",
                        "Pendiente",
                        "PENDING",
                        "Archivo pendiente de procesamiento o revisión.",
                        "bi bi-clock",
                        2),

                    new CatalogoItemSeedDefinition(
                        "BLOQUEADO",
                        "Bloqueado",
                        "BLOCKED",
                        "Archivo bloqueado temporalmente.",
                        "bi bi-lock",
                        3),

                    new CatalogoItemSeedDefinition(
                        "ARCHIVADO",
                        "Archivado",
                        "ARCHIVED",
                        "Archivo conservado como histórico.",
                        "bi bi-archive",
                        4)
                ]),

            new CatalogoSeedDefinition(
                "PROVEEDOR_ALMACENAMIENTO",
                "Proveedor de almacenamiento",
                [
                    new CatalogoItemSeedDefinition(
                        "LOCAL",
                        "Almacenamiento local",
                        "LOCAL",
                        "Archivos almacenados en el sistema de archivos local.",
                        "bi bi-hdd",
                        1),

                    new CatalogoItemSeedDefinition(
                        "NAS",
                        "Almacenamiento NAS",
                        "NAS",
                        "Archivos almacenados en un dispositivo NAS.",
                        "bi bi-device-hdd",
                        2),

                    new CatalogoItemSeedDefinition(
                        "MINIO",
                        "MinIO",
                        "MINIO",
                        "Almacenamiento de objetos mediante MinIO.",
                        "bi bi-database",
                        3),

                    new CatalogoItemSeedDefinition(
                        "AZURE_BLOB",
                        "Azure Blob Storage",
                        "AZURE_BLOB",
                        "Almacenamiento de objetos mediante Azure Blob Storage.",
                        "bi bi-cloud",
                        4),

                    new CatalogoItemSeedDefinition(
                        "AMAZON_S3",
                        "Amazon S3",
                        "AMAZON_S3",
                        "Almacenamiento de objetos mediante Amazon S3.",
                        "bi bi-cloud",
                        5)
                ])
        ];
    }

    // ============================================================
    // DEFINICIONES INTERNAS
    // ============================================================

    private sealed record CatalogoSeedDefinition(
        string Codigo,
        string Nombre,
        IReadOnlyCollection<CatalogoItemSeedDefinition> Items);

    private sealed record CatalogoItemSeedDefinition(
        string Codigo,
        string Nombre,
        string Valor,
        string Descripcion,
        string Icono,
        int Orden);
}