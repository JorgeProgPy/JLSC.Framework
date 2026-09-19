using JLSC.Framework.Application.Core.Seguridad.Comands.ActualizarRol;
using JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarModulo;
using JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarPermiso;
using JLSC.Framework.Application.Core.Seguridad.Commands.ActualizarRol;
using JLSC.Framework.Application.Core.Seguridad.Commands.AsignarPermisoRol;
using JLSC.Framework.Application.Core.Seguridad.Commands.CrearModulo;
using JLSC.Framework.Application.Core.Seguridad.Commands.CrearPermiso;
using JLSC.Framework.Application.Core.Seguridad.Commands.CrearRol;
using JLSC.Framework.Application.Core.Seguridad.Commands.EliminarModulo;
using JLSC.Framework.Application.Core.Seguridad.Commands.EliminarPermiso;
using JLSC.Framework.Application.Core.Seguridad.Commands.EliminarRol;
using JLSC.Framework.Application.Core.Seguridad.Commands.QuitarPermisoRol;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerModuloPorId;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerModulos;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerPermisos;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerPermisosRol;
using JLSC.Framework.Application.Core.Seguridad.Queries.ObtenerRoles;
using Microsoft.AspNetCore.Mvc;

namespace JLSC.Framework.Web.Controllers;

[ApiController]
[Route("api/seguridad")]
public sealed class SeguridadController : ControllerBase
{
    private readonly ObtenerModulosHandler _obtenerModulosHandler;
    private readonly CrearModuloHandler _crearModuloHandler;
    private readonly ActualizarModuloHandler _actualizarModuloHandler;
    private readonly ObtenerModuloPorIdHandler _obtenerModuloPorIdHandler;
    private readonly EliminarModuloHandler _eliminarModuloHandler;
    private readonly CrearRolHandler _crearRolHandler;
    private readonly ObtenerRolesHandler _obtenerRolesHandler;
    private readonly ActualizarRolHandler _actualizarRolHandler;
    private readonly EliminarRolHandler _eliminarRolHandler;
    private readonly CrearPermisoHandler _crearPermisoHandler;
    private readonly ObtenerPermisosHandler _obtenerPermisosHandler;
    private readonly ActualizarPermisoHandler _actualizarPermisoHandler;
    private readonly EliminarPermisoHandler _eliminarPermisoHandler;
    private readonly AsignarPermisoRolHandler _asignarPermisoRolHandler;
    private readonly ObtenerPermisosRolHandler _obtenerPermisosRolHandler;
    private readonly QuitarPermisoRolHandler _quitarPermisoRolHandler;
    
    public SeguridadController(
    ObtenerModulosHandler obtenerModulosHandler,
    CrearModuloHandler crearModuloHandler,
    ObtenerModuloPorIdHandler obtenerModuloPorIdHandler,
    ActualizarModuloHandler actualizarModuloHandler,
    EliminarModuloHandler eliminarModuloHandler,
    CrearRolHandler crearRolHandler,
    ObtenerRolesHandler obtenerRolesHandler,
    ActualizarRolHandler actualizarRolHandler,
    EliminarRolHandler eliminarRolHandler,
    CrearPermisoHandler crearPermisoHandler,
    ObtenerPermisosHandler obtenerPermisosHandler,
    ActualizarPermisoHandler actualizarPermisoHandler,
    EliminarPermisoHandler eliminarPermisoHandler,
    AsignarPermisoRolHandler asignarPermisoRolHandler,
    ObtenerPermisosRolHandler obtenerPermisosRolHandler,
    QuitarPermisoRolHandler quitarPermisoRolHandler)

    {
        ArgumentNullException.ThrowIfNull(obtenerModulosHandler);
        ArgumentNullException.ThrowIfNull(crearModuloHandler);
        ArgumentNullException.ThrowIfNull(obtenerModuloPorIdHandler);
        ArgumentNullException.ThrowIfNull(actualizarModuloHandler);
        ArgumentNullException.ThrowIfNull(eliminarModuloHandler);
        ArgumentNullException.ThrowIfNull(crearRolHandler);
        ArgumentNullException.ThrowIfNull(obtenerRolesHandler);
        ArgumentNullException.ThrowIfNull(actualizarRolHandler);
        ArgumentNullException.ThrowIfNull(eliminarRolHandler);
        ArgumentNullException.ThrowIfNull(crearPermisoHandler);
        ArgumentNullException.ThrowIfNull(obtenerPermisosHandler);
        ArgumentNullException.ThrowIfNull(actualizarPermisoHandler);
        ArgumentNullException.ThrowIfNull(eliminarPermisoHandler);
        ArgumentNullException.ThrowIfNull(asignarPermisoRolHandler);
        ArgumentNullException.ThrowIfNull(obtenerPermisosRolHandler);
        ArgumentNullException.ThrowIfNull(quitarPermisoRolHandler);

        _obtenerModulosHandler = obtenerModulosHandler;
        _crearModuloHandler = crearModuloHandler;
        _obtenerModuloPorIdHandler = obtenerModuloPorIdHandler;
        _actualizarModuloHandler = actualizarModuloHandler;
        _eliminarModuloHandler = eliminarModuloHandler;
        _crearRolHandler = crearRolHandler;
        _obtenerRolesHandler = obtenerRolesHandler;
        _actualizarRolHandler = actualizarRolHandler;
        _eliminarRolHandler = eliminarRolHandler;
        _crearPermisoHandler = crearPermisoHandler;
        _obtenerPermisosHandler = obtenerPermisosHandler;
        _actualizarPermisoHandler = actualizarPermisoHandler;
        _eliminarPermisoHandler = eliminarPermisoHandler;
        _asignarPermisoRolHandler = asignarPermisoRolHandler;
        _obtenerPermisosRolHandler = obtenerPermisosRolHandler;
        _quitarPermisoRolHandler = quitarPermisoRolHandler;
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

    [HttpGet("modulos/{id:long}")]
    public async Task<IActionResult> ObtenerModuloPorId(
        long id,
        CancellationToken cancellationToken = default)
    {
        var request = new ObtenerModuloPorIdRequest
        {
            Id = id
        };

        var result = await _obtenerModuloPorIdHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPut("modulos/{id:long}")]
    public async Task<IActionResult> ActualizarModulo(
        long id,
        [FromBody] ActualizarModuloRequest request,
        CancellationToken cancellationToken = default)
    {
        var actualizarRequest = new ActualizarModuloRequest
        {
            Id = id,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Icono = request.Icono,
            Color = request.Color,
            Orden = request.Orden,
            Visible = request.Visible,
            Activo = request.Activo
        };

        var result = await _actualizarModuloHandler.ExecuteAsync(
            actualizarRequest,
            cancellationToken);

        return Ok(result);
    }

    [HttpDelete("modulos/{id:long}")]
    public async Task<IActionResult> EliminarModulo(
        long id,
        CancellationToken cancellationToken = default)
    {
        var request = new EliminarModuloRequest
        {
            Id = id
        };

        var result = await _eliminarModuloHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("roles")]
    public async Task<IActionResult> CrearRol(
        [FromBody] CrearRolRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _crearRolHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);


    }

    [HttpGet("roles")]
    public async Task<IActionResult> ObtenerRoles(
    [FromQuery] bool incluirInactivos = false,
    CancellationToken cancellationToken = default)
    {
        var request = new ObtenerRolesRequest
        {
            IncluirInactivos = incluirInactivos
        };

        var result = await _obtenerRolesHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPut("roles/{id:long}")]
    public async Task<IActionResult> ActualizarRol(
    long id,
    [FromBody] ActualizarRolRequest request,
    CancellationToken cancellationToken = default)
    {
        var actualizarRequest = new ActualizarRolRequest
        {
            Id = id,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Activo = request.Activo
        };

        var result = await _actualizarRolHandler.ExecuteAsync(
            actualizarRequest,
            cancellationToken);

        return Ok(result);
    }

    [HttpDelete("roles/{id:long}")]
    public async Task<IActionResult> EliminarRol(
    long id,
    CancellationToken cancellationToken = default)
    {
        var request = new EliminarRolRequest
        {
            Id = id
        };

        var result = await _eliminarRolHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("permisos")]
    public async Task<IActionResult> ObtenerPermisos(
    [FromQuery] bool incluirInactivos = false,
    CancellationToken cancellationToken = default)
    {
        var request = new ObtenerPermisosRequest
        {
            IncluirInactivos = incluirInactivos
        };

        var result = await _obtenerPermisosHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("permisos")]
    public async Task<IActionResult> CrearPermiso(
    [FromBody] CrearPermisoRequest request,
    CancellationToken cancellationToken = default)
    {
        var result = await _crearPermisoHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPut("permisos/{id:long}")]
    public async Task<IActionResult> ActualizarPermiso(
    long id,
    [FromBody] ActualizarPermisoRequest request,
    CancellationToken cancellationToken = default)
    {
        var actualizarRequest = new ActualizarPermisoRequest
        {
            Id = id,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Activo = request.Activo
        };

        var result = await _actualizarPermisoHandler.ExecuteAsync(
            actualizarRequest,
            cancellationToken);

        return Ok(result);
    }

    [HttpDelete("permisos/{id:long}")]
    public async Task<IActionResult> EliminarPermiso(
    long id,
    CancellationToken cancellationToken = default)
    {
        var request = new EliminarPermisoRequest
        {
            Id = id
        };

        var result = await _eliminarPermisoHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("roles/{rolId:long}/permisos/{permisoId:long}")]
    public async Task<IActionResult> AsignarPermiso(
    long rolId,
    long permisoId,
    CancellationToken cancellationToken = default)
    {
        var request = new AsignarPermisoRolRequest
        {
            RolId = rolId,
            PermisoId = permisoId
        };

        var result = await _asignarPermisoRolHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("roles/{rolId:long}/permisos")]
    public async Task<IActionResult> ObtenerPermisosRol(
    long rolId,
    CancellationToken cancellationToken = default)
    {
        var request = new ObtenerPermisosRolRequest
        {
            RolId = rolId
        };

        var result = await _obtenerPermisosRolHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpDelete("roles/{rolId:long}/permisos/{permisoId:long}")]
    public async Task<IActionResult> QuitarPermiso(
    long rolId,
    long permisoId,
    CancellationToken cancellationToken = default)
    {
        var request = new QuitarPermisoRolRequest
        {
            RolId = rolId,
            PermisoId = permisoId
        };

        var result = await _quitarPermisoRolHandler.ExecuteAsync(
            request,
            cancellationToken);

        return Ok(result);
    }
}