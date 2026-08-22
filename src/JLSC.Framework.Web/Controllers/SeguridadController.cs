using JLSC.Framework.Application.Core.Seguridad.Commands.CrearModulo;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerModulos;
using Microsoft.AspNetCore.Mvc;

namespace JLSC.Framework.Web.Controllers;

[ApiController]
[Route("api/seguridad")]
public sealed class SeguridadController : ControllerBase
{
    private readonly ObtenerModulosHandler _obtenerModulosHandler;
    private readonly CrearModuloHandler _crearModuloHandler;

    public SeguridadController(
        ObtenerModulosHandler obtenerModulosHandler,
        CrearModuloHandler crearModuloHandler)
    {
        ArgumentNullException.ThrowIfNull(obtenerModulosHandler);
        ArgumentNullException.ThrowIfNull(crearModuloHandler);

        _obtenerModulosHandler = obtenerModulosHandler;
        _crearModuloHandler = crearModuloHandler;
    }

    [HttpGet("modulos")]
    public async Task<IActionResult> ObtenerModulos(
        [FromQuery] bool incluirInactivos = false,
        CancellationToken cancellationToken = default)
    {
        var request = new ObtenerModulosRequest
        {
            IncluirInactivos = incluirInactivos
        };

        var result = await _obtenerModulosHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("modulos")]
    public async Task<IActionResult> CrearModulo(
        [FromBody] CrearModuloRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _crearModuloHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }
}