using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Personas.Interfaces;

namespace JLSC.Framework.Application.Core.Personas.Queries.ObtenerPersonas;

public sealed class ObtenerPersonasHandler
    : IUseCase<ObtenerPersonasRequest, ObtenerPersonasResponse>
{
    private readonly IPersonaRepository _personaRepository;

    public ObtenerPersonasHandler(
        IPersonaRepository personaRepository)
    {
        ArgumentNullException.ThrowIfNull(personaRepository);

        _personaRepository = personaRepository;
    }

    public async Task<OperationResult<ObtenerPersonasResponse>> ExecuteAsync(
        ObtenerPersonasRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // ---------------------------------------------------------
        // Validaciones
        // ---------------------------------------------------------

        var pagina = request.Pagina <= 0
            ? 1
            : request.Pagina;

        var tamanoPagina = request.TamanoPagina <= 0
            ? 20
            : request.TamanoPagina;

        // ---------------------------------------------------------
        // Obtener información
        // ---------------------------------------------------------

        var (personas, totalRegistros) =
            await _personaRepository.ObtenerPaginaAsync(
                pagina,
                tamanoPagina,
                request.Buscar,
                request.Activo,
                cancellationToken);

        // ---------------------------------------------------------
        // Construcción de la respuesta
        // ---------------------------------------------------------

        var response = new ObtenerPersonasResponse
        {
            Pagina = pagina,
            TamanoPagina = tamanoPagina,
            TotalRegistros = totalRegistros,
            TotalPaginas = totalRegistros == 0
                ? 0
                : (int)Math.Ceiling((double)totalRegistros / tamanoPagina),

            Personas = personas
                .Select(persona => new PersonaResumenResponse
                {
                    PersonaId = persona.Id,

                    TipoPersona = persona.TipoPersona.Nombre,

                    TipoDocumento = persona.TipoDocumento.Nombre,

                    NumeroDocumento = persona.NumeroDocumento,

                    Nombre = !string.IsNullOrWhiteSpace(persona.RazonSocial)
                        ? persona.RazonSocial
                        : string.Join(
                            " ",
                            new[]
                            {
                                persona.Nombres,
                                persona.PrimerApellido,
                                persona.SegundoApellido
                            }
                            .Where(x => !string.IsNullOrWhiteSpace(x))),

                    Activo = persona.Activo
                })
                .ToList()
        };

        // ---------------------------------------------------------
        // Resultado
        // ---------------------------------------------------------

        return OperationResult<ObtenerPersonasResponse>.SuccessResult(
            response);
    }
}