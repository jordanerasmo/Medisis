using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class ConsultasController
    {
        /*
        public IList<DatosMedicoUIClass> getDatosMedico(int idMedico)
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                IList<DatosMedicoUIClass> datosMedico = (from M in dc.Medicos
                                                         join E in dc.Especialidades on M.Especialidad equals E.Id
                                                         join MC in dc.MedicosConsultorios on idMedico equals MC.Medico
                                                         join C in dc.Consultorios on MC.Consultorio equals C.Id
                                                         where M.Id == idMedico
                                                         select new DatosMedicoUIClass
                                                         {
                                                             IdMedico = M.Id,
                                                             Especialidad = E.Descripcion,
                                                             ConsultorioNro = C.Numero,
                                                             DescripcionCon = C.Observaciones
                                                         }).ToList();
                return datosMedico;   
            }
        }
        
        public IList<ObrasSocialesUIClass> getObraSocial(int idPaciente)
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                IList<ObrasSocialesUIClass> obraSocial = (from P in dc.Pacientes
                                                          join OB in dc.ObrasSociales on P.ObraSocial equals OB.Id
                                                          where P.Id == idPaciente
                                                          select new ObrasSocialesUIClass
                                                          {
                                                              Id = P.Id,
                                                              Nombre = OB.Nombre
                                                          }).ToList();
                return obraSocial;
            }
        }

        public IList<MedicosUIClass> getListadoMedicosPorPaciente(int idPaciente)
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                IList<MedicosUIClass> listadoMedicos = (from MP in dc.MedicosPacientes
                                                        where MP.Paciente == idPaciente
                                                        select new MedicosUIClass
                                                        {
                                                            Id = MP.Medico
                                                        }).ToList();
                
                return listadoMedicos;
            }
        }
        */
        public List<ConsultasUIClass> getConsultas()
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                List<ConsultasUIClass> listadoConsultas = (from C in dc.Consultas
                                                           join M in dc.Medicos on C.Medico equals M.Id
                                                           join P in dc.Pacientes on C.Paciente equals P.Id
                                                           select new ConsultasUIClass
                                                           {
                                                               Id = C.Id,
                                                               Medico = M.Nombres+" "+M.Apellidos,
                                                               Paciente = P.Nombres+" "+P.Apellidos,
                                                               UltimaFechaAtencion = C.UltimaFechaAtencion,
                                                               FechaSiguienteConsulta = C.FechaSiguienteConsulta.Value,
                                                               ConsultorioId = C.ConsultorioId,
                                                               Estado = C.Estado,
                                                               Observaciones = C.Observaciones,
                                                               NroConsulta = C.NroConsulta
                                                           }).ToList();
                return listadoConsultas;
            }
        }

        public IList<MedicosUIClass> getMedicos()
        {
            using(SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                IList<MedicosUIClass> listadoMedicos = (from M in dc.Medicos
                                                        select new MedicosUIClass
                                                        {
                                                            Id = M.Id,
                                                            Nombres = M.Nombres,
                                                            Apellidos = M.Apellidos
                                                        }).ToList();

                return listadoMedicos;
            }
        }

        public IList<PacientesUIClass> getPacientes()
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                IList<PacientesUIClass> listadoPacientes = (from P in dc.Pacientes
                                                            select new PacientesUIClass()
                                                            {
                                                                Id = P.Id,
                                                                Nombres = P.Nombres,
                                                                Apellidos = P.Apellidos
                                                            }).ToList();

                return listadoPacientes;
            }
        }

        /*
        public List<MedicosPacientesUIClass> traerInfoConsulta(int idConsulta)
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                List<MedicosPacientesUIClass> listadoConsultas = (from MP in dc.MedicosPacientes
                                                                  where MP.Id == idConsulta
                                                                  select new MedicosPacientesUIClass
                                                                  {
                                                                      Id = MP.Id,
                                                                      Medico = MP.Medico,
                                                                      Paciente = MP.Paciente,
                                                                      UltimaFechaAtencion = MP.UltimaFechaAtencion.Value,
                                                                      FechaSiguienteConsulta = MP.FechaSiguienteConsulta.Value
                                                                  }).ToList();
                return listadoConsultas;
            }
        }
        */
        /*
        public void grabarConsulta(int pacienteAtendido, int idMedico, int idPaciente, string fechaSiguienteConsulta)
        {
            if (pacienteAtendido == 0)
            {
                // Grabamos un nuevo registro.

                using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
                {
                    MedicosPacientes objConsulta = new MedicosPacientes();

                    DateTime fechaActual = DateTime.Now;

                    objConsulta.Medico = idMedico;
                    objConsulta.Paciente = idPaciente;
                    objConsulta.UltimaFechaAtencion = fechaActual;
                    objConsulta.FechaSiguienteConsulta = Convert.ToDateTime(fechaSiguienteConsulta);

                    dc.MedicosPacientes.Add(objConsulta);
                    dc.SaveChanges();
                }
            }else if (pacienteAtendido == 1)
            {
                // Actualizamos la fecha del registro.

                using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
                {
                    //Traemos el registro de la consulta donde coincida el id del medico con el del paciente

                    var consulta = dc.MedicosPacientes.Where(MP => MP.Medico == idMedico && MP.Paciente == idPaciente).FirstOrDefault();                   

                    //Obtenemos la fecha actual y modificamos el campo
                    DateTime fechaActual = DateTime.Now;
                    consulta.UltimaFechaAtencion = fechaActual;
                    consulta.FechaSiguienteConsulta = Convert.ToDateTime(fechaSiguienteConsulta);

                    dc.SaveChanges();
                }
            }
        }*/
    }


}
