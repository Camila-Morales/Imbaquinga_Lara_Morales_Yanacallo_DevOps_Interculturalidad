using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViajecitosSAClienteWeb.CondorService;
using ViajecitosSAClienteWeb.Models;
using FacturaModel = ViajecitosSAClienteWeb.Models.Factura;


namespace ViajecitosSAClienteWeb.Controllers
{
    public class VueloController : Controller
    {
        private CondorServiceSoapClient client = new CondorServiceSoapClient();

        public ActionResult Buscar()
        {
            var vuelos = client.ObtenerVuelos("", ""); // trae todos
            var vueloMayorValor = vuelos?.OrderByDescending(v => v.Valor).FirstOrDefault();

            var model = new BuscarVueloViewModel
            {
                Resultados = vueloMayorValor != null ? new List<Vuelo> { vueloMayorValor } : new List<Vuelo>()
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Buscar(BuscarVueloViewModel model)
        {
            var origen = string.IsNullOrWhiteSpace(model.CiudadOrigen) ? "" : model.CiudadOrigen.ToUpper();
            var destino = string.IsNullOrWhiteSpace(model.CiudadDestino) ? "" : model.CiudadDestino.ToUpper();

            var vuelos = client.ObtenerVuelos(origen, destino);

            var vueloMayorValor = vuelos?.OrderByDescending(v => v.Valor).FirstOrDefault();

            model.Resultados = vueloMayorValor != null ? new List<Vuelo> { vueloMayorValor } : new List<Vuelo>();

            if (!model.Resultados.Any())
                ViewBag.Mensaje = "No se encontraron vuelos con esos criterios.";

            return View(model);
        }

        [HttpPost]
        public ActionResult Resultado(BuscarVueloViewModel model)
        {
            var vuelos = client.ObtenerVuelos(model.CiudadOrigen, model.CiudadDestino);
            if (vuelos == null || vuelos.Length == 0)
            {
                ViewBag.Mensaje = "Vuelo no Disponible";
                return View("Resultado");
            }

            var vueloMayorValor = vuelos.OrderByDescending(v => v.Valor).First();
            return View("Resultado", vueloMayorValor);
        }

        public ActionResult Comprar(int id)
        {
            var vuelo = client.ObtenerVueloPorId(id);

            if (vuelo == null)
            {
                ViewBag.Mensaje = "No se encontró el vuelo";
                return RedirectToAction("Buscar");
            }

            return View(vuelo);
        }


        [HttpPost]
        public ActionResult ConfirmarCompra(int vueloId, int cantidad)
        {
            var cliente = new CondorService.CondorServiceSoapClient();
            int usuarioId = 1; // Aquí deberías usar el ID del usuario logueado

            string mensaje = cliente.ComprarVueloConCantidad(vueloId, usuarioId, cantidad);
            var vuelo = cliente.ObtenerVueloPorId(vueloId);

            var modelo = new ConfirmacionCompraViewModel
            {
                Vuelo = vuelo,
                Mensaje = mensaje,
                Cantidad = cantidad
            };

            return View("Confirmacion", modelo);
        }


        public ActionResult BuscarPorFiltro()
        {
            return View(new BuscarVueloViewModel());
        }

        [HttpPost]
        public ActionResult BuscarPorFiltro(BuscarVueloViewModel model, string tipoFiltro)
        {
            if (string.IsNullOrWhiteSpace(tipoFiltro) || string.IsNullOrWhiteSpace(model.ValorFiltro))
            {
                ViewBag.Mensaje = "Debe ingresar un tipo de filtro y un valor.";
                return View(model);
            }

            var vuelos = client.ObtenerVuelosPorFiltro(tipoFiltro, model.ValorFiltro);
            model.Resultados = vuelos?.ToList() ?? new List<Vuelo>();

            if (!model.Resultados.Any())
                ViewBag.Mensaje = "No se encontraron vuelos con ese filtro.";

            return View(model);
        }

        [HttpGet]
        public ActionResult VerFactura()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerFactura(int facturaId)
        {
            var client = new CondorService.CondorServiceSoapClient();
            var facturaDTO = client.ObtenerFacturaPorId(facturaId);

            if (facturaDTO == null)
            {
                ViewBag.Mensaje = "No se encontró ninguna factura con ese ID.";
                return View();
            }

            var factura = new FacturaModel
            {
                Id = facturaDTO.Id,
                UsuarioId = facturaDTO.UsuarioId,
                NombreUsuario = facturaDTO.NombreUsuario,
                EdadUsuario = facturaDTO.EdadUsuario,
                NacionalidadUsuario = facturaDTO.NacionalidadUsuario,
                CiudadOrigen = facturaDTO.CiudadOrigen,
                CiudadDestino = facturaDTO.CiudadDestino,
                HoraSalida = facturaDTO.HoraSalida,
                Cantidad = facturaDTO.Cantidad,
                PrecioUnitario = facturaDTO.PrecioUnitario,
                PrecioTotal = facturaDTO.PrecioTotal,
                FechaEmision = facturaDTO.FechaEmision
            };

            return View(factura);
        }
    }
}