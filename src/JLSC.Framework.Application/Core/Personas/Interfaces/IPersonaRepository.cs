using JLSC.Framework.Domain.Core.Personas.Entities;

namespace JLSC.Framework.Application.Core.Personas.Interfaces;

// ==========================
// Repositorio de Personas
// ==========================

public interface IPersonaRepository
{
    // ==========================
    // Consultas
    // ==========================

    Task<Persona?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<Persona?> GetByNumeroDocumentoAsync(
        string numeroDocumento,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsNumeroDocumentoAsync(
        string numeroDocumento,
        CancellationToken cancellationToken = default);

    Task<Persona?> ObtenerPorIdAsync(
        long personaId,
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyCollection<Persona> Personas, int TotalRegistros)>
        ObtenerPaginaAsync(
            int pagina,
            int tamanoPagina,
            string? buscar,
            bool? activo,
            CancellationToken cancellationToken = default);

    // ==========================
    // Comandos
    // ==========================

    Task AddAsync(
        Persona persona,
        CancellationToken cancellationToken = default);

    void Update(
        Persona persona);

    void Remove(
        Persona persona);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}