namespace JLSC.Framework.Application.Common.Results;

public sealed class PagedResult<T>
{
    public IReadOnlyCollection<T> Items { get; init; } = [];

    public int TotalRegistros { get; init; }

    public int Pagina { get; init; }

    public int TamanoPagina { get; init; }

    public int TotalPaginas =>
        TamanoPagina == 0
            ? 0
            : (int)Math.Ceiling((double)TotalRegistros / TamanoPagina);

    public bool TienePaginaAnterior => Pagina > 1;

    public bool TienePaginaSiguiente => Pagina < TotalPaginas;
}