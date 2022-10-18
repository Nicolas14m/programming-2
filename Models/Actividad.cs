using System;

namespace Obligatorio_2_NB_NT_V2.Models
{
    public class Actividad
    {

        private static int ultimoId = 1;

        public int Id { get; }

        public string Nombre { get; set; }

        public Categoria Categoria { get; set; }

        public DateTime Fecha { get; set; }

        public Lugar Lugar { get; set; }

        public ClasifEdad EdadMinima { get; set; }

        public int CantidadMeGusta { get; set; }

        public double PrecioFinal { get; set; }

        private static double precioBase = 250;


        public enum ClasifEdad
        {
            P = 0,
            C13 = 13,
            C16 = 16,
            C18 = 18
        }

        
        public Actividad(string nombre, Categoria categoria, DateTime fecha, Lugar lugar, ClasifEdad edadMinima)
        {
            Id = ultimoId;
            ultimoId++;
            Nombre = nombre;
            Categoria = categoria;
            Fecha = fecha;
            Lugar = lugar;
            EdadMinima = edadMinima;
            CantidadMeGusta = 0;
            PrecioFinal = CalcCostoFinalActividad();
        }

        public double CalcCostoFinalActividad()
        {
            return Lugar.CalcCostoFinal(precioBase);
        }


        // Metodo que recibe como parametro un enum y retorna un string explicando la clasificación con un mensaje más amigable para el usuario 
        public string ClasifEdadPorNombre(ClasifEdad edadMinima)
        {

            if ((int)edadMinima == 0)
            {
                return "Para todo público";
            }
            else
            {
                return $"Mayores que {(int)edadMinima}";
            }
          }

        // Redefinimos ToString() para mostrar las caracteristicas de la Actividad
        public override string ToString()
        {
            return $"Nombre: {Nombre} │ Lugar: {Lugar.Nombre} │ Fecha: {Fecha.ToString("dd/MM/yyyy")} │ Clasificacion Edad:  {ClasifEdadPorNombre(EdadMinima)}  │ Categoría: {Categoria.Nombre}";
        }

    }
}
