using System;
using System.Collections.Generic;

namespace Obligatorio_2_NB_NT_V2.Models
{
    public class Sistema
    {
        private static int aforoMaximo = 65;

        #region Singleton

        private static Sistema instancia = null;

        public static Sistema GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new Sistema();
            }
            return instancia;

        }

        private Sistema()
        {
            PreCarga();

        }

        #endregion


        #region Listas
        private List<Usuario> usuarios = new List<Usuario>();
        private List<Lugar> lugares = new List<Lugar>();
        private List<Actividad> actividades = new List<Actividad>();
        private List<Categoria> categorias = new List<Categoria>();
        private List<Compra> compras = new List<Compra>();

        internal double GetTotal(List<Compra> todasLasCompras)
        {
            double total = 0;

            foreach (Compra compra in todasLasCompras)
            {
                total += compra.PrecioTotal;

            }

            return total;
        }
        #endregion

        #region Obtener Listas


        public List<Usuario> GetUsuarios() => usuarios;
        public List<Lugar> GetLugares() => lugares;
        public List<Actividad> GetActividades() => actividades;

        public List<Categoria> GetCategorias() => categorias;
        public List<Compra> GetCompras() => compras;
        #endregion

        #region Funciones de Altas

        public Usuario AltaUsuario(string nombre, string apellido, string email, string nombreUsuario, string password, DateTime fechaNacimiento)
        {

            Usuario nuevoUsuario = null;

            if (ExisteUsuario(nombreUsuario))
            {
                throw new Exception("El usuario ya existe, pruebe uno nuevo");
            }


            if (ExisteEmail(email))
            {
                throw new Exception("El email ya esta asociado a un usuario registrado, pruebe uno nuevo");
            }

            if (!ExisteUsuario(nombreUsuario) && !ExisteEmail(email) && nombre != "" && nombre != null && nombre.Length >= 2 && apellido.Length >= 2 && apellido != "" && apellido != null && nombreUsuario.Length >= 4 && nombreUsuario != "" && email != "" && email != null && nombreUsuario != "" && nombreUsuario != null && fechaNacimiento < DateTime.Now && ValidarPassword(password))
            {
                nombreUsuario = nombreUsuario.ToLower();
                nuevoUsuario = new Usuario(nombre, apellido, email, nombreUsuario, password, fechaNacimiento);
                usuarios.Add(nuevoUsuario);
            }

            return nuevoUsuario;
        }





        public Abierto AltaLugarAbierto(string nombre, double dimensiones)
        {
            

                Abierto nuevoLugarAbierto = null;

                if (nombre != "" && dimensiones > 0)
                {
                    nuevoLugarAbierto = new Abierto(nombre, dimensiones);
                    lugares.Add(nuevoLugarAbierto);
                }
                return nuevoLugarAbierto;
            

        }

        public Cerrado AltaLugarCerrado(string nombre, double dimensiones, bool accesoTot, double costoMantenimieno)
        {
            

                Cerrado nuevoLugarCerrado = null;

                if (nombre != "" && dimensiones > 0 && costoMantenimieno >= 0)
                {
                    nuevoLugarCerrado = new Cerrado(nombre, dimensiones, accesoTot, costoMantenimieno);
                    lugares.Add(nuevoLugarCerrado);
                }
                return nuevoLugarCerrado;
            

        }

        public Actividad AltaActividad(string nombre, Categoria categoria, DateTime fecha, Lugar lugar, Actividad.ClasifEdad edad)
        {

            Actividad nuevaActividad = null;

            if (nombre != "" && categoria != null && fecha != null && fecha >= DateTime.Now && lugar != null)
            {
                nuevaActividad = new Actividad(nombre, categoria, fecha, lugar, edad);
                actividades.Add(nuevaActividad);
            }

            return nuevaActividad;
        }

        public Categoria AltaCategoria(string nombre, string descripcion)
        {

            Categoria nuevaCategoria = null;

            if (nombre != "" && descripcion != "")
            {
                nuevaCategoria = new Categoria(nombre, descripcion);
                categorias.Add(nuevaCategoria);
            }

            return nuevaCategoria;


        }

        public Compra NuevaCompra(Actividad actividad, int cantidadEntradas, Usuario usuario, DateTime fecha, bool activa)
        {

            Compra nuevaCompra = null;

            if (actividad != null && cantidadEntradas > 0 && usuario != null && fecha != null && fecha <= DateTime.Now)
            {
                nuevaCompra = new Compra(actividad, cantidadEntradas, usuario, fecha, activa);
                compras.Add(nuevaCompra); // se agrega a las compras del sistema
                usuario.AgregarCompra(nuevaCompra); // se agrega a su propia lista de compras

            }

            return nuevaCompra;
        }

        #endregion

        private void PreCarga()
        {

            // Clientes
            Usuario user1 = AltaUsuario("Eric", "Clapton", "eric@gmail.com", "eric1", "Eric1234", DateTime.Parse("1969-01-01"));
            Usuario user2 = AltaUsuario("Maria", "Del Carmen", "mary2002@gmail.com", "mariac", "Mery1234", DateTime.Parse("2002-01-12"));
            Usuario user3 = AltaUsuario("Gonzalo", "Clapton", "gonzag@gmail.com", "gonchi22", "Gonza1234", DateTime.Parse("1970-11-01"));
            Usuario user4 = AltaUsuario("Luciana", "Richline", "lurichline@gmail.com", "luchi99", "Lu1234", DateTime.Parse("1999-01-21"));
            Usuario user5 = AltaUsuario("Geogre", "Richline", "george@gmail.com", "gmaicol", "George1234", DateTime.Parse("1994-01-21"));
            Usuario user6 = AltaUsuario("Alberto", "Mondongo", "alberto@gmail.com", "albert3", "Alberto1234", DateTime.Parse("1977-01-21"));

            // Operadores
            Usuario oper1 = AltaUsuario("Santiago", "Ventura", "santi@gmail.com", "santisape", "Santi1234", DateTime.Parse("1987-01-18"));
            oper1.Rol = "operador";
            Usuario oper2 = AltaUsuario("Roberto", "Bolaño", "robert@gmail.com", "robertosape", "Roberto1234", DateTime.Parse("1991-03-05"));
            oper2.Rol = "operador";


            // Lugares Abiertos
            Abierto lugarA1 = AltaLugarAbierto("Parque Rodó", 200000);
            Abierto lugarA2 = AltaLugarAbierto("Teatro de Verano", 10000);
            Abierto lugarA3 = AltaLugarAbierto("Velódromo", 20000);
            Abierto lugarA4 = AltaLugarAbierto("Centenario", 22000);

            // Lugares Cerrados
            Cerrado lugarC1 = AltaLugarCerrado("Teatro Solis", 30000, true, 30000);
            Cerrado lugarC2 = AltaLugarCerrado("Museo Blanes", 10000, true, 10000);
            Cerrado lugarC3 = AltaLugarCerrado("Museo Zorilla de San Martín", 30000, false, 15000);

            // Categorias
            Categoria categoria1 = AltaCategoria("Cine", "Vea una pelicula en una pantalla");
            Categoria categoria2 = AltaCategoria("Teatro", "Vea actores en un escenario hacer una obra");
            Categoria categoria3 = AltaCategoria("Concierto", "Vea un espectaculo con músicos en un escenario");
            Categoria categoria4 = AltaCategoria("Feria", "Feria con muchos food trucks y exposiciones");


            // Actividades en lugares abiertos
            Actividad actividad1 = AltaActividad("Feria de juegos para todos", categoria4, DateTime.Parse("2021-12-21"), lugarA1, Actividad.ClasifEdad.P);
            Actividad actividad2 = AltaActividad("Agarrate Catalina en vivo", categoria3, DateTime.Parse("2022-02-01"), lugarA2, Actividad.ClasifEdad.P);
            Actividad actividad3 = AltaActividad("Peyote Asesino en vivo", categoria3, DateTime.Parse("2022-11-17"), lugarA3, Actividad.ClasifEdad.C18);
            Actividad actividad4 = AltaActividad("Hamlet - Shakespeare", categoria2, DateTime.Parse("2023-11-18"), lugarA2, Actividad.ClasifEdad.C16);
            Actividad actividad5 = AltaActividad("Matrix 4", categoria1, DateTime.Parse("2022-12-19"), lugarC1, Actividad.ClasifEdad.C13);

            // Actividades en lugares cerrados
            Actividad actividad6 = AltaActividad("Exposición de Picasso Interactiva", categoria4, DateTime.Parse("2021-12-21"), lugarC2, Actividad.ClasifEdad.P);
            Actividad actividad7 = AltaActividad("Shakespeare - Romeo y Julieta", categoria2, DateTime.Parse("2022-11-11"), lugarC1, Actividad.ClasifEdad.P);
            Actividad actividad8 = AltaActividad("Feria Interior de Comida", categoria4, DateTime.Parse("2022-11-17"), lugarC3, Actividad.ClasifEdad.P);
            Actividad actividad9 = AltaActividad("Concierto para Violin", categoria3, DateTime.Parse("2022-11-18"), lugarC1, Actividad.ClasifEdad.P);
            Actividad actividad10 = AltaActividad("Fería Esoterica", categoria4, DateTime.Parse("2023-12-19"), lugarC3, Actividad.ClasifEdad.C13);
            // Actividad que se realiza siempre mañana para probar funcionalidad:
            Actividad actividad11 = AltaActividad("Fecha Mañana Evento", categoria4, DateTime.Now.AddHours(24), lugarC3, Actividad.ClasifEdad.C13);

            // Alta compras
            Compra compra1 = NuevaCompra(actividad9, 8, user1, DateTime.Parse("2020-01-17"), true);
            Compra compra2 = NuevaCompra(actividad2, 1, user2, DateTime.Parse("2021-03-15"), true);
            Compra compra3 = NuevaCompra(actividad9, 8, user3, DateTime.Parse("2021-04-11"), true);
            Compra compra4 = NuevaCompra(actividad5, 7, user4, DateTime.Parse("2021-05-04"), true);
            Compra compra5 = NuevaCompra(actividad11, 2, user1, DateTime.Parse("2021-05-08"), true);

        }

        #region Funciones 

        public Usuario Login(string nombreUsuario, string password)
        {
            bool encontrado = false;
            Usuario buscado = null;

            foreach (Usuario u in usuarios) if (!encontrado)
                {
                    if (u.NombreUsuario.Equals(nombreUsuario) && u.Password.Equals(password))
                    {
                        encontrado = true;
                        buscado = u;
                    }

                }
            return buscado;
        }



        public bool PuedeCancelarCompra(DateTime fechaActividad)
        {
            bool ret = false;

            DateTime fechaActual = DateTime.Now;

            DateTime fechaLimiteParaCancelar = fechaActividad.AddHours(-24);

            if (fechaActual < fechaLimiteParaCancelar)
            {
                ret = true;
            }

            return ret;
        }


        private bool ValidarPassword(string password)
        {
            string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string minusculas = "abcdefghijklmnoupqrstuvwxyz";
            string digitos = "0123456789";

            bool tieneMayuscula = false;
            bool tieneMinuscula = false;
            bool tieneDigito = false;

            if (password.Length >= 6)
            {

                foreach (char c in password)
                {

                    if (mayusculas.Contains(c))
                    {
                        tieneMayuscula = true;
                    }

                    if (minusculas.Contains(c))
                    {
                        tieneMinuscula = true;
                    }

                    if (digitos.Contains(c))
                    {
                        tieneDigito = true;
                    }


                }

                if (tieneMayuscula && tieneMinuscula && tieneDigito)
                {
                    return true;
                }

                return false;
            }

            return false;
        }


        public bool ExisteUsuario(string nombreUsuario)
        {
            nombreUsuario = nombreUsuario.Trim().ToLower();

            bool ret = false;

            foreach (Usuario u in usuarios)
            {

                if (u.NombreUsuario == nombreUsuario)
                {
                    ret = true;
                }
            }

            return ret;

        }

        public bool ExisteEmail(string email)
        {
            email = email.Trim().ToLower();

            bool ret = false;

            foreach (Usuario u in usuarios)
            {

                if (u.Email == email)
                {
                    ret = true;
                }
            }

            return ret;

        }



        public static int GetAforo() => aforoMaximo;

        public Actividad GetActividad(int id)
        {
            Actividad actividad = null;


            foreach (Actividad a in actividades)
            {
                if (a.Id.Equals(id))
                {
                    actividad = a;
                }
            }
            return actividad;
        }

        public Usuario GetUsuario(int? id)
        {

            Usuario ret = null;

            foreach (Usuario u in usuarios)
            {
                if (u.Id.Equals(id))
                {
                    ret = u;
                }
            }
            return ret;
        }

        public List<Compra> GetComprasPorUsuario(int? id)
        {
            List<Compra> ret = new List<Compra>();

            foreach (Usuario u in usuarios)
            {

                if (u.Id.Equals(id))
                {
                    ret = u.GetCompras();
                }

            }
            return ret;

        }

        public List<Usuario> GetClientes()
        {
            List<Usuario> clientes = new List<Usuario>();



            foreach (Usuario u in usuarios)
            {
                if (u.Rol == "cliente")
                {
                    clientes.Add(u);
                }
            }

            clientes.Sort();
            return clientes;
        }

        public Compra GetCompraPorId(int id)
        {
            Compra ret = null;

            foreach (Compra c in compras)
            {

                if (c.Id.Equals(id))
                {
                    ret = c;

                }
            }

            return ret;
        }

        internal List<Actividad> GetActividadesPorCategoria(string categoria, List<Actividad> listaActividades)
        {

            List<Actividad> ret = new List<Actividad>();

            foreach (Actividad a in listaActividades)
            {

                if (a.Categoria.Nombre == categoria)
                {
                    ret.Add(a);
                }
            }

            return ret;
        }

        public List<Compra> GetComprasMayorValor()
        {

            List<Compra> comprasMayorValor = new List<Compra>();

            double max = 0;

            foreach (Compra c in compras)
            {

                if (c.PrecioTotal > max)
                {
                    comprasMayorValor.Clear();
                    max = c.PrecioTotal;
                    comprasMayorValor.Add(c);
                }

                else if (c.PrecioTotal == max)
                {
                    comprasMayorValor.Add(c);

                }

            }

            return comprasMayorValor;
        }
        internal List<Actividad> GetActividadesEntreFechas(DateTime f1, DateTime f2)
        {

            List<Actividad> ret = new List<Actividad>();

            // Invertimos fechas en caso de que sea necesario
            if (f1 > f2)
            {
                DateTime aux;
                aux = f2;
                f2 = f1;
                f1 = aux;
            }


            foreach (Actividad a in actividades)
            {

                if (a.Fecha >= f1 && a.Fecha <= f2)
                {
                    ret.Add(a);
                }


            }

            return ret;
        }

        public List<Actividad> GetActividadesPorIdLugar(int idLugar)
        {
            List<Actividad> actividadesEn = new List<Actividad>();

            foreach (Actividad a in actividades)
            {
                if (a.Lugar.Id.Equals(idLugar))
                {
                    actividadesEn.Add(a);

                }
            }

            return actividadesEn;
        }

        internal List<Compra> GetComprasEntreFechas(DateTime f1, DateTime f2)
        {

            List<Compra> ret = new List<Compra>();

            // Invertimos fechas en caso de que sea necesario
            if (f1 > f2)
            {
                DateTime aux;
                aux = f2;
                f2 = f1;
                f1 = aux;
            }


            foreach (Compra c in compras)
            {

                if (c.FechaCompra >= f1 && c.FechaCompra <= f2)
                {
                    ret.Add(c);
                }


            }

            return ret;
        }
        #endregion
    }

}

