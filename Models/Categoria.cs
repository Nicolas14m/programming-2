namespace Obligatorio_2_NB_NT_V2.Models
{
    public class Categoria
    {

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Categoria(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            
        }
    }
}
