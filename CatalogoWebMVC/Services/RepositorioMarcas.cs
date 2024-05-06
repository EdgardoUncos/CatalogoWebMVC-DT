using CatalogoWebMVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatalogoWebMVC.Services
{
    public interface IRepositorioMarcas
    {
        IEnumerable<Marca> ObtenerMarcas();
        List<SelectListItem> ObtenerItems();
    }
    public class RepositorioMarcas: IRepositorioMarcas
    {

        private CATALOGO_WEB_DBContext db;

        public RepositorioMarcas(CATALOGO_WEB_DBContext db)
        {
            this.db = db;
        }

        public IEnumerable<Marca> ObtenerMarcas()
        {
            return db.Marcas.ToList();
        }

        public List<SelectListItem> ObtenerItems()
        {
            var items = (from m in db.Marcas
                         select new SelectListItem
                         {
                             Value = m.Id.ToString(),
                             Text = m.Descripcion.ToString(),
                         }).ToList();

            return items;
        }
    }
}
