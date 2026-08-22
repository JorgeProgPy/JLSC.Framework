using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Domain.Core.Personas.Constants;
using JLSC.Framework.Application.Core.Personas.Interfaces;

namespace JLSC.Framework.Application.Core.Personas.Queries.ObtenerPersona;

public sealed class ObtenerPersonaHandler
    : IUseCase<ObtenerPersonaRequest, ObtenerPersonaResponse>
{
    private readonly IPersonaRepository _personaRepository;

    public ObtenerPersonaHandler(
        IPersonaRepository personaRepository)
    {
        ArgumentNullException.ThrowIfNull(personaRepository);

        _personaRepository = personaRepository;
    }

    public async Task<OperationResult<ObtenerPersonaResponse>> ExecuteAsync(
        ObtenerPersonaRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // -----------------------------------------------------------------
        // Validaciones
        // -----------------------------------------------------------------

        if (request.PersonaId <= 0)
        {
            return OperationResult<ObtenerPersonaResponse>.Failure(
                PersonaMessages.PersonaInvalida);
        }

        // -----------------------------------------------------------------
        // Obtener Persona
        // -----------------------------------------------------------------

        var persona = await _personaRepository.ObtenerPorIdAsync(
            request.PersonaId,
            cancellationToken);

        if (persona is null)
        {
            return OperationResult<ObtenerPersonaResponse>.Failure(
                PersonaMessages.PersonaNoExiste);
        }

        // -----------------------------------------------------------------
        // Construcción de la respuesta
        // -----------------------------------------------------------------

        var response = new ObtenerPersonaResponse
        {
            PersonaId = persona.Id,

            TipoPersona = persona.TipoPersona.Nombre,

            TipoDocumento = persona.TipoDocumento.Nombre,

            NumeroDocumento = persona.NumeroDocumento,

            Complemento = persona.Complemento,

            Nit = persona.Nit,

            Nombres = persona.Nombres,

            PrimerApellido = persona.PrimerApellido,

            SegundoApellido = persona.SegundoApellido,

            FechaNacimiento = persona.FechaNacimiento,

            Genero = persona.Genero?.Nombre,

            EstadoCivil = persona.EstadoCivil?.Nombre,

            RazonSocial = persona.RazonSocial,

            NombreComercial = persona.NombreComercial,

            Sigla = persona.Sigla,

            Observacion = persona.Observacion,

            Activo = persona.Activo
        };

        // -----------------------------------------------------------------
        // Resultado
        // -----------------------------------------------------------------

        return OperationResult<ObtenerPersonaResponse>.SuccessResult(
            response);
    }
}