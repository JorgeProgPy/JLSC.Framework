using JLSC.Framework.Application.Core.Seguridad.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Seguridad.Repositories;

public sealed class RolRepository : IRolRepository
{
    private readonly JLSCDbContext _context;

    public RolRepository(JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<Rol?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<List<Rol>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Roles
            .AsNoTracking()
            .OrderBy(x => x.Nombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsCodigoAsync(
        string codigo,
        long? excludeId = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Roles
            .AsNoTracking()
            .Where(x => x.Codigo == codigo);

        if (excludeId.HasValue)
        {
            query = query.Where(
                x => x.Id != excludeId.Value);
        }

        return await query.AnyAsync(
            cancellationToken);
    }

    public void Add(Rol rol)
    {
        ArgumentNullException.ThrowIfNull(rol);

        _context.Roles.Add(rol);
    }

    public void Update(Rol rol)
    {
        ArgumentNullException.ThrowIfNull(rol);

        _context.Roles.Update(rol);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }
}