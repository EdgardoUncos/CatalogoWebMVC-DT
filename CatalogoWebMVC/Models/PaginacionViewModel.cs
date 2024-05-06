namespace CatalogoWebMVC.Models
{
    public class PaginacionViewModel
    {
        public int Pagina { get; set; } = 1;

        private int registrosPorPagina = 5;

        private readonly int cantidadMaximaRegitrosPorPagina = 50;

        public int CantidadTotalRegistros { get; set; } // segun el tipo de consulta


        public int RegistrosPorPagina
        {
            get { return registrosPorPagina; }
            set { registrosPorPagina = value > 50 ? cantidadMaximaRegitrosPorPagina : value; }
        }

        public int RegistrosASaltar => registrosPorPagina * (Pagina - 1);

    }
}
