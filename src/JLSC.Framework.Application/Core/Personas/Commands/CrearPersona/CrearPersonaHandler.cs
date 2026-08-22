using JLSC.Framework.Application.Common.Constants.Catalogos;
using JLSC.Framework.Application.Common.Interfaces;
using JLSC.Framework.Application.Common.Results;
using JLSC.Framework.Application.Core.Catalogos.Interfaces;
using JLSC.Framework.Domain.Core.Personas.Constants;
using JLSC.Framework.Application.Core.Personas.Interfaces;
using JLSC.Framework.Domain.Core.Catalogos.Entities;
using JLSC.Framework.Domain.Core.Personas.Entities;

namespace JLSC.Framework.Application.Core.Personas.Commands.CrearPersona;

public sealed class CrearPersonaHandler
    : IUseCase<CrearPersonaRequest, CrearPersonaResponse>
{
    private readonly IPersonaRepository _personaRepository;
    private readonly ICatalogoService _catalogoService;

    public CrearPersonaHandler(
        IPersonaRepository personaRepository,
        ICatalogoService catalogoService)
    {
        ArgumentNullException.ThrowIfNull(personaRepository);
        ArgumentNullException.ThrowIfNull(catalogoService);

        _personaRepository = personaRepository;
        _catalogoService = catalogoService;
    }

    public async Task<OperationResult<CrearPersonaResponse>> ExecuteAsync(
        CrearPersonaRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        // -----------------------------------------------------------------
        // Validaciones
        // -----------------------------------------------------------------

        if (string.IsNullOrWhiteSpace(request.NumeroDocumento))
        {
            return OperationResult<CrearPersonaResponse>.Failure(
                PersonaMessages.NumeroDocumentoObligatorio);
        }

        if (await _personaRepository.ExistsNumeroDocumentoAsync(
                request.NumeroDocumento,
                cancellationToken))
        {
            return OperationResult<CrearPersonaResponse>.Failure(
                PersonaMessages.NumeroDocumentoDuplicado);
        }

        // -----------------------------------------------------------------
        // Obtener catálogos
        // -----------------------------------------------------------------

        var tipoPersona = await _catalogoService.ObtenerItemAsync(
            CatalogosCore.TipoPersona,
            request.TipoPersonaCodigo,
            cancellationToken);

        if (tipoPersona is null)
        {
            return OperationResult<CrearPersonaResponse>.Failure(
                PersonaMessages.TipoPersonaNoConfigurado);
        }

        var tipoDocumento = await _catalogoService.ObtenerItemAsync(
            CatalogosCore.TipoDocumento,
            request.TipoDocumentoCodigo,
            cancellationToken);

        if (tipoDocumento is null)
        {
            return OperationResult<CrearPersonaResponse>.Failure(
                PersonaMessages.TipoDocumentoNoConfigurado);
        }

        CatalogoItem? genero = null;

        if (!string.IsNullOrWhiteSpace(request.GeneroCodigo))
        {
            genero = await _catalogoService.ObtenerItemAsync(
                CatalogosCore.Genero,
                request.GeneroCodigo,
                cancellationToken);

            if (genero is null)
            {
                return OperationResult<CrearPersonaResponse>.Failure(
                    PersonaMessages.GeneroNoConfigurado);
            }
        }

        CatalogoItem? estadoCivil = null;

        if (!string.IsNullOrWhiteSpace(request.EstadoCivilCodigo))
        {
            estadoCivil = await _catalogoService.ObtenerItemAsync(
                CatalogosCore.EstadoCivil,
                request.EstadoCivilCodigo,
                cancellationToken);

            if (estadoCivil is null)
            {
                return OperationResult<CrearPersonaResponse>.Failure(
                    PersonaMessages.EstadoCivilNoConfigurado);
            }
        }

        // -----------------------------------------------------------------
        // Crear entidad
        // -----------------------------------------------------------------

        var persona = Persona.Create(
            tipoPersona.Id,
            tipoDocumento.Id,
            request.NumeroDocumento);

        // -----------------------------------------------------------------
        // Información General
        // -----------------------------------------------------------------

        persona.CambiarComplemento(request.Complemento);

        persona.CambiarNit(request.Nit);

        persona.CambiarObservacion(request.Observacion);

        // -----------------------------------------------------------------
        // Persona Natural / Jurídica
        // -----------------------------------------------------------------

        if (tipoPersona.Codigo == TipoPersonaCodigos.Natural)
        {
            persona.CambiarDatosNaturales(
                request.Nombres!,
                request.PrimerApellido!,
                request.SegundoApellido,
                request.FechaNacimiento,
                genero?.Id,
                estadoCivil?.Id);
        }
        else if (tipoPersona.Codigo == TipoPersonaCodigos.Juridica)
        {
            persona.CambiarDatosJuridicos(
                request.RazonSocial!,
                request.NombreComercial,
                request.Sigla);
        }

        // -----------------------------------------------------------------
        // Persistencia
        // -----------------------------------------------------------------

        await _personaRepository.AddAsync(
            persona,
            cancellationToken);

        await _personaRepository.GuardarCambiosAsync(
            cancellationToken);

        // -----------------------------------------------------------------
        // Construcción de la respuesta
        // -----------------------------------------------------------------

        var response = new CrearPersonaResponse
        {
            PersonaId = persona.Id,
            NumeroDocumento = persona.NumeroDocumento,
            Nombres = persona.Nombres,
            PrimerApellido = persona.PrimerApellido,
            SegundoApellido = persona.SegundoApellido,
            RazonSocial = persona.RazonSocial
        };

        return OperationResult<CrearPersonaResponse>.SuccessResult(response);
    }
}