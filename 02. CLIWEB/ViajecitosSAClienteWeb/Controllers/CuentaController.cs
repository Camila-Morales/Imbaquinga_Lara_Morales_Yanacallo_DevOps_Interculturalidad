using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViajecitosSAClienteWeb.CondorService;
using ViajecitosSAClienteWeb.Models;

namespace ViajecitosSAClienteWeb.Controllers
{
    public class CuentaController : Controller
    {
        private CondorServiceSoapClient client = new CondorServiceSoapClient();

        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var resultado = client.Login(model.Username, model.Password);
            if (resultado.Contains("concedido"))
            {
                Session["Usuario"] = model.Username;
                return RedirectToAction("Buscar", "Vuelo");
            }
            ViewBag.Error = "Credenciales incorrectas.";
            return View();
        }
    }
}