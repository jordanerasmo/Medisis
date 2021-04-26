using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using Controllers;

namespace Medesis
{
    public partial class Consultas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarListados();
        }

        protected void llenarListados()
        {
            ConsultasController objConsulta = new ConsultasController();

            this.medicoConsulta.Items.Add(new ListItem("Seleccione", "0"));
            IList<MedicosUIClass> objMedicos = objConsulta.getMedicos();
            foreach (MedicosUIClass obj in objMedicos)
            {
                this.medicoConsulta.Items.Add(new ListItem(obj.Nombres + ' ' + obj.Apellidos, obj.Id.ToString()));
            }

            this.pacienteConsulta.Items.Add(new ListItem("Seleccione", "0"));
            IList<PacientesUIClass> objPacientes = objConsulta.getPacientes();
            foreach (PacientesUIClass obj in objPacientes)
            {
                this.pacienteConsulta.Items.Add(new ListItem(obj.Nombres + " " + obj.Apellidos, obj.Id.ToString()));
            }
        }
        /*
      protected void medicoConsulta_SelectedIndexChanged(object sender, EventArgs e)
      {
          int idMedico = int.Parse(this.medicoConsulta.SelectedValue);

          ConsultasController objConsulta = new ConsultasController();
          IList<DatosMedicoUIClass> objDatosMedico = objConsulta.getDatosMedico(idMedico);

          //Mostramos los datos del medico

          mostrarConsultorio.Text = "No registrado";
          mostrarEspecialidad.Text = "No registrado";

          foreach (var dato in objDatosMedico)
          { 
              mostrarConsultorio.Visible = true;
              mostrarEspecialidad.Visible = true;

              mostrarConsultorio.Text = "Nro " + dato.ConsultorioNro.ToString() + ", " + dato.DescripcionCon;
              mostrarEspecialidad.Text = dato.Especialidad;
          }

      }

     protected void pacientesConsulta_SelectedIndexChanged(object sender, EventArgs e)
     {
         int idPaciente = int.Parse(this.pacienteConsulta.SelectedValue);

         ConsultasController objConsulta = new ConsultasController();
         IList<ObrasSocialesUIClass> objObraSocial = objConsulta.getObraSocial(idPaciente);

         //Mostramos la obra social del paciente

         mostrarObraSocial.Text = "No registrada";

         foreach(var info in objObraSocial)
         {
             mostrarObraSocial.Visible = true;
             mostrarObraSocial.Text = info.Nombre;
         }


     }

     protected void btnGrabarConsulta_Click(object sender, EventArgs e)
     {
         //validarCamposAddConsulta();
     }


     protected void validarCamposAddConsulta()
     {
         var campoMedicoConsulta = false;
         var campoPacienteConsulta = false;
         var campoFechaSiguienteConsulta = false;

         if (medicoConsulta.Text != "0")
         {
             errorMedicoConsulta.Text = "";
             campoMedicoConsulta = true;
         }
         else
         {
             errorMedicoConsulta.Text = "Seleccione un medico";
         }

         if (pacienteConsulta.Text != "0")
         {
             errorPacienteConsulta.Text = "";
             campoPacienteConsulta = true;
         }
         else
         {
             errorPacienteConsulta.Text = "Seleccione un paciente";
         }

         if (fechaSiguienteConsulta.Text != "")
         {
             errorFechaSiguienteConsulta.Text = "";
             campoFechaSiguienteConsulta = true;
         }
         else
         {
             errorFechaSiguienteConsulta.Text = "Seleccione una fecha";
         }

         if (campoMedicoConsulta && campoPacienteConsulta && campoFechaSiguienteConsulta)
         {
             grabarConsulta();
         }
     }

     protected void grabarConsulta()
     {
         int idMedico = int.Parse(this.medicoConsulta.SelectedValue);
         int idPaciente = int.Parse(this.pacienteConsulta.SelectedValue);
         var fechaSigConsulta = fechaSiguienteConsulta.Text;

         ConsultasController objConsulta = new ConsultasController();
         IList<MedicosUIClass> objListadoMedicos = objConsulta.getListadoMedicosPorPaciente(idPaciente);

         int pacienteAtendido = 0;

         //Comprobamos si el paciente fue atendido por ese medico.
         foreach (var medico in objListadoMedicos)
         {
             if (medico.Id == idMedico)
             {
                 pacienteAtendido = 1;
             }
         }

         //Pasamos el parametro para guardar o actualizar.
         objConsulta.grabarConsulta(pacienteAtendido, idMedico, idPaciente, fechaSigConsulta);
         ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> alertify.success('Consulta registrada con exito'); </script>");
         limpiarCamposConsulta();
     }


     protected void limpiarCamposConsulta()
     {
         medicoConsulta.SelectedIndex = 0;
         pacienteConsulta.SelectedIndex = 0;

         mostrarConsultorio.Text = "-";
         mostrarEspecialidad.Text = "-";

         mostrarObraSocial.Text = "-";
         fechaSiguienteConsulta.Text = "";
     }
     */
    }
}