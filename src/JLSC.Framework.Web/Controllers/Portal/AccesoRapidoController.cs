using JLSC.Framework.Application.Core.Portal.Commands.ActivarAccesoRapido;
using JLSC.Framework.Application.Core.Portal.Commands.ActualizarAccesoRapido;
using JLSC.Framework.Application.Core.Portal.Commands.CrearAccesoRapido;
using JLSC.Framework.Application.Core.Portal.Commands.DesactivarAccesoRapido;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerAccesoRapido;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerAccesosRapidos;
using Microsoft.AspNetCore.Mvc;

namespace JLSC.Framework.Web.Controllers.Portal;

[ApiController]
[Route("api/portal/accesos-rapidos")]
public sealed class AccesoRapidoController : ControllerBase
{
    private readonly CrearAccesoRapidoHandler _crearHandler;
    private readonly ActualizarAccesoRapidoHandler _actualizarHandler;
    private readonly ActivarAccesoRapidoHandler _activarHandler;
    private readonly DesactivarAccesoRapidoHandler _desactivarHandler;
    private readonly ObtenerAccesoRapidoHandler _obtenerHandler;
    private readonly ObtenerAccesosRapidosHandler _obtenerTodosHandler;

    public AccesoRapidoController(
        CrearAccesoRapidoHandler crearHandler,
        ActualizarAccesoRapidoHandler actualizarHandler,
        ActivarAccesoRapidoHandler activarHandler,
        DesactivarAccesoRapidoHandler desactivarHandler,
        ObtenerAccesoRapidoHandler obtenerHandler,
        ObtenerAccesosRapidosHandler obtenerTodosHandler)
    {
        _crearHandler = crearHandler;
        _actualizarHandler = actualizarHandler;
        _activarHandler = activarHandler;
        _desactivarHandler = desactivarHandler;
        _obtenerHandler = obtenerHandler;
        _obtenerTodosHandler = obtenerTodosHandler;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos(
        [FromQuery] bool incluirInactivos = false,
        CancellationToken cancellationToken = default)
    {
        var request = new ObtenerAccesosRapidosRequest
        {
            IncluirInactivos = incluirInactivos
        };

        var resultado = await _obtenerTodosHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpGet("{accesoRapidoId:long}")]
    public async Task<IActionResult> Obtener(
        long accesoRapidoId,
        CancellationToken cancellationToken = default)
    {
        var request = new ObtenerAccesoRapidoRequest
        {
            AccesoRapidoId = accesoRapidoId
        };

        var resultado = await _obtenerHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearAccesoRapidoRequest request,
        CancellationToken cancellationToken = default)
    {
        var resultado = await _crearHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPut("{accesoRapidoId:long}")]
    public async Task<IActionResult> Actualizar(
        long accesoRapidoId,
        [FromBody] ActualizarAccesoRapidoRequest request,
        CancellationToken cancellationToken = default)
    {
        request.AccesoRapidoId = accesoRapidoId;

        var resultado = await _actualizarHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPatch("{accesoRapidoId:long}/activar")]
    public async Task<IActionResult> Activar(
        long accesoRapidoId,
        CancellationToken cancellationToken = default)
    {
        var request = new ActivarAccesoRapidoRequest
        {
            AccesoRapidoId = accesoRapidoId
        };

        var resultado = await _activarHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPatch("{accesoRapidoId:long}/desactivar")]
    public async Task<IActionResult> Desactivar(
        long accesoRapidoId,
        CancellationToken cancellationToken = default)
    {
        var request = new DesactivarAccesoRapidoRequest
        {
            AccesoRapidoId = accesoRapidoId
        };

        var resultado = await _desactivarHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }
}