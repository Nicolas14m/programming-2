using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Obligatorio_2_NB_NT_V2.Models
{
    public class Usuario : IComparable<Usuario>
    {

        private static int ultimoId = 1;

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string NombreUsuario { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; }

        public DateTime FechaNacimiento { get; set; }

        private List<Compra> compras = new List<Compra>();



        public List<Compra> GetCompras()
        {
            return compras;
        }

        public void AgregarCompra(Compra c)
        {
            if (c != null)
            {
                compras.Add(c);
            }
        }

        public int CompareTo([AllowNull] Usuario other)
        {
            if (this.Apellido.CompareTo(other.Apellido) > 0)
            {
                return 1;

            }
            else if (this.Apellido.CompareTo(other.Apellido) < 0)
            {
                return -1;
            }
            else
            {
                if (this.Nombre.CompareTo(other.Nombre) > 0)
                {
                    return 1;
                }
                else if (this.Nombre.CompareTo(other.Nombre) < 0)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }


        // Constructor para Usuario
        public Usuario(string nombre, string apellido, string email, string nombreUsuario, string password, DateTime fechaNacimiento)
        {
            Id = ultimoId;
            ultimoId++;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            NombreUsuario = nombreUsuario;
            Password = password;
            Rol = "cliente";
            FechaNacimiento = fechaNacimiento;
        }

    }
}
