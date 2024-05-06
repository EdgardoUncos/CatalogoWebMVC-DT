using CatalogoWebMVC.Data;
using Microsoft.AspNetCore.Mvc;
using CatalogoWebMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CatalogoWebMVC.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;
using Microsoft.AspNetCore.Authorization;

namespace CatalogoWebMVC.Controllers
{
    public class ArticuloController : Controller
    {
        private IRepositorioArticulos repositorio;
        private IRepositorioMarcas repositorioMarcas;
        private IRepositorioCategorias repositorioCategorias;
        private IMapper mapper;
        public ArticuloController(IRepositorioArticulos repositorio,
            IRepositorioMarcas repositorioMarcas, 
            IRepositorioCategorias repositorioCategorias,
            IMapper mapper)
        {
            this.repositorio = repositorio;
            this.repositorioMarcas = repositorioMarcas;
            this.repositorioCategorias = repositorioCategorias;
            this.mapper = mapper;
        }
        public IActionResult Index(int Pagina, int RegistrosPorPagina)
        {

            PaginacionViewModel paginaViewModel = new PaginacionViewModel();

            if (Pagina != 0 && RegistrosPorPagina != 0)
            {
                paginaViewModel.Pagina = Pagina;
                paginaViewModel.RegistrosPorPagina = RegistrosPorPagina;

            }

            // Obtenemos una pagina desde el repositorio
            var paginaMostrar = repositorio.ObtenerPaginaArticulo(paginaViewModel);

            //Cargamos una pagina en PaginacionRespuesta
            var pagina = new PaginacionRespuesta<Articulo>
            {
                Elementos = paginaMostrar,
                Pagina = paginaViewModel.Pagina,
                CantidadTotalRegistros = paginaViewModel.CantidadTotalRegistros,
                RegistrosPorPagina = paginaViewModel.RegistrosPorPagina,

                // Asigna la url completa de la accion y controlador
                BaseUrl = Url.Action()

            };


            return View(pagina);
        }

        public IActionResult FormularioAlta()
        {
            ArticuloViewModel articuloNuevo = new ArticuloViewModel();

            //var listaMarcas = (from m in db.Marcas select new SelectListItem { Text = m.Descripcion.ToString(), Value = m.Id.ToString() }).ToList();
            //var listaCategorias = (from c in db.Categorias select new SelectListItem { Text = c.Descripcion.ToString(), Value = c.Id.ToString() }).ToList();
            var listaMarcas = repositorioMarcas.ObtenerItems();
            var listaCategorias = repositorioCategorias.ObtenerItems();

            articuloNuevo.Marca = listaMarcas;
            articuloNuevo.Categoria = listaCategorias;

            return View(articuloNuevo);
        }

        [HttpPost]
        public IActionResult FormularioAlta(ArticuloViewModel modelo)
        {
            if (modelo != null)
            {
                repositorio.AgregarArticulo(modelo.ToArticulo());

            }

            return RedirectToAction("Index");
        }

        public IActionResult ArticulosLista()
        {
            ArticulosListaViewModel modelo = new ArticulosListaViewModel();
            modelo.ListadoArticulos =  repositorio.ObtenerArticulos().ToList();
            modelo.FiltroModelo = new FiltroViewModel();
            Campo campo = Campo.Nombre;
            modelo.FiltroModelo.Criterio = ObtenerCriterio(campo).ToList();
            return View(modelo);
        }

        public IActionResult Detalle(int IdProducto)
        {
            var articuloSeleccionado = repositorio.ObtenerArticulo(IdProducto);
            var articulo = articuloSeleccionado.ToViewModel();
            articulo.Categoria = repositorioCategorias.ObtenerItems();
            articulo.Marca = repositorioMarcas.ObtenerItems();
            return View(articulo);
        }

        [Authorize]
        public IActionResult ModificarArticulo(int IdProducto)
        {
            var articuloSeleccionado = repositorio.ObtenerArticulo(IdProducto).ToViewModel();
            articuloSeleccionado.Categoria = repositorioCategorias.ObtenerItems();
            articuloSeleccionado.Marca = repositorioMarcas.ObtenerItems();

            return View(articuloSeleccionado);
        }

        [HttpPost]
        public IActionResult ModificarArticulo(ArticuloViewModel articulo)
        {
            Articulo articuloGuardar = articulo.ToArticulo();

            repositorio.ModificarArticulo(articuloGuardar);

            return RedirectToAction("Index");
        }

        public IActionResult EliminarArticulo(ArticuloViewModel articulo)
        {
            int idArticulo = articulo.Id;
            Articulo artBorrar = repositorio.ObtenerArticulo(idArticulo);
            repositorio.BorrarArticulo(artBorrar);

            return RedirectToAction("Index");
        }

        public IActionResult FiltroRapido(string Buscar)
        {
            List<Articulo> lista = repositorio.BuscarArticulo(Buscar);
            return View("ArticulosLista", lista);
        }

        [HttpPost]
        public IActionResult FiltroAvanzado(bool estado)
        {
            if (estado)
            {
                FiltroViewModel filtroViewModel = new FiltroViewModel();
                filtroViewModel.Campo = new List<SelectListItem>()
                {
                    new SelectListItem{ Text="Nombre", Value = "1", Selected = true },
                    new SelectListItem{ Text="Descripcion", Value = "2" },
                    new SelectListItem{ Text="Precio", Value = "3" }
                };
                filtroViewModel.Criterio = new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Comienza con", Value = "1"},
                    new SelectListItem { Text = "Termina con", Value = "2"},
                    new SelectListItem { Text = "Igual a", Value = "3" }
                };


                return PartialView("_FiltroAvanzado", filtroViewModel);
            }
            else
                return PartialView("_FiltroAvanzadoVacio");
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerCriterios([FromBody] Campo valorSeleccionado)
        {
            

            return Ok(ObtenerCriterio(valorSeleccionado));
        }

        private IEnumerable<SelectListItem> ObtenerCriterio(Campo valorSeleccionado)
        {
            List<SelectListItem> ListaCriterio;

            if (valorSeleccionado == Campo.Nombre || valorSeleccionado == Campo.Descripcion)
            {
                ListaCriterio = new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Comienza con", Value = "1"},
                    new SelectListItem { Text = "Termina con", Value = "2"},
                    new SelectListItem { Text = "Igual a", Value = "3" }
                };
            }
            else
            {
                ListaCriterio = new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Mayor a", Value = "1"},
                    new SelectListItem { Text = "Menor a", Value = "2"},
                    new SelectListItem { Text = "Igual a", Value = "3" }
                };
            }

            return ListaCriterio;
        }

        [HttpPost]
        public IActionResult BusquedaAvanzada(FiltroViewModel filtro)
        {


            //return Json(new { resultado = "Exito" });
            return PartialView("_CuerpoListaArticulos", repositorio.BusquedaAvanzada(filtro));
            
        }

        [HttpGet]
        public IActionResult Buscar(string buscar = "")
        {
            var listaArticulos = repositorio.ObtenerArticulos().ToList();
            var listaFiltrada = listaArticulos;

            if (!string.IsNullOrEmpty(buscar) && buscar.Length > 3)
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(buscar.ToUpper()) || 
                                                            x.Descripcion.ToUpper().Contains(buscar.ToUpper()));

            return PartialView("_CuerpoListaArticulos", listaFiltrada);

        }

        // Agregados para trabajar con JQuery Data Table
        // El resultado seran datos para el script de paginado JQ
        [HttpGet]
        public JsonResult ListaArticulos()
        {
            var Lista = ListarAVM();

            return Json(new { data = Lista });
        }

        [HttpGet]
        public IActionResult AdmProductos()
        {
            var Lista = ListarAVM();

            return View("AdmProductos", Lista);
        }

        public List<ArticuloViewModelDT> ListarAVM()
        {
            List<ArticuloViewModelDT> Lista = new List<ArticuloViewModelDT>();
            var listaA = repositorio.ObtenerArticulos();
            var listaM = repositorioMarcas.ObtenerMarcas();
            var listaC = repositorioCategorias.ObtenerCategorias();

            Lista = (from a in listaA
                     select new ArticuloViewModelDT
                     {
                         Id = a.Id,
                         Codigo = a.Codigo,
                         Nombre = a.Nombre,
                         Descripcion = a.Descripcion,
                         IdMarca = a.IdMarca,
                         Marca = (from m in listaM where m.Id == a.IdMarca select m.Descripcion).First(),
                         IdCategoria = a.IdCategoria,
                         Categoria = (from c in listaC where c.Id == a.IdCategoria select c.Descripcion).First(),
                         ImagenUrl = a.ImagenUrl,
                         Precio = a.Precio
                     }).ToList();

            return Lista;
        }


    }

   
}
