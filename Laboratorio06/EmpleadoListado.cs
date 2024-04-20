using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio06
{
    public class EmpleadoListado
    {
        public int IdEmpleado {  get; set; }
        public string Apellidos { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaContratacion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string CodPostal { get; set; }
        public decimal SueldoBasico { get; set; }
    }
}
