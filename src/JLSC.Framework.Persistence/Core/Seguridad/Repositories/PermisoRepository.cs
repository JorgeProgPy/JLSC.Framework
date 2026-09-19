using JLSC.Framework.Application.Core.Seguridad.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Seguridad.Repositories;

public sealed class PermisoRepository : IPermisoRepository
{
    private readonly JLSCDbContext _context;

    public PermisoRepository(JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<Permiso?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Permisos
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<List<Permiso>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Permisos
            .AsNoTracking()
            .OrderBy(x => x.Nombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsCodigoAsync(
        string codigo,
        long? excludeId = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Permisos
            .AsNoTracking()
            .Where(x => x.Codigo == codigo);

        if (excludeId.HasValue)
        {
            query = query.Where(x => x.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public void Add(Permiso permiso)
    {
        ArgumentNullException.ThrowIfNull(permiso);
        _context.Permisos.Add(permiso);
    }

    public void Update(Permiso permiso)
    {
        ArgumentNullException.ThrowIfNull(permiso);
        _context.Permisos.Update(permiso);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}