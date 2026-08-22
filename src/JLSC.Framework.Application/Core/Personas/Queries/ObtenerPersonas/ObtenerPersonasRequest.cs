namespace JLSC.Framework.Application.Core.Personas.Queries.ObtenerPersonas;

public sealed class ObtenerPersonasRequest
{
    //====================================
    /// Número de página a consultar.
    //====================================
    public int Pagina { get; init; } = 1;

    //====================================
    // Cantidad de registros por página.
   //====================================
    public int TamanoPagina { get; init; } = 20;

    //====================================
    // Texto para búsqueda.
    //====================================
    public string? Buscar { get; init; }

    //====================================
    // Filtra personas activas/inactivas.
    // Null = todas.
    //====================================

    public bool? Activo { get; init; }
}