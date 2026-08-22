using JLSC.Framework.Application.Core.Archivos.Interfaces;
using JLSC.Framework.Domain.Core.Archivos.Entities;
using JLSC.Framework.Persistence.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace JLSC.Framework.Persistence.Core.Archivos.Repositories;

public sealed class ArchivoReferenciaRepository
    : IArchivoReferenciaRepository
{
    private readonly JLSCDbContext _context;

    public ArchivoReferenciaRepository(
        JLSCDbContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(
        ArchivoReferencia archivoReferencia,
        CancellationToken cancellationToken = default)
    {
        await _context.ArchivoReferencias.AddAsync(
            archivoReferencia,
            cancellationToken);
    }

    public Task ActualizarAsync(
        ArchivoReferencia archivoReferencia,
        CancellationToken cancellationToken = default)
    {
        _context.ArchivoReferencias.Update(archivoReferencia);

        return Task.CompletedTask;
    }

    public async Task<ArchivoReferencia?> ObtenerPorIdAsync(
        long archivoReferenciaId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ArchivoReferencias
            .Include(x => x.Archivo)
            .Include(x => x.Modulo)
            .Include(x => x.TipoUso)
            .FirstOrDefaultAsync(
                x => x.Id == archivoReferenciaId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<ArchivoReferencia>> ObtenerPorEntidadAsync(
        long moduloId,
        long entidadId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ArchivoReferencias
            .Include(x => x.Archivo)
            .Where(x =>
                x.ModuloId == moduloId &&
                x.EntidadId == entidadId)
            .OrderBy(x => x.Orden)
            .ToListAsync(cancellationToken);
    }

    public async Task<ArchivoReferencia?> ObtenerPrincipalAsync(
        long moduloId,
        long entidadId,
        long tipoUsoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ArchivoReferencias
            .FirstOrDefaultAsync(x =>
                x.ModuloId == moduloId &&
                x.EntidadId == entidadId &&
                x.TipoUsoId == tipoUsoId &&
                x.EsPrincipal &&
                x.Activo,
                cancellationToken);
    }

    public async Task<bool> ExisteAsync(
        long archivoId,
        long moduloId,
        long entidadId,
        long tipoUsoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ArchivoReferencias
            .AnyAsync(x =>
                x.ArchivoId == archivoId &&
                x.ModuloId == moduloId &&
                x.EntidadId == entidadId &&
                x.TipoUsoId == tipoUsoId &&
                x.Activo,
                cancellationToken);
    }

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}