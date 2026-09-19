using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Portal.Repositories;

public sealed class IndicadorInstitucionalRepository
    : IIndicadorInstitucionalRepository
{
    private readonly JLSCDbContext _context;

    public IndicadorInstitucionalRepository(
        JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<List<IndicadorInstitucional>> GetAllAsync(
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.IndicadoresInstitucionales
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

    public async Task<IndicadorInstitucional?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _context.IndicadoresInstitucionales
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IndicadorInstitucional?> GetByPublicIdAsync(
        Guid publicId,
        CancellationToken cancellationToken = default)
    {
        return await _context.IndicadoresInstitucionales
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.PublicId == publicId,
                cancellationToken);
    }

    public void Add(
        IndicadorInstitucional indicador)
    {
        ArgumentNullException.ThrowIfNull(indicador);

        _context.IndicadoresInstitucionales.Add(indicador);
    }

    public void Update(
        IndicadorInstitucional indicador)
    {
        ArgumentNullException.ThrowIfNull(indicador);

        _context.IndicadoresInstitucionales.Update(indicador);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}