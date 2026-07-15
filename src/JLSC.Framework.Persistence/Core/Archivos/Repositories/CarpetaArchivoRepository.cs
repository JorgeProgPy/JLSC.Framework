using JLSC.Framework.Application.Core.Archivos.Interfaces;
using JLSC.Framework.Domain.Core.Archivos.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Archivos.Repositories;

public sealed class CarpetaArchivoRepository
    : ICarpetaArchivoRepository
{
    private readonly JLSCDbContext _context;

    public CarpetaArchivoRepository(
        JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<CarpetaArchivo?> ObtenerPorCodigoAsync(
        string codigo,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(codigo);

        return await _context.CarpetasArchivo
            .FirstOrDefaultAsync(
                x => x.Codigo == codigo,
                cancellationToken);
    }
}