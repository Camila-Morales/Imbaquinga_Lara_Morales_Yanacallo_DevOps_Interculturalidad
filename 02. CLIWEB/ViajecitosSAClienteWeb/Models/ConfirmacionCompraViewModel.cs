using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViajecitosSAClienteWeb.CondorService;

namespace ViajecitosSAClienteWeb.Models
{
    public class ConfirmacionCompraViewModel
    {
        public CondorService.Vuelo Vuelo { get; set; }
        public string Mensaje { get; set; }
        public int Cantidad { get; set; } // Nueva propiedad
    }


}
