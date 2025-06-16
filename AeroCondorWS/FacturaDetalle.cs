using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AeroCondorWS
{
    public class Factura
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public int EdadUsuario { get; set; }
        public string NacionalidadUsuario { get; set; }
        public string CiudadOrigen { get; set; }
        public string CiudadDestino { get; set; }
        public DateTime HoraSalida { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime FechaEmision { get; set; }
    }


}