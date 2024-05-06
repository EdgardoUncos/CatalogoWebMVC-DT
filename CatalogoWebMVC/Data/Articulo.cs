﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CatalogoWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoWebMVC.Data
{
    [Table("ARTICULOS")]
    public partial class Articulo
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Codigo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Nombre { get; set; }
        [StringLength(150)]
        [Unicode(false)]
        public string Descripcion { get; set; }

        [Display(Name = "Marca")]
        public int? IdMarca { get; set; }

        [Display(Name = "Categoria")]
        public int? IdCategoria { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string ImagenUrl { get; set; }
        [Column(TypeName = "money")]
        public decimal? Precio { get; set; }

        public ArticuloViewModel ToViewModel()
        {
            return new ArticuloViewModel()
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