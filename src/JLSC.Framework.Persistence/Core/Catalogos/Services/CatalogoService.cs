using JLSC.Framework.Application.Core.Catalogos.Interfaces;
using JLSC.Framework.Domain.Core.Catalogos.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Catalogos.Services;

public sealed class CatalogoService : ICatalogoService
{
    private readonly JLSCDbContext _context;

    public CatalogoService(
        JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<CatalogoItem?> ObtenerItemAsync(
        string codigoCatalogo,
        string codigoItem,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(codigoCatalogo);
        ArgumentException.ThrowIfNullOrWhiteSpace(codigoItem);

        return await _context.CatalogoItems
            .Include(x => x.Catalogo)
            .FirstOrDefaultAsync(
                x => x.Catalogo.Codigo == codigoCatalogo &&
                     x.Codigo == codigoItem,
                cancellationToken);
    }

    public async Task<long> ObtenerItemIdAsync(
        string codigoCatalogo,
        string codigoItem,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(codigoCatalogo);
        ArgumentException.ThrowIfNullOrWhiteSpace(codigoItem);

        var id = await _context.CatalogoItems
            .Where(x =>
                x.Catalogo.Codigo == codigoCatalogo &&
                x.Codigo == codigoItem)
            .Select(x => (long?)x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (!id.HasValue)
        {
            throw new InvalidOperationException(
                $"No existe el catálogo '{codigoCatalogo}' con el código '{codigoItem}'.");
        }

        return id.Value;
    }

    public async Task<bool> ExisteItemAsync(
        long catalogoItemId,
        CancellationToken cancellationToken = default)
    {
        if (catalogoItemId <= 0)
        {
            return false;
        }

        return await _context.CatalogoItems
            .AnyAsync(
                x => x.Id == catalogoItemId,
                cancellationToken);
    }
}