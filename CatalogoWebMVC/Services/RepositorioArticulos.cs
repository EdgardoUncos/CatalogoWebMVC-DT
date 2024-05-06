using CatalogoWebMVC.Data;
using CatalogoWebMVC.Models;

namespace CatalogoWebMVC.Services
{
    public interface IRepositorioArticulos
    {
        IEnumerable<Articulo> ObtenerArticulos();
        IEnumerable<Articulo> ObtenerPaginaArticulo(PaginacionViewModel paginaViewModel);

        void AgregarArticulo(Articulo articulo);
        Articulo ObtenerArticulo(int idArticulo);
        void ModificarArticulo(Articulo articulo);
        void BorrarArticulo(Articulo articulo);
        List<Articulo> BuscarArticulo(string buscar);
        List<Articulo> BusquedaAvanzada(FiltroViewModel filtro);
    }

    public class RepositorioArticulos: IRepositorioArticulos
    {
        private CATALOGO_WEB_DBContext _context;
        public RepositorioArticulos(CATALOGO_WEB_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Articulo> ObtenerArticulos()
        {
            return _context.Articulos.ToList();
        }

        public IEnumerable<Articulo> ObtenerPaginaArticulo(PaginacionViewModel paginaViewModel)
        {
            var paginaMostrar = (from a in _context.Articulos select a).Skip(paginaViewModel.RegistrosASaltar).Take(paginaViewModel.RegistrosPorPagina).ToList();
            paginaViewModel.CantidadTotalRegistros = _context.Articulos.Count();

            return paginaMostrar;
        }

        public void AgregarArticulo(Articulo articulo)
        {
            _context.Add(articulo);
            _context.SaveChanges();
        }

        public Articulo ObtenerArticulo(int idArticulo)
        {
            return (from a in _context.Articulos where a.Id == idArticulo select a).FirstOrDefault();
        }

        public void ModificarArticulo(Articulo articulo)
        {
            _context.Update(articulo);
            _context.SaveChanges();
        }

        public void BorrarArticulo(Articulo articulo)
        {
            if (articulo == null)
                return;

            _context.Remove(articulo);
            _context.SaveChanges();
        }

        public List<Articulo> BuscarArticulo(string buscar)
        {
            List<Articulo> listaArticulo = (from a in _context.Articulos select a).ToList();
            List<Articulo> listaFiltrada;

            if (!string.IsNullOrEmpty(buscar) && buscar.Length > 3)
                listaFiltrada = listaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(buscar.ToUpper()));
            else
                listaFiltrada = listaArticulo;

            return listaFiltrada;
        }

        public List<Articulo> BusquedaAvanzada(FiltroViewModel filtro)
        {
            var lista = _context.Articulos.ToList();
            List<Articulo> listaFiltrada = lista;

            if ( filtro.TextoCampo == Campo.Nombre.ToString())
            {
                switch (filtro.TextoCriterio)
                {
                    case "Comienza con":
                                            listaFiltrada = (from a in _context.Articulos where a.Nombre.StartsWith(filtro.Texto) select a).ToList();
                                            break;
                                        
                    case "Termina con":
                                            listaFiltrada = (from a in _context.Articulos where a.Nombre.EndsWith(filtro.Texto) select a).ToList();
                                            
                        
                        
                        break;

                    case "Igual a":         
                                            listaFiltrada = (from a in _context.Articulos where a.Nombre.Equals(filtro.Texto) select a).ToList();
                                            break;
                }
            }
            else if ( filtro.TextoCampo == Campo.Descripcion.ToString())
            {
                switch ( filtro.TextoCriterio )
                {
                    case "Comienza con":
                        listaFiltrada = (from a in _context.Articulos where a.Descripcion.StartsWith(filtro.Texto) select a).ToList();
                        break;

                    case "Termina con":
                        listaFiltrada = (from a in _context.Articulos where a.Descripcion.EndsWith(filtro.Texto) select a).ToList();
                        break;

                    case "Igual a":
                        listaFiltrada = (from a in _context.Articulos where a.Descripcion.Equals(filtro.Texto) select a).ToList();
                        break;
                }
            }
            else
            {
                switch ( filtro.TextoCriterio )
                {
                    case "Mayor a":
                        listaFiltrada = (from a in _context.Articulos where a.Precio > Decimal.Parse(filtro.Texto) select a).ToList();
                        break;

                    case "Menor a":
                        listaFiltrada = (from a in _context.Articulos where a.Precio < Decimal.Parse(filtro.Texto) select a).ToList();
                        break;

                    case "Igual a":
                        listaFiltrada = (from a in _context.Articulos where a.Precio == Decimal.Parse(filtro.Texto) select a).ToList();
                        break;
                }
            }

            return listaFiltrada;
        }

        public string ObtenerMarca(int IdMarca)
        {
            string marca = (from a in _context.Articulos
                            join m in _context.Marcas
                            on a.IdMarca equals m.Id
                            select m.Descripcion).FirstOrDefault();

            return marca;
        }

        public string ObtenerCategoria(int IdCategoria)
        {
            string categoria = (from a in _context.Articulos
                            join m in _context.Categorias
                            on a.IdCategoria equals m.Id
                            select m.Descripcion).FirstOrDefault();

            return categoria;
        }




    }
}
