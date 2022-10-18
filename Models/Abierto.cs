namespace Obligatorio_2_NB_NT_V2.Models
{
    public class Abierto : Lugar
    {

        private static double precioButaca = 150;

        public Abierto(string nombre, double dimensiones)
        {
            Id = ultimoId;
            ultimoId++;
            Nombre = nombre;
            Dimensiones = dimensiones;
        }

        // Metodo para cambiar el precio de la butaca, toma como parametro un Double y si se cumple la validación se asigna el nuevo valor, devuleve un booleano.

        public static bool CambiarPrecioButaca(double precioNuevo)
        {
            bool exito = false;

            if (precioNuevo >= 0)
            {
                precioButaca = precioNuevo;
                exito = true;
            }
            return exito;
        }

        public static double GetPrecioButaca()
        {
            return precioButaca;
        }

        public override double CalcCostoFinal(double precioBase)
        {
            if(Dimensiones > 1000) // Usamos metros cuadrados para nuestra precarga por lo tanto 1km2 = 1000m2
            {
                return precioBase * 1.10;
            } else
            {
                return precioBase;
            }
        }

    }
}
