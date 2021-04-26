<%@ Page Language="C#" Title="Registrar Consulta" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaConsulta.aspx.cs" Inherits="Medesis.Consultas" %>

<asp:Content ContentPlaceHolderID="tituloPagina" runat="server">
    <i class="fas fa-plus"></i> Registrar Consulta
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="contenidoPagina" runat="server">

    <div class="card" style="width: 27rem">
        <div class="card-header">Datos de la consulta</div>
        <div class="card-body">

            <div class="form-group">
                <label for="medicoConsulta" class="col-form-label">Medico</label>
                <asp:DropDownList ID="medicoConsulta" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                <div class="invalid-feedback"></div>
            </div>

            <div class="form-group">
                <label for="pacienteConsulta" class="col-form-label">Paciente</label>
                <asp:DropDownList ID="pacienteConsulta" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                <div class="invalid-feedback"></div>
            </div>

            <div class="form-group">
                <label for="fechaSiguienteConsulta" class="col-form-label">Siguiente consulta</label>
                <asp:TextBox ID="fechaSiguienteConsulta" class="form-control form-control-sm" runat="server" type="date"></asp:TextBox>
                <div class="invalid-feedback"></div>
            </div>

        </div>

        <div class="card-footer">
            <asp:LinkButton ID="btnCancelarAgConsulta" OnClientClick="return limpiarCamposAgConsulta()" runat="server" Text="<i class='fas fa-times'></i> Cancelar" class="btn btn-sm" style="float: left"/>
            <asp:LinkButton ID="btnGrabarConsulta" OnClientClick="return validarCamposConsulta()" runat="server" Text="<i class='fas fa-save'></i> Guardar" class="btn btn-sm btn-success" style="float: right"/>
        </div>
    </div>

    <script src="../../Js/consultas.js"></script>

</asp:Content>



<%--
Mostrar datos
<p>
    <label for="mostrarConsultorio" class="col-form-label-sm">Consultorio:</label>
    <asp:Label ID="mostrarConsultorio" runat="server" class="col-form-label-sm">Consultorio 12 con camilla</asp:Label>
               
    <label for="mostrarEspecialidad" class="col-form-label-sm">Especialidad:</label>
    <asp:Label ID="mostrarEspecialidad" runat="server" class="col-form-label-sm">Cardiologia</asp:Label>
</p>

    
    

<p>
    <label for="mostrarObraSocial" class="col-form-label-sm">Obra social:</label>
    <asp:Label ID="mostrarObraSocial" runat="server" class="col-form-label-sm">OBRA SOCIAL DEL PERSONAL NAVAL</asp:Label>
</p>
--%> 
