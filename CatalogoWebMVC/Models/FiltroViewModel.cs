using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatalogoWebMVC.Models
{
    public class FiltroViewModel
    {
        public string Texto { get; set; }
        public int IdCampo { get; set; }
        public string TextoCampo { get; set; }
        public int IdCriterio { get; set; }
        public string TextoCriterio { get; set; }
        public List<SelectListItem> Campo { get; set; }
        public List<SelectListItem> Criterio { get; set; }

        public Campo CampoSeleccionado { get; set; }


    }

    public enum Campo { Nombre, Descripcion, Precio };

    public enum Criterio1 { Comienza_con, Termina_con, Igual_a };

    public enum Criterio2 { Comienza_con, Termina_con, Igual_a };
}
