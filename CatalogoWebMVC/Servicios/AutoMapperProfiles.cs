using AutoMapper;
using CatalogoWebMVC.Data;
using CatalogoWebMVC.Models;

namespace CatalogoWebMVC.Servicios
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Articulo, ArticuloViewModel>().ReverseMap();
        }
    }
}
