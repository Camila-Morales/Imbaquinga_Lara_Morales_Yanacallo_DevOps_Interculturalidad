using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViajecitosSAClienteWeb.Models
{
    public class BuscarVueloViewModel
    {
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public DateTime Fecha { get; set; }

        public string ValorFiltro { get; set; } // <-- Nuevo campo

        public List<ViajecitosSAClienteWeb.CondorService.Vuelo> Resultados { get; set; } = new List<ViajecitosSAClienteWeb.CondorService.Vuelo>();
    }

}