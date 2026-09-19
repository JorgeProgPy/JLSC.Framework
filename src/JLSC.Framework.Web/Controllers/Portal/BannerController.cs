using JLSC.Framework.Application.Core.Portal.Commands.CrearBanner;
using JLSC.Framework.Application.Core.Portal.Commands.ActualizarBanner;
using JLSC.Framework.Application.Core.Portal.Commands.ActivarBanner;
using JLSC.Framework.Application.Core.Portal.Commands.DesactivarBanner;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerBanner;
using JLSC.Framework.Application.Core.Portal.Queries.ObtenerBanners;
using Microsoft.AspNetCore.Mvc;

namespace JLSC.Framework.Web.Controllers.Portal;

[ApiController]
[Route("api/portal/banners")]
public sealed class BannerController : ControllerBase
{
    private readonly CrearBannerHandler _crearBannerHandler;
    private readonly ActualizarBannerHandler _actualizarBannerHandler;

    private readonly ActivarBannerHandler _activarBannerHandler;
    private readonly DesactivarBannerHandler _desactivarBannerHandler;
    private readonly ObtenerBannerHandler _obtenerBannerHandler;
    private readonly ObtenerBannersHandler _obtenerBannersHandler;

    public BannerController(
    CrearBannerHandler crearBannerHandler,
    ActualizarBannerHandler actualizarBannerHandler,
    ActivarBannerHandler activarBannerHandler,
    DesactivarBannerHandler desactivarBannerHandler,
    ObtenerBannerHandler obtenerBannerHandler,
    ObtenerBannersHandler obtenerBannersHandler)
    {
        ArgumentNullException.ThrowIfNull(crearBannerHandler);
        ArgumentNullException.ThrowIfNull(actualizarBannerHandler);
        ArgumentNullException.ThrowIfNull(activarBannerHandler);
        ArgumentNullException.ThrowIfNull(desactivarBannerHandler);
        ArgumentNullException.ThrowIfNull(obtenerBannerHandler);
        ArgumentNullException.ThrowIfNull(obtenerBannersHandler);

        _crearBannerHandler = crearBannerHandler;
        _actualizarBannerHandler = actualizarBannerHandler;
        _activarBannerHandler = activarBannerHandler;
        _desactivarBannerHandler = desactivarBannerHandler;
        _obtenerBannerHandler = obtenerBannerHandler;
        _obtenerBannersHandler = obtenerBannersHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CrearBanner(
        [FromBody] CrearBannerRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _crearBannerHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPut("{bannerId:long}")]
    public async Task<IActionResult> ActualizarBanner(
    long bannerId,
    [FromBody] ActualizarBannerRequest request,
    CancellationToken cancellationToken = default)
    {
        var requestConId = new ActualizarBannerRequest
        {
            BannerId = bannerId,
            Titulo = request.Titulo,
            Subtitulo = request.Subtitulo,
            ArchivoId = request.ArchivoId,
            Enlace = request.Enlace,
            Orden = request.Orden,
            FechaInicio = request.FechaInicio,
            FechaFin = request.FechaFin
        };

        var result = await _actualizarBannerHandler.ExecuteAsync(
            requestConId,
            cancellationToken);

        return Ok(result);
    }


    [HttpPatch("{bannerId:long}/activar")]
    public async Task<IActionResult> ActivarBanner(
    long bannerId,
    CancellationToken cancellationToken = default)
    {
        var request = new ActivarBannerRequest
        {
            BannerId = bannerId
        };

        var result = await _activarBannerHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{bannerId:long}/desactivar")]
    public async Task<IActionResult> DesactivarBanner(
        long bannerId,
        CancellationToken cancellationToken = default)
    {
        var request = new DesactivarBannerRequest
        {
            BannerId = bannerId
        };

        var result = await _desactivarBannerHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{bannerId:long}")]
    public async Task<IActionResult> ObtenerBanner(
    long bannerId,
    CancellationToken cancellationToken = default)
    {
        var request = new ObtenerBannerRequest
        {
            BannerId = bannerId
        };

        var result = await _obtenerBannerHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerBanners(
    [FromQuery] bool incluirInactivos = false,
    CancellationToken cancellationToken = default)
    {
        var request = new ObtenerBannersRequest
        {
            IncluirInactivos = incluirInactivos
        };

        var result = await _obtenerBannersHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }
}