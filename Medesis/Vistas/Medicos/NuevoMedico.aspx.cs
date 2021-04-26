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
    public partial class Medicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarListados();
        }

        private void llenarListados()
        {
            if (!IsPostBack)
            {
                MedicosController objMedico = new MedicosController();

                especialidadMedico.Items.Add(new ListItem("Seleccione", "0"));

                IList<EspecialidadesUIClass> objEspecialidad = objMedico.getEspecialidades();
                foreach (EspecialidadesUIClass obj in objEspecialidad)
                {
                    this.especialidadMedico.Items.Add(new ListItem(obj.Descripcion, obj.Id.ToString()));
                }
            }
            
        }

        [WebMethod]
        public static bool validarMatriculaUnica(string matricula)
        {
            MedicosController objConsultaMatricula = new MedicosController();
            bool matDisponible = objConsultaMatricula.validarMatricula(matricula, "");
            return matDisponible;
        }

        protected void btnGrabarMedico_Click(object sender, EventArgs e)
        {
            validarCamposAddMedico();
        }
        protected void validarCamposAddMedico()
        {
            if (nombreMedico.Text != "" && apellidoMedico.Text != "" && especialidadMedico.SelectedIndex != 0 && matriculaMedico.Text != ""){

                //Validamos con expresiones regulares.
                var expSoloCaracteres = @"^[A-Za-zÁÉÍÓÚñáéíóúÑ]{3,10}$";
                var expMat = @"^([A-Za-z]{1})+([1-9]{2})$";

                bool expNombre = Regex.IsMatch(nombreMedico.Text, expSoloCaracteres);
                bool expApellido = Regex.IsMatch(apellidoMedico.Text, expSoloCaracteres);
                bool expMatricula = Regex.IsMatch(matriculaMedico.Text, expMat);

                MedicosController ObjMed = new MedicosController();
                var matriculaUnica = ObjMed.validarMatricula(matriculaMedico.Text, "");

                if (expNombre && expApellido && expMatricula && matriculaUnica)
                {
                    grabarMedico();
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

        protected void grabarMedico()
        {
            MedicosController objRegistro = new MedicosController();
            bool ret = objRegistro.grabarMedico(nombreMedico.Text, apellidoMedico.Text, especialidadMedico.Text, matriculaMedico.Text);
            if (ret)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> alertify.success('Medico registrado.') </script>");
                limpiarCamposAddMedico();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> alertify.error('Error al registrar...') </script>");
            }

        }

        protected void limpiarCamposAddMedico()
        {
            nombreMedico.Text = "";
            apellidoMedico.Text = "";
            especialidadMedico.SelectedIndex = 0;
            matriculaMedico.Text = "";
        }
    }
}