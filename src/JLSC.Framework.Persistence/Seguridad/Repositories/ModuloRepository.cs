using JLSC.Framework.Application.Core.Seguridad.Interfaces;
using JLSC.Framework.Domain.Core.Seguridad.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Seguridad.Repositories;

public sealed class ModuloRepository : IModuloRepository
{
    private readonly JLSCDbContext _context;

    public ModuloRepository(JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    // ==========================
    // Consultas
    // ==========================

    public async Task<Modulo?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Modulos
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Modulo?> GetByCodigoAsync(
        string codigo,
        CancellationToken cancellationToken = default)
    {
        return await _context.Modulos
            .FirstOrDefaultAsync(
                x => x.Codigo == codigo,
                cancellationToken);
    }

    public async Task<bool> ExistsCodigoAsync(
        string codigo,
        CancellationToken cancellationToken = default)
    {
        return await _context.Modulos
            .AnyAsync(
                x => x.Codigo == codigo,
                cancellationToken);
    }

    public async Task<IReadOnlyCollection<Modulo>> GetAllAsync(
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Modulo> query = _context.Modulos
            .AsNoTracking();

        if (!incluirInactivos)
        {
            query = query.Where(x => x.Activo);
        }

        return await query
            .OrderBy(x => x.Orden)
            .ThenBy(x => x.Nombre)
            .ToListAsync(cancellationToken);
    }

    // ==========================
    // Comandos
    // ==========================

    public async Task AddAsync(
        Modulo modulo,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(modulo);

        await _context.Modulos.AddAsync(
            modulo,
            cancellationToken);
    }

    public void Update(Modulo modulo)
    {
        ArgumentNullException.ThrowIfNull(modulo);

        _context.Modulos.Update(modulo);
    }

    public void Remove(Modulo modulo)
    {
        ArgumentNullException.ThrowIfNull(modulo);

        _context.Modulos.Remove(modulo);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}