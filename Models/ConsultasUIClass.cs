using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ConsultasUIClass
    {
        public int Id { get; set; }
        public string Medico { get; set; }
        public string Paciente{ get; set; }
        public DateTime UltimaFechaAtencion { get; set; }
        public DateTime FechaSiguienteConsulta { get; set; }
        public int ConsultorioId { get; set; }
        public int Estado { get; set; }
        public string Observaciones { get; set; }
        public string NroConsulta { get; set; }
    }
}
