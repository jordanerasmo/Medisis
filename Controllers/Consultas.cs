//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Controllers
{
    using System;
    using System.Collections.Generic;
    
    public partial class Consultas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Consultas()
        {
            this.HistorialConsultas = new HashSet<HistorialConsultas>();
        }
    
        public int Id { get; set; }
        public int Medico { get; set; }
        public int Paciente { get; set; }
        public System.DateTime UltimaFechaAtencion { get; set; }
        public Nullable<System.DateTime> FechaSiguienteConsulta { get; set; }
        public int ConsultorioId { get; set; }
        public int Estado { get; set; }
        public string Observaciones { get; set; }
        public string NroConsulta { get; set; }
    
        public virtual Medicos Medicos { get; set; }
        public virtual Pacientes Pacientes { get; set; }
        public virtual Consultorios Consultorios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialConsultas> HistorialConsultas { get; set; }
    }
}
