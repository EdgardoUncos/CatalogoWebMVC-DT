using CatalogoWebMVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatalogoWebMVC.Models
{
    public class ArticuloViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdMarca { get; set; }
        public string TMarca { get; set; }

        [Display(Name = "Categoria")]
        public int? IdCategoria { get; set; }
        public string TCategoria { get; set; }
        public string ImagenUrl { get; set; }
        public decimal? Precio { get; set; }

        public List<SelectListItem> Marca {  get; set; }

        public List<SelectListItem> Categoria{ get; set; }

        public Articulo ToArticulo()
        {
            return new Articulo()
            {
                Id = Id,
                Codigo = Codigo,
                Nombre = Nombre,
                Descripcion = Descripcion,
                IdMarca = IdMarca,
                IdCategoria = IdCategoria,
                ImagenUrl = ImagenUrl,
                Precio = Precio
            };
        }
    }
}
