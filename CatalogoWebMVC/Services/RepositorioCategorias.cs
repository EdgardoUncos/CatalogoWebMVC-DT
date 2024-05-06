using CatalogoWebMVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatalogoWebMVC.Services
{
    public interface IRepositorioCategorias
    {

        IEnumerable<Categoria> ObtenerCategorias();
        List<SelectListItem> ObtenerItems();
    }
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private CATALOGO_WEB_DBContext db;

        public RepositorioCategorias(CATALOGO_WEB_DBContext db)
        {
            this.db = db;
        }
        public IEnumerable<Categoria> ObtenerCategorias()
        {
            return db.Categorias.ToList();
        }

        public List<SelectListItem> ObtenerItems()
        {
            var items = (from c in db.Categorias
                         select new SelectListItem
                         {
                             Value = c.Id.ToString(),
                             Text = c.Descripcion.ToString(),
                         }).ToList();

            return items;
        }
    }
}
