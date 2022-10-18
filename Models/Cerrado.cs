namespace Obligatorio_2_NB_NT_V2.Models
{
    public class Cerrado : Lugar
    {
        public bool AccesibilidadTotal { get; set; }
        public double CostoMantenimiento { get; set; }

        public Cerrado(string nombre, double dimensiones, bool accesibilidad, double costoMantenimiento)
        {
            Id = ultimoId;
            ultimoId++;
            Nombre = nombre;
            Dimensiones = dimensiones;
            AccesibilidadTotal = accesibilidad;
            CostoMantenimiento = costoMantenimiento;
        }

        public override double CalcCostoFinal(double precioBase)
        {
            double ret = precioBase;


            if (Sistema.GetAforo() < 50)
            {
                ret = ret * 1.30;
            }
            else if (Sistema.GetAforo() >= 50 && Sistema.GetAforo() <= 70)
            {
                return ret = ret * 1.15;
            }


            return ret;

        }

    }
}
