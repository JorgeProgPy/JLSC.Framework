using JLSC.Framework.Application.Core.Portal.Commands.ActualizarConfiguracionInstitucional;
using JLSC.Framework.Application.Core.Portal.Commands.CrearConfiguracionInstitucional;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerConfiguracionInstitucional;
using Microsoft.AspNetCore.Mvc;

namespace JLSC.Framework.Web.Controllers.Portal;

[ApiController]
[Route("api/portal/configuracion-institucional")]
public sealed class ConfiguracionInstitucionalController : ControllerBase
{
    private readonly CrearConfiguracionInstitucionalHandler _crearHandler;
    private readonly ActualizarConfiguracionInstitucionalHandler _actualizarHandler;
    private readonly ObtenerConfiguracionInstitucionalHandler _obtenerHandler;

    public ConfiguracionInstitucionalController(
        CrearConfiguracionInstitucionalHandler crearHandler,
        ActualizarConfiguracionInstitucionalHandler actualizarHandler,
        ObtenerConfiguracionInstitucionalHandler obtenerHandler)
    {
        _crearHandler = crearHandler;
        _actualizarHandler = actualizarHandler;
        _obtenerHandler = obtenerHandler;
    }

    [HttpGet]
    public async Task<IActionResult> Obtener(
        CancellationToken cancellationToken = default)
    {
        var request = new ObtenerConfiguracionInstitucionalRequest();

        var resultado = await _obtenerHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearConfiguracionInstitucionalRequest request,
        CancellationToken cancellationToken = default)
    {
        var resultado = await _crearHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPut("{configuracionInstitucionalId:long}")]
    public async Task<IActionResult> Actualizar(
    long configuracionInstitucionalId,
    [FromBody] ActualizarConfiguracionInstitucionalRequest request,
    CancellationToken cancellationToken = default)
    {
        var requestActualizacion = new ActualizarConfiguracionInstitucionalRequest
        {
            ConfiguracionInstitucionalId = configuracionInstitucionalId,
            NombreInstitucion = request.NombreInstitucion,
            NombreCorto = request.NombreCorto,
            Descripcion = request.Descripcion,
            Eslogan = request.Eslogan,
            LogoPrincipalArchivoId = request.LogoPrincipalArchivoId,
            LogoSecundarioArchivoId = request.LogoSecundarioArchivoId,
            FaviconArchivoId = request.FaviconArchivoId,
            Telefono = request.Telefono,
            Correo = request.Correo,
            Direccion = request.Direccion
        };

        var resultado = await _actualizarHandler.ExecuteAsync(
            requestActualizacion,
            cancellationToken);

        return Ok(resultado);
    }
}