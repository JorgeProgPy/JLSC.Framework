using Microsoft.EntityFrameworkCore;
using JLSC.Framework.Application.Core.Personas.Interfaces;
using JLSC.Framework.Persistence.Core.Database;
using JLSC.Framework.Domain.Core.Personas.Entities;

namespace JLSC.Framework.Persistence.Core.Personas.Repositories;

// ==========================
// Repositorio de Personas
// ==========================

public class PersonaRepository : IPersonaRepository
{
    private readonly JLSCDbContext _context;

    public PersonaRepository(
        JLSCDbContext context)
    {
        _context = context;
    }

    // ==========================
    // Consultas
    // ==========================

    public async Task<Persona?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Personas
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Persona?> GetByNumeroDocumentoAsync(
        string numeroDocumento,
        CancellationToken cancellationToken = default)
    {
        return await _context.Personas
            .FirstOrDefaultAsync(
                x => x.NumeroDocumento == numeroDocumento,
                cancellationToken);
    }

    public async Task<bool> ExistsNumeroDocumentoAsync(
        string numeroDocumento,
        CancellationToken cancellationToken = default)
    {
        return await _context.Personas
            .AnyAsync(
                x => x.NumeroDocumento == numeroDocumento,
                cancellationToken);
    }

    // ==========================
    // Comandos
    // ==========================

    public async Task AddAsync(
        Persona persona,
        CancellationToken cancellationToken = default)
    {
        await _context.Personas.AddAsync(
            persona,
            cancellationToken);
    }

    public void Update(
        Persona persona)
    {
        _context.Personas.Update(persona);
    }

    public void Remove(
        Persona persona)
    {
        _context.Personas.Remove(persona);
    }
    public async Task GuardarCambiosAsync(
    CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task<Persona?> ObtenerPorIdAsync(
    long personaId,
    CancellationToken cancellationToken = default)
    {
        return await _context.Personas
            .Include(x => x.TipoPersona)
            .Include(x => x.TipoDocumento)
            .Include(x => x.Genero)
            .Include(x => x.EstadoCivil)
            .FirstOrDefaultAsync(
                x => x.Id == personaId,
                cancellationToken);
    }

    public async Task<(IReadOnlyCollection<Persona> Personas, int TotalRegistros)>
    ObtenerPaginaAsync(
        int pagina,
        int tamanoPagina,
        string? buscar,
        bool? activo,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Persona> query = _context.Personas
            .Include(x => x.TipoPersona)
            .Include(x => x.TipoDocumento)
            .Include(x => x.Genero)
            .Include(x => x.EstadoCivil);

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            buscar = buscar.Trim();

            query = query.Where(x =>
                x.NumeroDocumento.Contains(buscar) ||
                (x.Nombres != null && x.Nombres.Contains(buscar)) ||
                (x.PrimerApellido != null && x.PrimerApellido.Contains(buscar)) ||
                (x.SegundoApellido != null && x.SegundoApellido.Contains(buscar)) ||
                (x.RazonSocial != null && x.RazonSocial.Contains(buscar)));
        }

        if (activo.HasValue)
        {
            query = query.Where(x => x.Activo == activo.Value);
        }

        var totalRegistros = await query.CountAsync(cancellationToken);

        var personas = await query
            .OrderBy(x => x.Id)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(cancellationToken);

        return (personas, totalRegistros);
    }
}