using JLSC.Framework.Application.Core.Portal.Commands.ActivarProyectoDestacado;
using JLSC.Framework.Application.Core.Portal.Commands.ActualizarProyectoDestacado;
using JLSC.Framework.Application.Core.Portal.Commands.CrearProyectoDestacado;
using JLSC.Framework.Application.Core.Portal.Commands.DesactivarProyectoDestacado;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerProyectoDestacado;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerProyectosDestacados;
using Microsoft.AspNetCore.Mvc;

namespace JLSC.Framework.Web.Controllers.Portal;

[ApiController]
[Route("api/portal/proyectos-destacados")]
public sealed class ProyectoDestacadoController : ControllerBase
{
    private readonly CrearProyectoDestacadoHandler _crearHandler;
    private readonly ActualizarProyectoDestacadoHandler _actualizarHandler;
    private readonly ActivarProyectoDestacadoHandler _activarHandler;
    private readonly DesactivarProyectoDestacadoHandler _desactivarHandler;
    private readonly ObtenerProyectoDestacadoHandler _obtenerHandler;
    private readonly ObtenerProyectosDestacadosHandler _obtenerTodosHandler;


    public ProyectoDestacadoController(
        CrearProyectoDestacadoHandler crearHandler,
        ActualizarProyectoDestacadoHandler actualizarHandler,
        ActivarProyectoDestacadoHandler activarHandler,
        DesactivarProyectoDestacadoHandler desactivarHandler,
        ObtenerProyectoDestacadoHandler obtenerHandler,
        ObtenerProyectosDestacadosHandler obtenerTodosHandler)
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
        var request = new ObtenerProyectosDestacadosRequest
        {
            IncluirInactivos = incluirInactivos
        };

        var resultado = await _obtenerTodosHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpGet("{proyectoDestacadoId:long}")]
    public async Task<IActionResult> Obtener(
        long proyectoDestacadoId,
        CancellationToken cancellationToken = default)
    {
        var request = new ObtenerProyectoDestacadoRequest
        {
            ProyectoDestacadoId = proyectoDestacadoId
        };

        var resultado = await _obtenerHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearProyectoDestacadoRequest request,
        CancellationToken cancellationToken = default)
    {
        var resultado = await _crearHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPut("{proyectoDestacadoId:long}")]
    public async Task<IActionResult> Actualizar(
        long proyectoDestacadoId,
        [FromBody] ActualizarProyectoDestacadoRequest request,
        CancellationToken cancellationToken = default)
    {
        request.ProyectoDestacadoId = proyectoDestacadoId;

        var resultado = await _actualizarHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPatch("{proyectoDestacadoId:long}/activar")]
    public async Task<IActionResult> Activar(
        long proyectoDestacadoId,
        CancellationToken cancellationToken = default)
    {
        var request = new ActivarProyectoDestacadoRequest
        {
            ProyectoDestacadoId = proyectoDestacadoId
        };

        var resultado = await _activarHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    [HttpPatch("{proyectoDestacadoId:long}/desactivar")]
    public async Task<IActionResult> Desactivar(
        long proyectoDestacadoId,
        CancellationToken cancellationToken = default)
    {
        var request = new DesactivarProyectoDestacadoRequest
        {
            ProyectoDestacadoId = proyectoDestacadoId
        };

        var resultado = await _desactivarHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }
}