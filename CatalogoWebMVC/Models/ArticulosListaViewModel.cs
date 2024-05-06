using CatalogoWebMVC.Data;

namespace CatalogoWebMVC.Models
{
    public class ArticulosListaViewModel
    {
        public List<ArticuloViewModel> ListaArticulos { get; set; }
        public FiltroViewModel FiltroModelo { get; set; }

        public List<Articulo>   ListadoArticulos { get; set; }
    }
}
