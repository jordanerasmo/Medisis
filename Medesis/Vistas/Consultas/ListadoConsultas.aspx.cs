using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using Models;
using Controllers;

namespace Medesis
{
    public partial class ListadoConsultas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e){
            ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> llenarTablaConsultas() </script>");
        }


        [WebMethod]
        public static object listadoConsultasTabla()
        {
            ConsultasController objConsulta = new ConsultasController();
            List<ConsultasUIClass> listado = objConsulta.getConsultas();
            return listado;
        }

        protected void darBajaConsulta(int idConsulta)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", "<script> alertify.success('Se da de baja la consulta') </script>");
        }

    }
}