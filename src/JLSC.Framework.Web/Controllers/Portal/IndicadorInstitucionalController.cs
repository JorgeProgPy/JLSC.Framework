using JLSC.Framework.Application.Core.Portal.Commands.ActivarIndicadorInstitucional;
using JLSC.Framework.Application.Core.Portal.Commands.ActualizarIndicadorInstitucional;
using JLSC.Framework.Application.Core.Portal.Commands.CrearIndicadorInstitucional;
using JLSC.Framework.Application.Core.Portal.Commands.DesactivarIndicadorInstitucional;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerIndicadorInstitucional;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerIndicadoresInstitucionales;
using Microsoft.AspNetCore.Mvc;

namespace JLSC.Framework.Web.Controllers.Portal;

[ApiController]
[Route("api/portal/indicadores-institucionales")]
public sealed class IndicadorInstitucionalController : ControllerBase
{
    private readonly CrearIndicadorInstitucionalHandler _crearHandler;
    private readonly ActualizarIndicadorInstitucionalHandler _actualizarHandler;
    private readonly ActivarIndicadorInstitucionalHandler _activarHandler;
    private readonly DesactivarIndicadorInstitucionalHandler _desactivarHandler;
    private readonly ObtenerIndicadorInstitucionalHandler _obtenerHandler;
    private readonly ObtenerIndicadoresInstitucionalesHandler _obtenerTodosHandler;

    public IndicadorInstitucionalController(
        CrearIndicadorInstitucionalHandler crearHandler,
        ActualizarIndicadorInstitucionalHandler actualizarHandler,
        ActivarIndicadorInstitucionalHandler activarHandler,
        DesactivarIndicadorInstitucionalHandler desactivarHandler,
        ObtenerIndicadorInstitucionalHandler obtenerHandler,
        ObtenerIndicadoresInstitucionalesHandler obtenerTodosHandler)
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
        var request = new ObtenerIndicadoresInstitucionalesRequest
        {
            IncluirInactivos = incluirInactivos
        };

        var resultado = await _obtenerTodosHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpGet("{indicadorId:long}")]
    public async Task<IActionResult> Obtener(
        long indicadorId,
        CancellationToken cancellationToken = default)
    {
        var request = new ObtenerIndicadorInstitucionalRequest
        {
            IndicadorId = indicadorId
        };

        var resultado = await _obtenerHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearIndicadorInstitucionalRequest request,
        CancellationToken cancellationToken = default)
    {
        var resultado = await _crearHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPut("{indicadorId:long}")]
    public async Task<IActionResult> Actualizar(
        long indicadorId,
        [FromBody] ActualizarIndicadorInstitucionalRequest request,
        CancellationToken cancellationToken = default)
    {
        request.IndicadorId = indicadorId;

        var resultado = await _actualizarHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPatch("{indicadorId:long}/activar")]
    public async Task<IActionResult> Activar(
        long indicadorId,
        CancellationToken cancellationToken = default)
    {
        var request = new ActivarIndicadorInstitucionalRequest
        {
            IndicadorId = indicadorId
        };

        var resultado = await _activarHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPatch("{indicadorId:long}/desactivar")]
    public async Task<IActionResult> Desactivar(
        long indicadorId,
        CancellationToken cancellationToken = default)
    {
        var request = new DesactivarIndicadorInstitucionalRequest
        {
            IndicadorId = indicadorId
        };

        var resultado = await _desactivarHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }
}