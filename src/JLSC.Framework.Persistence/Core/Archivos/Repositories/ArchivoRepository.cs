using JLSC.Framework.Application.Core.Archivos.Interfaces;
using JLSC.Framework.Domain.Core.Archivos.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Archivos.Repositories;

public sealed class ArchivoRepository : IArchivoRepository
{
    private readonly JLSCDbContext _context;

    public ArchivoRepository(
        JLSCDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task AgregarAsync(
        Archivo archivo,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(archivo);

        await _context.Archivos.AddAsync(
            archivo,
            cancellationToken);
    }
    public async Task<Archivo?> ObtenerPorIdAsync(
    long archivoId,
    CancellationToken cancellationToken = default)
    {
        return await _context.Archivos
            .Include(x => x.CarpetaArchivo)
            .Include(x => x.TipoArchivo)
            .Include(x => x.EstadoArchivo)
            .Include(x => x.ProveedorAlmacenamiento)
            .FirstOrDefaultAsync(
                x => x.Id == archivoId,
                cancellationToken);
    }

    public Task ActualizarAsync(
    Archivo archivo,
    CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(archivo);

        _context.Archivos.Update(archivo);

        return Task.CompletedTask;
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }
}