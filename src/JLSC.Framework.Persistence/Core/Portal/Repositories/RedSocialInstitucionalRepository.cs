using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Portal.Repositories;

public sealed class RedSocialInstitucionalRepository
    : IRedSocialInstitucionalRepository
{
    private readonly JLSCDbContext _context;

    public RedSocialInstitucionalRepository(
        JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<List<RedSocialInstitucional>> GetAllAsync(
        long configuracionInstitucionalId,
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.RedesSocialesInstitucionales
            .AsNoTracking()
            .Include(x => x.CatalogoItem)
            .Where(x =>
                x.ConfiguracionInstitucionalId ==
                configuracionInstitucionalId);

        if (!incluirInactivos)
            query = query.Where(x => x.Activo);

        return await query
            .OrderBy(x => x.Orden)
            .ThenBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<RedSocialInstitucional?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _context.RedesSocialesInstitucionales
            .Include(x => x.CatalogoItem)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<RedSocialInstitucional?> GetByPublicIdAsync(
        Guid publicId,
        CancellationToken cancellationToken = default)
    {
        return await _context.RedesSocialesInstitucionales
            .AsNoTracking()
            .Include(x => x.CatalogoItem)
            .FirstOrDefaultAsync(
                x => x.PublicId == publicId,
                cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        long configuracionInstitucionalId,
        long catalogoItemId,
        long? excluirId = null,
        CancellationToken cancellationToken = default)
    {
        return await _context.RedesSocialesInstitucionales
            .AnyAsync(
                x =>
                    x.ConfiguracionInstitucionalId ==
                    configuracionInstitucionalId &&
                    x.CatalogoItemId == catalogoItemId &&
                    (!excluirId.HasValue || x.Id != excluirId.Value),
                cancellationToken);
    }

    public void Add(
        RedSocialInstitucional redSocial)
    {
        ArgumentNullException.ThrowIfNull(redSocial);
        _context.RedesSocialesInstitucionales.Add(redSocial);
    }

    public void Update(
        RedSocialInstitucional redSocial)
    {
        ArgumentNullException.ThrowIfNull(redSocial);
        _context.RedesSocialesInstitucionales.Update(redSocial);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}