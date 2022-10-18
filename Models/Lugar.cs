namespace Obligatorio_2_NB_NT_V2.Models
{
    public abstract class Lugar
    {

        protected static int ultimoId = 1;

        public int Id { get; set; }

        public string Nombre { get; set; }

        public double Dimensiones { get; set; }

        public abstract double CalcCostoFinal( double precioBase);

    }
}
