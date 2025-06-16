using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;


namespace AeroCondorWS{
    [WebService(Namespace = "http://aerocondor.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CondorService : System.Web.Services.WebService
    {

        private VueloService vueloService = new VueloService();

        [WebMethod]
        public List<Vuelo> ObtenerVuelos(string origen, string destino)
        {
            return vueloService.ObtenerVuelos(origen, destino);
        }

        [WebMethod]
        public string ComprarVuelo(int vueloId, int usuarioId)
        {
            return vueloService.ComprarVuelo(vueloId, usuarioId);
        }

        [WebMethod]
        public Vuelo ObtenerVueloPorId(int id)
        {
            return vueloService.ObtenerVueloPorId(id);
        }

        [WebMethod]
        public List<Vuelo> ObtenerVuelosPorFiltro(string tipoFiltro, string valor)
        {
            return vueloService.ObtenerVuelosPorUnFiltro(tipoFiltro, valor);
        }

        [WebMethod]
        public string Login(string username, string password)
        {
            return vueloService.Login(username, password);
        }

        [WebMethod]
        public string ComprarVueloConCantidad(int vueloId, int usuarioId, int cantidad)
        {
            return vueloService.ComprarVuelo(vueloId, usuarioId, cantidad);
        }

        [WebMethod]
        public Factura ObtenerFacturaPorId(int facturaId)
        {
            return vueloService.ObtenerFacturaPorId(facturaId);
        }


    }

}
