<%@ Page Title="Listado Pacientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoPacientes.aspx.cs" Inherits="Medesis.ListadoPacientes" %>

<asp:Content ContentPlaceHolderID="tituloPagina" runat="server">
    <i class="fas fa-user-injured"></i>
    Listado Pacientes
</asp:Content>

<asp:Content ContentPlaceHolderID="contenidoPagina" runat="server">

    <div id="tablaListadoPacientes">
        <table id="TablaListadoPacientes" class="tablaListPac table table-striped table-bordered">
            <thead></thead>
            <tbody></tbody>
        </table>
    </div>

    <!-- Modal modificar datos -->

    <div class="modal fade" id="mdModPac" data-backdrop="static" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Modificar informacion</h5>
                </div>
                <div class="modal-body">

                    <!-- Formulario -->
                    <div class="alert alert-secondary" role="alert" style="text-align: center">
                        Paciente <strong id="nroPac"></strong>
                    </div>

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

                    <div class="form-group">
                        <label for="estadoPaciente" class="col-form-label">Estado</label>
                        <asp:DropDownList ID="estadoPaciente" runat="server" class="custom-select custom-select-sm">
                            <asp:ListItem Value="0"> Activo </asp:ListItem>
                            <asp:ListItem Value="1"> Baja </asp:ListItem>
                        </asp:DropDownList>
                        <div class="invalid-feedback"></div>
                    </div>

                    <!-- Fin Formulario -->

                </div>

                <div class="modal-footer">
                    <button type="button" id="btnCerrarModEdPac" data-dismiss="modal" class="btn btn-sm btn-secondary"> Cancelar</button>
                    <button type="button" id="btnModPac" class="btn btn-sm btn-success"> <i class='fas fa-save'></i> Modificar</button>
                </div>

            </div>
        </div>
    </div>

    <script src="../../Js/pacientes.js"></script>

</asp:Content>
