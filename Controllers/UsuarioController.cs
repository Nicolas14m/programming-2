using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Obligatorio_2_NB_NT_V2.Models;
using System;
using System.Collections.Generic;

namespace Obligatorio_2_NB_NT_V2.Controllers
{
    public class UsuarioController : Controller
    {

        Sistema s = Sistema.GetInstancia();


        public IActionResult Index()
        {
            return View();
        }



        // Login
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("logueadoUsuario") == null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(string nombreUsuario, string password)
        {
            nombreUsuario = nombreUsuario.ToLower();

            Usuario u = s.Login(nombreUsuario, password);

            if (u != null)
            {
                HttpContext.Session.SetInt32("logueadoId", u.Id);
                HttpContext.Session.SetString("logueadoNombre", u.Nombre);
                HttpContext.Session.SetString("logueadoApellido", u.Apellido);
                HttpContext.Session.SetString("logueadoRol", u.Rol);
                HttpContext.Session.SetString("logueadoUsuario", u.NombreUsuario);
                HttpContext.Session.SetString("logueadoEmail", u.Email);
                HttpContext.Session.SetString("logueadofechaNacimiento", (u.FechaNacimiento).ToString("dd/MM/yyyy"));

                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                ViewBag.Resultado = "Usuario o contraseña incorrectos";
                return View();
            }

        }

        // Logout

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("logueadoUsuario") != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult VerClientes()
        {

            List<Usuario> clientes = s.GetClientes();


            if (HttpContext.Session.GetString("logueadoRol") == "operador")
            {
                return View(clientes);
                
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public IActionResult Logout(string _)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        // Registro
        public IActionResult Registro()
        {
            if (HttpContext.Session.GetString("logueadoUsuario") == null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Registro(string nombre, string apellido, string nombreUsuario, string password, DateTime fechaNac, string email)
        {

            try
            {
            Usuario usuarioNuevo = s.AltaUsuario(nombre, apellido, email, nombreUsuario, password, fechaNac);

                if (usuarioNuevo != null)
                {
                    Login(nombreUsuario, password);
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception e)
            {

                ViewBag.ResultadoRegistro = e.Message;
                ViewBag.Nombre = nombre;
                ViewBag.Apellido = apellido;
                ViewBag.Password = password;
                ViewBag.FechaNac = fechaNac.ToString("yyyy-MM-dd");

            }

             return View();

     
        }

    }
}
