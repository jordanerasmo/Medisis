<%@ Page Language="C#" Title="Registrar Paciente" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoPaciente.aspx.cs" Inherits="Medesis.Pacientes" %>

<asp:Content ContentPlaceHolderID="tituloPagina" runat="server">
    <i class="fas fa-plus"></i> Registrar Paciente
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="contenidoPagina" runat="server">

    <div class="card border mb-3" style="width:27rem">
        <div class="card-header">Datos del paciente</div>
        <div class="card-body">

            <div class="form-row">            
                <div class="form-group col-sm-6">
                    <label for="nombrePaciente" class="col-form-label">Nombre</label>
                    <asp:TextBox ID="nombrePaciente" runat="server" class="form-control form-control-sm"></asp:TextBox>
                    <div class="invalid-feedback"></div>
                </div>

                <div class="form-group col-sm-6">
                    <label for="apellidoPaciente" class="col-form-label">Apellido</label>
                    <asp:TextBox ID="apellidoPaciente" runat="server" class="form-control form-control-sm"></asp:TextBox>
                    <div class="invalid-feedback"></div>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-sm-7">
                    <label for="fechaNacPaciente" class="col-form-label">Nacimiento</label>
                    <asp:TextBox ID="fechaNacPaciente" Type="date" runat="server" class="form-control form-control-sm"></asp:TextBox>
                    <div class="invalid-feedback"></div>
                </div>

                <div class="form-group col-sm-5">
                    <label for="documentoPaciente" class="col-form-label">Documento</label>
                    <asp:TextBox ID="documentoPaciente" runat="server" class="form-control form-control-sm"></asp:TextBox>
                    <div class="invalid-feedback"></div>
                    <div class="valid-feedback"></div>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-sm-7">
                    <label for="emailPaciente" class="col-form-label">Email</label>
                    <asp:TextBox ID="emailPaciente" Type="email" runat="server" class="form-control form-control-sm"></asp:TextBox>
                    <div class="invalid-feedback"></div>
                    <div class="valid-feedback"></div>
                </div>

                <div class="form-group col-sm-5">
                    <label for="telefonoPaciente" class="col-form-label">Telefono</label>
                    <asp:TextBox ID="telefonoPaciente" runat="server" class="form-control form-control-sm"></asp:TextBox>
                    <div class="invalid-feedback"></div>
                </div>
            </div>

            <div class="form-group">
                <label for="obraSocialPaciente" class="col-form-label">Obra Social</label>
                <asp:DropDownList ID="obraSocialPaciente" runat="server" class="custom-select custom-select-sm"></asp:DropDownList>
                <div class="invalid-feedback"></div>
            </div>
        </div>
        <div class="card-footer">
            <asp:LinkButton ID="btnCancelarAgPaciente" OnClientClick="return limpiarCamposAgPaciente()" runat="server" Text="<i class='fas fa-times'></i> Cancelar" class="btn btn-sm" style="float: left"/>
            <asp:LinkButton ID="btnGrabarPaciente" OnClientClick="return validarPaciente()" runat="server" Text="<i class='fas fa-save'></i> Guardar" class="btn btn-sm btn-success" style="float: right" OnClick="btnGrabarPaciente_Click"/>
        </div>
    </div>

    <script src="../../Js/pacientes.js"></script>
</asp:Content>
