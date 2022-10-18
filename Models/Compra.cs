using System;

namespace Obligatorio_2_NB_NT_V2.Models
{
    public class Compra
    {

        private static int ultimoId = 1;

        public int Id { get; }

        public Actividad Actividad { get; set; }

        public int CantidadEntradas { get; set; }

        public Usuario Usuario { get; set; }

        public DateTime FechaCompra { get; set; }

        public bool Activa { get; set; }

        public double PrecioTotal { get; set; }

        public double PrecioTotalCompra()
        {
            return CantidadEntradas * Actividad.CalcCostoFinalActividad();
        }

        public Compra(Actividad actividad, int cantidadEntradas, Usuario usuario, DateTime fechaCompra, bool activa)
        {
            Id = ultimoId;
            ultimoId++;
            Actividad = actividad;
            CantidadEntradas = cantidadEntradas;
            Usuario = usuario;
            FechaCompra = fechaCompra;
            Activa = activa;
            PrecioTotal = PrecioTotalCompra();
        }

    }
}
