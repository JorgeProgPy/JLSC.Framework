using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Portal.Repositories;

public sealed class ProyectoDestacadoRepository
    : IProyectoDestacadoRepository
{
    private readonly JLSCDbContext _context;

    public ProyectoDestacadoRepository(
        JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<List<ProyectoDestacado>> GetAllAsync(
        bool incluirInactivos = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.ProyectosDestacados
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

    public async Task<ProyectoDestacado?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
        => await _context.ProyectosDestacados
            .Include(x => x.Archivo)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

    public async Task<ProyectoDestacado?> GetByPublicIdAsync(
        Guid publicId,
        CancellationToken cancellationToken = default)
        => await _context.ProyectosDestacados
            .AsNoTracking()
            .Include(x => x.Archivo)
            .FirstOrDefaultAsync(
                x => x.PublicId == publicId,
                cancellationToken);

    public void Add(ProyectoDestacado proyecto)
    {
        ArgumentNullException.ThrowIfNull(proyecto);
        _context.ProyectosDestacados.Add(proyecto);
    }

    public void Update(ProyectoDestacado proyecto)
    {
        ArgumentNullException.ThrowIfNull(proyecto);
        _context.ProyectosDestacados.Update(proyecto);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}