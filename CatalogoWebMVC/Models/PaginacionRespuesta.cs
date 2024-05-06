namespace CatalogoWebMVC.Models
{
    public class PaginacionRespuesta
    {
        public int Pagina { get; set; } = 1;

        public int RegistrosPorPagina { get; set; } = 10;

        public int CantidadTotalRegistros { get; set; }

        public int CantidadTotalPaginas => (int)Math.Ceiling((double)CantidadTotalRegistros / RegistrosPorPagina);

        public string BaseUrl { get; set; }

    }

    public class PaginacionRespuesta<T> : PaginacionRespuesta
    {
        public IEnumerable<T> Elementos { get; set;}
    }
}
