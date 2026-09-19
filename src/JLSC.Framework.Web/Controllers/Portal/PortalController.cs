using JLSC.Framework.Application.Core.Portal.Interfaces;
using JLSC.Framework.Web.ViewModels.Portal;
using Microsoft.AspNetCore.Mvc;

namespace JLSC.Framework.Web.Controllers.Portal;

public sealed class PortalController : Controller
{
    private readonly IBannerRepository _bannerRepository;
    private readonly IIndicadorInstitucionalRepository _indicadorRepository;
    private readonly IAccesoRapidoRepository _accesoRapidoRepository;
    private readonly IProyectoDestacadoRepository _proyectoRepository;

    public PortalController(
        IBannerRepository bannerRepository,
        IIndicadorInstitucionalRepository indicadorRepository,
        IAccesoRapidoRepository accesoRapidoRepository,
        IProyectoDestacadoRepository proyectoRepository)
    {
        ArgumentNullException.ThrowIfNull(bannerRepository);
        ArgumentNullException.ThrowIfNull(indicadorRepository);
        ArgumentNullException.ThrowIfNull(accesoRapidoRepository);
        ArgumentNullException.ThrowIfNull(proyectoRepository);

        _bannerRepository = bannerRepository;
        _indicadorRepository = indicadorRepository;
        _accesoRapidoRepository = accesoRapidoRepository;
        _proyectoRepository = proyectoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken = default)
    {
        var banners = await _bannerRepository.GetAllAsync(
            false,
            cancellationToken);

        var indicadores = await _indicadorRepository.GetAllAsync(
            false,
            cancellationToken);

        var accesosRapidos = await _accesoRapidoRepository.GetAllAsync(
            false,
            cancellationToken);

        var proyectosDestacados = await _proyectoRepository.GetAllAsync(
            false,
            cancellationToken);

        var model = new InicioPortalViewModel
        {
            Banners = banners
                .Select(x => new BannerViewModel
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Subtitulo = x.Subtitulo,
                    ArchivoId = x.ArchivoId,
                    Enlace = x.Enlace,
                    Orden = x.Orden
                })
                .ToList(),

            Indicadores = indicadores
                .Select(x => new IndicadorInstitucionalViewModel
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Valor = x.Valor,
                    Descripcion = x.Descripcion,
                    Icono = x.Icono,
                    Orden = x.Orden
                })
                .ToList(),

            AccesosRapidos = accesosRapidos
                .Select(x => new AccesoRapidoViewModel
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Descripcion = x.Descripcion,
                    Icono = x.Icono,
                    Enlace = x.Enlace,
                    Orden = x.Orden
                })
                .ToList(),

            ProyectosDestacados = proyectosDestacados
                .Select(x => new ProyectoDestacadoViewModel
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Descripcion = x.Descripcion,
                    Ubicacion = x.Ubicacion,
                    ArchivoId = x.ArchivoId,
                    Enlace = x.Enlace,
                    Orden = x.Orden
                })
                .ToList()
        };

        return View(model);
    }
}