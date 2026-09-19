using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Portal.Repositories;

public sealed class BannerRepository : IBannerRepository
{
    private readonly JLSCDbContext _context;

    public BannerRepository(JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<List<Banner>> GetAllAsync(
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Banners
            .AsNoTracking()
            .Include(x => x.Archivo)
            .AsQueryable();

        if (!incluirInactivos)
        {
            query = query.Where(x => x.Activo);
        }

        return await query
            .OrderBy(x => x.Orden)
            .ThenBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Banner?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Banners
            .Include(x => x.Archivo)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Banner?> GetByPublicIdAsync(
        Guid publicId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Banners
            .AsNoTracking()
            .Include(x => x.Archivo)
            .FirstOrDefaultAsync(
                x => x.PublicId == publicId,
                cancellationToken);
    }

    public void Add(Banner banner)
    {
        ArgumentNullException.ThrowIfNull(banner);

        _context.Banners.Add(banner);
    }

    public void Update(Banner banner)
    {
        ArgumentNullException.ThrowIfNull(banner);

        _context.Banners.Update(banner);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}