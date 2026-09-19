using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Domain.Core.Portal.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Portal.Repositories;

public sealed class ConfiguracionInstitucionalRepository
    : IConfiguracionInstitucionalRepository
{
    private readonly JLSCDbContext _context;

    public ConfiguracionInstitucionalRepository(
        JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<ConfiguracionInstitucional?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _context.ConfiguracionesInstitucionales
            .Include(x => x.LogoPrincipalArchivo)
            .Include(x => x.LogoSecundarioArchivo)
            .Include(x => x.FaviconArchivo)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<ConfiguracionInstitucional?> GetActivaAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.ConfiguracionesInstitucionales
            .AsNoTracking()
            .Include(x => x.LogoPrincipalArchivo)
            .Include(x => x.LogoSecundarioArchivo)
            .Include(x => x.FaviconArchivo)
            .FirstOrDefaultAsync(
                x => x.Activo,
                cancellationToken);
    }

    public void Add(
        ConfiguracionInstitucional configuracion)
    {
        ArgumentNullException.ThrowIfNull(configuracion);
        _context.ConfiguracionesInstitucionales.Add(configuracion);
    }

    public void Update(
        ConfiguracionInstitucional configuracion)
    {
        ArgumentNullException.ThrowIfNull(configuracion);
        _context.ConfiguracionesInstitucionales.Update(configuracion);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}