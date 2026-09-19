using JLSC.Framework.Application.Core.Seguridad.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Seguridad.Repositories;

public sealed class RolPermisoRepository : IRolPermisoRepository
{
    private readonly JLSCDbContext _context;

    public RolPermisoRepository(JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<List<RolPermiso>> GetByRolIdAsync(
        long rolId,
        CancellationToken cancellationToken = default)
    {
        return await _context.RolesPermisos
            .AsNoTracking()
            .Where(x => x.RolId == rolId)
            .OrderBy(x => x.PermisoId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        long rolId,
        long permisoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.RolesPermisos
            .AnyAsync(
                x => x.RolId == rolId &&
                     x.PermisoId == permisoId,
                cancellationToken);
    }

    public async Task<RolPermiso?> GetAsync(
        long rolId,
        long permisoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.RolesPermisos
            .FirstOrDefaultAsync(
                x => x.RolId == rolId &&
                     x.PermisoId == permisoId,
                cancellationToken);
    }

    public void Add(RolPermiso rolPermiso)
    {
        ArgumentNullException.ThrowIfNull(rolPermiso);

        _context.RolesPermisos.Add(rolPermiso);
    }

    public void Update(RolPermiso rolPermiso)
    {
        ArgumentNullException.ThrowIfNull(rolPermiso);

        _context.RolesPermisos.Update(rolPermiso);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}