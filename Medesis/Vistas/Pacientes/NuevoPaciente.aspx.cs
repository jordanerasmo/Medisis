using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text.RegularExpressions;
using Models;
using Controllers;

namespace Medesis
{
    public partial class Pacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarListados();
        }

        protected void llenarListados()
        {
            if (!IsPostBack)
            {
                PacientesController objPacienteController = new PacientesController();
                this.obraSocialPaciente.Items.Add(new ListItem("Seleccione", "0"));

                IList<ObrasSocialesUIClass> objObrasSociales = objPacienteController.getObrasSociales();
                foreach (ObrasSocialesUIClass obj in objObrasSociales)
                {
                    this.obraSocialPaciente.Items.Add(new ListItem(obj.Nombre, obj.Id.ToString()));
                }
            }
            
        }

        [WebMethod]
        public static bool validarDocumentoUnico(string documento)
        {
            PacientesController objPac = new PacientesController();
            bool respuesta = objPac.validarDocumento(documento, "");
            return respuesta;
        }

        [WebMethod]
        public static bool validarEmailUnico(string email)
        {
            PacientesController objPac = new PacientesController();
            bool respuesta = objPac.validarEmail(email, "");
            return respuesta;
        }

        protected void btnGrabarPaciente_Click(object sender, EventArgs e)
        {
            validarCamposAddPaciente();
        }

        protected void validarCamposAddPaciente()
        {
            if (nombrePaciente.Text != "" && 
                apellidoPaciente.Text != "" && 
                fechaNacPaciente.Text != "" && 
                documentoPaciente.Text != "" && 
                emailPaciente.Text != "" && 
                telefonoPaciente.Text != "" && 
                obraSocialPaciente.SelectedIndex != 0)
            {

                var fecha = Convert.ToDateTime(fechaNacPaciente.Text).ToString("dd/MM/yyyy");

                //Validamos con expresiones regulares.
                var expSoloCaracteres = @"^[A-Za-zÁÉÍÓÚñáéíóúÑ /s]{3,20}?$";
                var expDoc = @"^([1-6]{1}?)+([0-9]{6,7}?)$";
                var expFech = @"^\d{1,2}\/\d{1,2}\/\d{2,4}$";
                var expEm = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
                var expTel = @"^[\s\.-]?\d{2,6}[\s-]?\d{3,7}$";

                bool expNombre = Regex.IsMatch(nombrePaciente.Text, expSoloCaracteres);
                bool expApellido = Regex.IsMatch(apellidoPaciente.Text, expSoloCaracteres);
                bool expDocumento = Regex.IsMatch(documentoPaciente.Text, expDoc);
                bool expNacimiento = Regex.IsMatch(fecha, expFech);
                bool expEmail = Regex.IsMatch(emailPaciente.Text, expEm);
                bool expTelefono = Regex.IsMatch(telefonoPaciente.Text, expTel);

                PacientesController ObjPac = new PacientesController();
                var documentoUnico = ObjPac.validarDocumento(documentoPaciente.Text, "");
                var emailUnico = ObjPac.validarEmail(emailPaciente.Text, "");

                if (expNombre && expApellido && expDocumento && expNacimiento && expEmail && expTelefono && documentoUnico && emailUnico)
                {
                    grabarPaciente(fecha);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> alertify.error('Error al procesar la peticion.') </script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> alertify.error('Complete todos los campos.') </script>");
            }
        }

        protected void grabarPaciente(string fechaNacimiento)
        {
            PacientesController objPaciente = new PacientesController();
            bool accion = objPaciente.grabarPaciente(nombrePaciente.Text, apellidoPaciente.Text, fechaNacimiento, documentoPaciente.Text, emailPaciente.Text, telefonoPaciente.Text, obraSocialPaciente.SelectedIndex);
            if (accion)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> alertify.success('Paciente registrado.') </script>");
                limpiarCamposAddPaciente();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> alertify.error('Error al registrar.') </script>");
            }
        }

        protected void limpiarCamposAddPaciente()
        {
            nombrePaciente.Text = "";
            apellidoPaciente.Text = "";
            documentoPaciente.Text = "";
            fechaNacPaciente.Text = "";
            emailPaciente.Text = "";
            telefonoPaciente.Text = "";
            obraSocialPaciente.SelectedIndex = 0;
        }
    }

}