using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text.RegularExpressions;
using Controllers;
using Models;

namespace Medesis
{
    public partial class ListadoPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> llenarTablaPacientes() </script>");
            llenarListados();
        }

        [WebMethod]
        public static object llenarTablaPacientes()
        {
            PacientesController objPacientes = new PacientesController();
            IList<PacientesUIClass> listado = objPacientes.getPacientes();
            return listado;
        }

        protected void llenarListados()
        {
            PacientesController objPacienteController = new PacientesController();
            this.obraSocialPaciente.Items.Add(new ListItem("Seleccione", "0"));

            IList<ObrasSocialesUIClass> objObrasSociales = objPacienteController.getObrasSociales();
            foreach (ObrasSocialesUIClass obj in objObrasSociales)
            {
                this.obraSocialPaciente.Items.Add(new ListItem(obj.Nombre, obj.Id.ToString()));
            }

        }

        [WebMethod]
        public static object generarFormularioModPaciente(int idPaciente)
        {
            int idP = idPaciente;
            PacientesController objPac= new PacientesController();
            object paciente = objPac.getPaciente(idP);
            return paciente;
        }

        [WebMethod]
        public static string updatePaciente(int id, String nombrePacienteU, String apellidoPacienteU, String telefonoPacienteU, String emailPacienteU, String fechaNacPacienteU, String documentoPacienteU, int obraSocialPacienteU, int estadoPacienteU, String docAcomp, String emailAcomp)
        {
            return validarCamposModPaciente(id, nombrePacienteU, apellidoPacienteU, telefonoPacienteU, emailPacienteU, fechaNacPacienteU, documentoPacienteU, obraSocialPacienteU, estadoPacienteU, docAcomp, emailAcomp);
        }

        public static string validarCamposModPaciente(int idPaciente, String nombrePacienteU, String apellidoPacienteU, String telefonoPacienteU, String emailPacienteU, String fechaNacPacienteU, String documentoPacienteU, int obraSocialPacienteU, int estadoPacienteU, String docAcomp, String emailAcomp)
        {
            var nombre = nombrePacienteU;
            var apellido = apellidoPacienteU;
            var telefono = telefonoPacienteU;
            var email = emailPacienteU;
            var fechaNac = fechaNacPacienteU;
            var documento = documentoPacienteU;
            var obraSocial = obraSocialPacienteU;
            var estado = estadoPacienteU;
            var docAcomparar = docAcomp;
            var emailAcomparar = emailAcomp;

            if (nombre != "" && apellido != "" && telefono != "" &&
                email != "" && fechaNac != "" && documento != "" && 
                obraSocial != 0 && estado >= 0 && estado <= 1 && docAcomparar != "" && emailAcomparar != "")
            {
                //Validamos con expresiones regulares.
              
                var expSoloCaracteres = @"^[A-Za-zÁÉÍÓÚñáéíóúÑ /s]{3,20}?$";
                var expDoc = @"^([1-6]{1}?)+([0-9]{6,7}?)$";
                var expFec = @"^\d{1,2}\/\d{1,2}\/\d{2,4}$";
                var expEm = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
                var expTel = @"^[\s\.-]?\d{2,6}[\s-]?\d{3,9}$";

                bool expNombre = Regex.IsMatch(nombre, expSoloCaracteres);
                bool expApellido = Regex.IsMatch(nombre, expSoloCaracteres);
                bool expFecha = Regex.IsMatch(fechaNac, expFec);
                bool expEmail = Regex.IsMatch(email, expEm);
                bool expDocumento = Regex.IsMatch(documento, expDoc);
                bool expTelefono = Regex.IsMatch(telefono, expTel);

                PacientesController ObjPac= new PacientesController();
                var documentoUnico = ObjPac.validarDocumento(documento, docAcomparar);
                var emailUnico = ObjPac.validarEmail(email, emailAcomp);

                if (expNombre && expApellido && expFecha && expEmail && expDocumento && expTelefono && documentoUnico && emailUnico)
                {
                    //Devolvemos la respuesta de la funcion modificar.
                    return modificarPaciente(idPaciente, nombre, apellido, telefono, email, fechaNac, documento, obraSocial, estado);
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

        protected static string modificarPaciente(int idPaciente, String nombre, String apellido, String telefono, String email, String fechaNac, String documento, int obraSocial, int estado)
        {
            PacientesController objPac = new PacientesController();
            return objPac.modificarPaciente(idPaciente, nombre, apellido, telefono, email, fechaNac, documento, obraSocial, estado);
        }
    }
}