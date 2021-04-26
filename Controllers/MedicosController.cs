using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class MedicosController
    {
        public IList<EspecialidadesUIClass> getEspecialidades()
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                IList<EspecialidadesUIClass> objEspecialidades = (from x in dc.Especialidades
                                                                  select new EspecialidadesUIClass()
                                                                  {
                                                                      Id = x.Id,
                                                                      Descripcion = x.Descripcion,
                                                                  }).ToList();
                return objEspecialidades;
            }
        }

        public IList<ConsultoriosUIClass> getConsultorios()
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                IList<ConsultoriosUIClass> objConsultorios = (from c in dc.Consultorios
                                                              select new ConsultoriosUIClass()
                                                              {
                                                                  Id = c.Id,
                                                                  Numero = c.Numero,
                                                                  Observaciones = c.Observaciones
                                                              }).ToList();
                return objConsultorios;
            }
        }

        public IList<MedicosUIClass> getMedicos()
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                IList<MedicosUIClass> listadoMedicos = (from M in dc.Medicos
                                                        join EM in dc.Especialidades on M.Especialidad equals EM.Id
                                                        select new MedicosUIClass()
                                                        {
                                                            Numero = M.numero,
                                                            Id = M.Id,
                                                            Nombres = M.Nombres+" "+M.Apellidos,
                                                            Especialidad = EM.Descripcion,
                                                            Matricula = M.Matricula,
                                                            Estado = M.Estado
                                                        }).ToList();

                return listadoMedicos;
                
            }
        }

        public object getMedico(int idMedico)
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                object listadoMedicos = (from M in dc.Medicos
                                         where M.Id == idMedico
                                         select new MedicosUIClass()
                                         {
                                             Id = M.Id,
                                             Numero = M.numero,
                                             Apellidos = M.Apellidos,
                                             Nombres = M.Nombres,
                                             Especialidad = M.Especialidad.ToString(),
                                             Matricula = M.Matricula,
                                             Estado = M.Estado
                                         }).ToList();

                return listadoMedicos;

            }
        }

        public bool validarMatricula(string matricula, string matAcomparar)
        {
            var mat = matricula.Trim().ToUpper();

            IList<MedicosUIClass> listado = this.getMedicos();

            for (int i = 0; i < listado.Count; i++)
            {
                if(mat == matAcomparar)
                {
                    return true;
                }
                else
                {
                    if (listado[i].Matricula.Trim().ToUpper() == mat)
                    {
                        return false;
                    }
                }
                
            }

            return true;
        }

        public bool grabarMedico(string nombre, string apellido, string especialidad, string matricula)
        {
            using (SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                Random r = new Random();         
                string nro = (((char)r.Next('A', 'Z')).ToString() + r.Next(11111111, 99999999).ToString());

                Medicos objMedico = new Medicos();

                objMedico.numero = nro;
                objMedico.Nombres = nombre;
                objMedico.Apellidos = apellido;
                if (!string.IsNullOrEmpty(especialidad))
                    objMedico.Especialidad = int.Parse(especialidad);
                objMedico.Matricula = matricula.ToUpper();
                objMedico.Estado = 0;

                dc.Medicos.Add(objMedico);
                int ret = dc.SaveChanges();

                if(ret > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string modificarMedico(int idMedico, string nombre, string apellido, int especialidad, string matricula, int estado)
        {
            using(SistemaMedicoEntities dc = new SistemaMedicoEntities())
            {
                //Obtenemos el registro del medico.
                var medico = dc.Medicos.FirstOrDefault(x => x.Id == idMedico);

                if(medico.Nombres == nombre && 
                   medico.Apellidos == apellido &&
                   medico.Especialidad == especialidad &&
                   medico.Matricula == matricula && 
                   medico.Estado == estado)
                {
                    return "mismo-registro";
                }
                else
                {
                    medico.Nombres = nombre;
                    medico.Apellidos = apellido;
                    medico.Especialidad = especialidad;
                    medico.Matricula = matricula.ToUpper();
                    medico.Estado = estado;

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
