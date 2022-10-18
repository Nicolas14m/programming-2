using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obligatorio_2_NB_NT_V2.Models;
using System;
using System.Collections.Generic;

namespace Obligatorio_2_NB_NT_V2.Controllers
{
    public class CompraController : Controller
    {

        Sistema s = Sistema.GetInstancia();


        public IActionResult Comprar(int idActividad)
        {
            if (HttpContext.Session.GetString("logueadoUsuario") != null)
            {
                Actividad actividad = s.GetActividad(idActividad);
                ViewBag.Actividad = actividad;
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        public IActionResult Estadisticas()
        {
            if (HttpContext.Session.GetString("logueadoRol") == "operador")
            {
                List<Compra> comprasMayorValor = s.GetComprasMayorValor();
                return View(comprasMayorValor);

            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        public IActionResult ComprasEntreFechas()
        {

            List<Compra> todasLasCompras = s.GetCompras();


            double total = s.GetTotal(todasLasCompras);



            ViewBag.TotalRecaudado = total;

            if (HttpContext.Session.GetString("logueadoUsuario") != null && HttpContext.Session.GetString("logueadoRol") == "operador")
            {

                return View(todasLasCompras);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public IActionResult ComprasEntreFechas(DateTime fecha1, DateTime fecha2)
        {

            List<Compra> comprasEntreFechas = s.GetComprasEntreFechas(fecha1, fecha2);

            double totalRecaudadoEntreFechas = s.GetTotal(comprasEntreFechas);

            ViewBag.Fecha1 = fecha1.ToString("yyyy-MM-dd");
            ViewBag.Fecha2 = fecha2.ToString("yyyy-MM-dd");

            if (comprasEntreFechas.Count > 0)
            {

                ViewBag.TotalRecaudado = totalRecaudadoEntreFechas;
                return View(comprasEntreFechas);

            }
            else
            {
                ViewBag.Resultado = "No hay compras entre esas fechas";
                ViewBag.TotalRecaudado = 0;

                return View(comprasEntreFechas);
            }
        }

        [HttpPost]
        public IActionResult Comprar(int idActividad, int cantidadEntradas)
        {

            int? idusuario = HttpContext.Session.GetInt32("logueadoId");
            Usuario usuarioLogueado = s.GetUsuario(idusuario);
            Actividad actividad = s.GetActividad(idActividad);
            DateTime fechaActual = DateTime.Now;
            ViewBag.Actividad = actividad;
            ViewBag.Usuario = usuarioLogueado;

            Compra nuevaCompra = s.NuevaCompra(actividad, cantidadEntradas, usuarioLogueado, fechaActual, true);

            if (nuevaCompra != null)
            {

                ViewBag.ResultadoCompra = "Compra realizada con éxito";

                return View();

            }
            else
            {
                ViewBag.ResultadoCompra = "Error al comprar, verifique sus datos";
                return View();
            }
        }

        public IActionResult MisCompras()
        {



            if (HttpContext.Session.GetString("logueadoUsuario") != null)
            {
                int? idusuario = HttpContext.Session.GetInt32("logueadoId");
                Usuario usuarioLogueado = s.GetUsuario(idusuario);

                List<Compra> compras = new List<Compra>();
                compras = s.GetComprasPorUsuario(idusuario);

                return View(compras);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }



        }

        public IActionResult Cancelar(int idCompra)
        {

            if (HttpContext.Session.GetString("logueadoRol") == "cliente")
            {
                Compra compraACancelar = s.GetCompraPorId(idCompra);

                if (compraACancelar != null)
                {
                    ViewBag.PuedeCancelar = s.PuedeCancelarCompra(compraACancelar.Actividad.Fecha);
                    ViewBag.CompraACancelar = compraACancelar;

                    return View();
                }
                else
                {
                    ViewBag.CompraACancelar = "Hubo un error con la compra buscada";
                    return View();

                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }








        }

        [HttpPost]
        public IActionResult Cancelar(int idCompraACancelar, int _)
        {

            Compra compraACancelar = s.GetCompraPorId(idCompraACancelar);

            if (compraACancelar != null)
            {
                compraACancelar.Activa = false;
                ViewBag.Resultado = "Compra cancelada con éxito";
                ViewBag.CompraACancelar = compraACancelar;
            }


            return View();

        }


    }
}
