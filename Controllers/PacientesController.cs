using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class PacientesController
    {
        public IList<ObrasSocialesUIClass> getObrasSociales()
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                IList<ObrasSocialesUIClass> objObrasSociales = (from x in dc.ObrasSociales
                                                                select new ObrasSocialesUIClass()
                                                                {
                                                                    Id = x.Id,
                                                                    Codigo = x.Codigo,
                                                                    Nombre = x.Nombre
                                                                }).ToList();
                return objObrasSociales;
            }
        }
        
        public IList<PacientesUIClass> getPacientes()
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                IList<PacientesUIClass> listadoPacientes = (from P in dc.Pacientes
                                                            join OS in dc.ObrasSociales on P.ObraSocial equals OS.Id
                                                            select new PacientesUIClass
                                                            {
                                                                Id = P.Id,
                                                                Numero = P.Numero,
                                                                Apellidos = P.Apellidos,
                                                                Nombres = P.Nombres,
                                                                Telefono = P.Telefono,
                                                                Email = P.Email,
                                                                FechaNacimiento = P.FechaNacimiento,
                                                                Documento = P.Documento,
                                                                ObraSocial = P.ObraSocial,
                                                                Estado = P.Estado,
                                                                ObraSocialNombre = OS.Nombre
                                                            }).ToList();
                return listadoPacientes;
            }
        }
        
        public bool validarDocumento(string documento, string docAcomparar)
        {
            var doc = documento.Trim();

            IList<PacientesUIClass> listado = this.getPacientes();

            for (int i = 0; i < listado.Count; i++)
            {
                if (doc == docAcomparar)
                {
                    return true;
                }
                else
                {
                    if (listado[i].Documento.Trim() == doc)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public bool validarEmail(string email, string emailAcomparar)
        {
            var em = email.Trim().ToLower();

            IList<PacientesUIClass> listado = this.getPacientes();

            for(int i = 0; i<listado.Count; i++)
            {
                if(em == emailAcomparar)
                {
                    return true;
                }
                else
                {
                    if(listado[i].Email.Trim().ToLower() == em)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool grabarPaciente(string nombre, string apellido, string fechaNac, string documento, string email, string telefono, int obraSocial)
        {
           
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                Random r = new Random();
                string nro = (((char)r.Next('A', 'Z')).ToString() + r.Next(11111111, 99999999).ToString());

                Pacientes objPaciente = new Pacientes();

                objPaciente.Numero = nro;
                objPaciente.Nombres = nombre;
                objPaciente.Apellidos = apellido;
                objPaciente.FechaNacimiento = Convert.ToDateTime(fechaNac);
                objPaciente.Documento = documento;
                objPaciente.Email = email;
                objPaciente.Telefono = telefono;
                objPaciente.ObraSocial = obraSocial;
                objPaciente.Estado = 0;

                dc.Pacientes.Add(objPaciente);

                int ret = dc.SaveChanges();

                if (ret > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }



        }

        public object getPaciente(int idPaciente)
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                object listadoPacientes = (from P in dc.Pacientes
                                         where P.Id == idPaciente
                                         select new PacientesUIClass()
                                         {
                                             Id = P.Id,
                                             Numero = P.Numero,
                                             Apellidos = P.Apellidos,
                                             Nombres = P.Nombres,
                                             FechaNacimiento = P.FechaNacimiento,
                                             Documento = P.Documento,
                                             Email = P.Email,
                                             Telefono = P.Telefono,
                                             ObraSocial = P.ObraSocial,
                                             Estado = P.Estado
                                         }).ToList();

                return listadoPacientes;

            }
        }

        public string modificarPaciente(int idPaciente, String nombre, String apellido, String telefono, String email, String fechaNac, String documento, int obraSocial, int estado)
        {
            var fechaNacFor = Convert.ToDateTime(fechaNac);

            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                //Obtenemos el registro del paciente.
                var paciente = dc.Pacientes.FirstOrDefault(p => p.Id == idPaciente);

                if (paciente.Nombres == nombre &&
                   paciente.Apellidos == apellido &&
                   paciente.Telefono == telefono &&
                   paciente.Email == email &&
                   paciente.FechaNacimiento == fechaNacFor &&
                   paciente.Documento == documento &&
                   paciente.ObraSocial == obraSocial &&
                   paciente.Estado == estado)
                {
                    return "mismo-registro";
                }
                else
                {
                    paciente.Nombres = nombre;
                    paciente.Apellidos = apellido;
                    paciente.Telefono = telefono;
                    paciente.Email = email;
                    paciente.FechaNacimiento = fechaNacFor;
                    paciente.Documento = documento;
                    paciente.ObraSocial = obraSocial;
                    paciente.Estado = estado;

                    int r = dc.SaveChanges();

                    if (r > 0)
                    {
                        return "modificado";
                    }
                    else
                    {
                        return "no-modificado";
                    }
                }
            }
        }
    }
}
