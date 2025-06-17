using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AeroCondorWS
{
    public class Vuelo
    {
        public int Id { get; set; }
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public decimal Valor { get; set; }
        public DateTime HoraSalida { get; set; }
    }
}
