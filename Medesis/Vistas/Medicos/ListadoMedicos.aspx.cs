using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Controllers;
using Models;

namespace Medesis
{
    public partial class ListadoMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> llenarTablaMedicos() </script>");
            llenarListados();
        }

        [WebMethod]
        public static object listadoMedicosTabla()
        {
            MedicosController objMedico = new MedicosController();
            IList<MedicosUIClass> listado = objMedico.getMedicos();
            return listado;
        }

        [WebMethod]
        public static object generarFormularioModMedico(string idMedico)
        {
            int idM = Int32.Parse(idMedico);
            MedicosController objMedico = new MedicosController();
            object medico = objMedico.getMedico(idM);
            return medico;
        }
        

        private void llenarListados()
        {
            MedicosController objMedico = new MedicosController();

            especialidadMedico.Items.Add(new ListItem("Seleccione", "0"));

            IList<EspecialidadesUIClass> objEspecialidad = objMedico.getEspecialidades();
            foreach (EspecialidadesUIClass obj in objEspecialidad)
            {
                this.especialidadMedico.Items.Add(new ListItem(obj.Descripcion, obj.Id.ToString()));
            }
        }

        [WebMethod]
        public static string updateMedico(int id, String nombreMedicoU, String apellidoMedicoU, int especialidadMedicoU, String matriculaMedicoU, int estadoMedicoU, String matComp)
        {
            return validarCamposModMedico(id, nombreMedicoU, apellidoMedicoU, especialidadMedicoU, matriculaMedicoU, estadoMedicoU, matComp);
        }

        public static string validarCamposModMedico(int idMedico, string nombreMedicoU, string apellidoMedicoU, int especialidadMedicoU, string matriculaMedicoU, int estadoMedicoU, string matComp)
        {
            var nombre = nombreMedicoU;
            var apellido = apellidoMedicoU;
            var especialidad = especialidadMedicoU;
            var matricula = matriculaMedicoU;
            var estado = estadoMedicoU;
            var matComparar = matComp;
            
            if (nombre != "" && apellido != "" && especialidad != 0 &&
                matricula != "" && estado >= 0 && estado <= 1  && matComparar != "")
            {
                //Validamos con expresiones regulares.
                var expSoloCaracteres = @"^[A-Za-zÁÉÍÓÚñáéíóúÑ]{3,10}$";
                var expMat = @"^([A-Za-z]{1})+([1-9]{2})$";

                bool expNombre = Regex.IsMatch(nombre, expSoloCaracteres);
                bool expApellido = Regex.IsMatch(apellido, expSoloCaracteres);
                bool expMatricula = Regex.IsMatch(matricula, expMat);

                MedicosController ObjMed = new MedicosController();
                var matriculaUnica = ObjMed.validarMatricula(matricula, matComparar);

                if (expNombre && expApellido && expMatricula && matriculaUnica)
                {
                    //Devolvemos la respuesta de la funcion modificar.
                    return modificarMedico(idMedico, nombre, apellido, especialidad, matricula, estado);
                }
                else
                {
                    return "error-validacion";
                }
            }
            else
            {
                return "datos-incompletos";
            }

        }

        protected static string modificarMedico(int idMedico, string nombre, string apellido, int especialidad, string matricula, int estado)
        {
            MedicosController objMedico = new MedicosController();
            return objMedico.modificarMedico(idMedico, nombre, apellido, especialidad, matricula, estado);
        }
    }
}