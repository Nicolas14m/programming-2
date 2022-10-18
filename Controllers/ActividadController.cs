using Microsoft.AspNetCore.Mvc;
using Obligatorio_2_NB_NT_V2.Models;
using System;
using System.Collections.Generic;

namespace Obligatorio_2_NB_NT_V2.Controllers
{
    public class ActividadController : Controller
    {
        
        Sistema s = Sistema.GetInstancia();

        public IActionResult Index()
        {
            List<Actividad> actividades = s.GetActividades();
            return View(actividades);
        }

        public IActionResult AgregarMeGusta(int idActividad)
        {

            Actividad actividadBuscada = s.GetActividad(idActividad);

            actividadBuscada.CantidadMeGusta++;

            return RedirectToAction("Index", "Actividad");

        }

        public IActionResult ActividadPorLugar()
        {

            List<Lugar> lugares = s.GetLugares();

            ViewBag.lugares = lugares;

            List<Actividad> nuevaLista = new List<Actividad>();
            ViewBag.ActividadesEn = nuevaLista;

            return View(nuevaLista);
        }

        [HttpPost]
        public IActionResult ActividadPorLugar(int idLugar)
        {

            List<Actividad> actividadesEn = s.GetActividadesPorIdLugar(idLugar);

            ViewBag.ActividadesEn = actividadesEn;

            ViewBag.IdLugar = idLugar;

            List<Lugar> lugares = s.GetLugares();

            ViewBag.lugares = lugares;

            if (actividadesEn.Count > 0)
            {
                return View(actividadesEn);

            }
            else
            {
                ViewBag.Resultado = "No hay actividades para ese lugar";
                return View(actividadesEn);
            }
        }

        public IActionResult ActividadPorFechas()
        {

            List<Categoria> categorias = s.GetCategorias();
            ViewBag.categorias = categorias;

            List<Actividad> nuevaLista = new List<Actividad>();
            ViewBag.ActividadesEn = nuevaLista;


            return View(nuevaLista);
        }

        [HttpPost]
        public IActionResult ActividadPorFechas(DateTime fecha1, DateTime fecha2, string nombreCategoria)
        {

            ViewBag.Fecha1 = fecha1.ToString("yyyy-MM-dd");
            ViewBag.Fecha2 = fecha2.ToString("yyyy-MM-dd");
            ViewBag.nombreCategoria = nombreCategoria;

            List<Actividad> actividadesPorFecha = s.GetActividadesEntreFechas(fecha1, fecha2);

            List<Categoria> categorias = s.GetCategorias();
            ViewBag.categorias = categorias;

            actividadesPorFecha = s.GetActividadesPorCategoria(nombreCategoria, actividadesPorFecha);

            if (actividadesPorFecha.Count > 0)
            {
                return View(actividadesPorFecha);

            }
            else
            {
                ViewBag.Resultado = "No hay actividades para esas fechas y categoría";
                return View(actividadesPorFecha);
            }

        }

    }
}
