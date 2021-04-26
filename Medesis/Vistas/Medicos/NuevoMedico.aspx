<%@ Page Language="C#" Title="Agregar Medico" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoMedico.aspx.cs" Inherits="Medesis.Medicos" %>

<asp:Content ContentPlaceHolderID="tituloPagina" runat="server">
    <i class="fas fa-plus"></i> Agregar Medico
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="contenidoPagina" runat="server">

    <div class="card" style="width: 27rem" id="formularioNuevoMedico">

      <div class="card-header">Datos del medico</div>
      <div class="card-body">
   
          <div class="form-row">
              <div class="form-group col-sm-6">
                  <label for="nombreMedico" class="col-form-label">Nombre</label>
                  <asp:TextBox ID="nombreMedico" runat="server" class="form-control form-control-sm"></asp:TextBox>
                  <div class="invalid-feedback"></div>
              </div>
              <div class="form-group col-sm-6">
                  <label for="apellidoMedico" class="col-form-label">Apellido</label>
                  <asp:TextBox ID="apellidoMedico" runat="server" class="form-control form-control-sm"></asp:TextBox>
                  <div class="invalid-feedback"></div>
              </div>
          </div>

          <div class="form-group">
              <label for="especialidadMedico" class="col-form-label">Especialidad</label>
              <asp:DropDownList ID="especialidadMedico" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
              <div class="invalid-feedback"></div>
          </div>

          <div class="form-group">
              <label for="matriculaMedico" class="col-form-label">Matricula</label>
              <asp:TextBox ID="matriculaMedico" runat="server" class="form-control form-control-sm"></asp:TextBox>
              <div class="invalid-feedback"></div>
              <div class="valid-feedback"></div>
          </div>
         
      </div>

      <div class="card-footer">
          <asp:LinkButton ID="btnCancelarAgMedico" OnClientClick="return limpiarCamposMedico()" runat="server" Text="<i class='fas fa-times'></i> Cancelar" class="btn btn-sm" style="float:left"/>
          <asp:LinkButton ID="btnGrabarMedico" OnClientClick="return validarMedico()" runat="server" Text="<i class='fas fa-save'></i> Guardar" class="btn btn-sm btn-success" style="float:right" OnClick="btnGrabarMedico_Click"/>
      </div>

    </div>

    <script src="../../Js/medicos.js"></script>

</asp:Content>
