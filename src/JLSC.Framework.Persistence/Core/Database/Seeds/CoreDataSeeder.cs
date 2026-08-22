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
        // Catálogos existentes
        // ==========================

        var catalogosExistentes = await context.Catalogos
            .Where(x => codigosCatalogo.Contains(x.Codigo))
            .ToDictionaryAsync(x => x.Codigo);

        // ==========================
        // Crear catálogos faltantes
        // ==========================

        foreach (var definicion in definiciones)
        {
            if (catalogosExistentes.ContainsKey(definicion.Codigo))
                continue;

            var catalogo = Catalogo.Create(
                definicion.Codigo,
                definicion.Nombre,
                true);

            context.Catalogos.Add(catalogo);

            catalogosExistentes.Add(
                definicion.Codigo,
                catalogo);
        }

        // Guardar para obtener los Id generados

        await context.SaveChangesAsync();

        // ==========================
        // Obtener Ids
        // ==========================

        var catalogoIds = catalogosExistentes
            .Values
            .Select(x => x.Id)
            .ToArray();

        // ==========================
        // Ítems existentes
        // ==========================

        var itemsExistentes = await context.CatalogoItems
            .Where(x => catalogoIds.Contains(x.CatalogoId))
            .Select(x => new
            {
                x.CatalogoId,
                x.Codigo
            })
            .ToListAsync();

        var clavesExistentes = itemsExistentes
            .Select(x => (x.CatalogoId, x.Codigo))
            .ToHashSet();

        // ==========================
        // Crear ítems
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
                    continue;

                var item = CatalogoItem.Create(
                    catalogo.Id,
                    definicionItem.Codigo,
                    definicionItem.Nombre,
                    definicionItem.Valor,
                    definicionItem.Descripcion,
                    definicionItem.Icono,
                    definicionItem.Orden);

                context.CatalogoItems.Add(item);

                clavesExistentes.Add(clave);
            }
        }

        // ==========================
        // Guardar cambios
        // ==========================

        await context.SaveChangesAsync();
    }
    private static IReadOnlyCollection<CatalogoSeedDefinition> ObtenerDefiniciones()
    {
        return
        [
            new CatalogoSeedDefinition(
                "TIPO_ARCHIVO",
                "Tipos de Archivo",
                [
                    new CatalogoItemSeedDefinition(
                        "JPG",
                        "JPG",
                        "JPG",
                        "Imágenes en formato JPG.",
                        "bi bi-file-earmark-image",
                        1),

                    new CatalogoItemSeedDefinition(
                        "PNG",
                        "PNG",
                        "PNG",
                        "Imágenes en formato PNG.",
                        "bi bi-file-earmark-image",
                        2),

                    new CatalogoItemSeedDefinition(
                        "PDF",
                        "PDF",
                        "PDF",
                        "Documentos en formato PDF.",
                        "bi bi-file-earmark-pdf",
                        3),

                    new CatalogoItemSeedDefinition(
                        "DOCX",
                        "Word",
                        "DOCX",
                        "Documentos de Microsoft Word.",
                        "bi bi-file-earmark-word",
                        4),

                    new CatalogoItemSeedDefinition(
                        "XLSX",
                        "Excel",
                        "XLSX",
                        "Hojas de cálculo de Microsoft Excel.",
                        "bi bi-file-earmark-excel",
                        5)
                ]),

            new CatalogoSeedDefinition(
                "ESTADO_ARCHIVO",
                "Estados del Archivo",
                [
                    new CatalogoItemSeedDefinition(
                        "ACTIVO",
                        "Activo",
                        "ACTIVO",
                        "Archivo disponible.",
                        "bi bi-check-circle",
                        1),

                    new CatalogoItemSeedDefinition(
                        "INACTIVO",
                        "Inactivo",
                        "INACTIVO",
                        "Archivo deshabilitado.",
                        "bi bi-x-circle",
                        2),

                    new CatalogoItemSeedDefinition(
                        "ELIMINADO",
                        "Eliminado",
                        "ELIMINADO",
                        "Archivo eliminado lógicamente.",
                        "bi bi-trash",
                        3)
                ]),

            new CatalogoSeedDefinition(
                "PROVEEDOR_ALMACENAMIENTO",
                "Proveedor de Almacenamiento",
                [
                    new CatalogoItemSeedDefinition(
                        "LOCAL",
                        "Servidor Local",
                        "LOCAL",
                        "Archivos almacenados en el servidor.",
                        "bi bi-hdd",
                        1),

                    new CatalogoItemSeedDefinition(
                        "AZURE",
                        "Azure Blob Storage",
                        "AZURE",
                        "Archivos almacenados en Azure.",
                        "bi bi-cloud",
                        2),

                    new CatalogoItemSeedDefinition(
                        "AWS",
                        "Amazon S3",
                        "AWS",
                        "Archivos almacenados en Amazon S3.",
                        "bi bi-cloud-upload",
                        3)
                ])
        ];
    }
    // ==========================
    // Definiciones
    // ==========================

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
