using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Portal.Repositories;

public sealed class AccesoRapidoRepository : IAccesoRapidoRepository
{
    private readonly JLSCDbContext _context;

    public AccesoRapidoRepository(JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<List<AccesoRapido>> GetAllAsync(
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.AccesosRapidos
            .AsNoTracking()
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

    public async Task<AccesoRapido?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _context.AccesosRapidos
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<AccesoRapido?> GetByPublicIdAsync(
        Guid publicId,
        CancellationToken cancellationToken = default)
    {
        return await _context.AccesosRapidos
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.PublicId == publicId,
                cancellationToken);
    }

    public void Add(AccesoRapido acceso)
    {
        ArgumentNullException.ThrowIfNull(acceso);
        _context.AccesosRapidos.Add(acceso);
    }

    public void Update(AccesoRapido acceso)
    {
        ArgumentNullException.ThrowIfNull(acceso);
        _context.AccesosRapidos.Update(acceso);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}