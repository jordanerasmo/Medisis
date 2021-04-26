using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PacientesUIClass
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Documento { get; set; }
        public int ObraSocial { get; set; }
        public string ObraSocialNombre { get; set; }
        public int Estado { get; set; }

    }
}
