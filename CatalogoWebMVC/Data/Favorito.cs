﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CatalogoWebMVC.Data
{
    [Keyless]
    [Table("FAVORITOS")]
    public partial class Favorito
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdArticulo { get; set; }
    }
}