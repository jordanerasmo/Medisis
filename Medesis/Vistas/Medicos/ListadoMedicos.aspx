<%@ Page Title="Listado Medicos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoMedicos.aspx.cs" Inherits="Medesis.ListadoMedicos" %>

<asp:Content ContentPlaceHolderID="tituloPagina" runat="server">
    <i class="fas fa-user-md"></i>
    Listado Medicos
</asp:Content>

<asp:Content ContentPlaceHolderID="contenidoPagina" runat="server">

    <div id="tablaListadoMedicos">
        <table id="TablaListadoMedicos" class="tablaListMedico table table-striped table-bordered">
            <thead></thead>
            <tbody></tbody>
        </table>
    </div>

    <!-- Modal modificar datos -->

    <div class="modal fade" id="mdModMedico" data-backdrop="static"  tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Modificar informacion</h5>
                </div>
                <div class="modal-body">

                    <!-- Formulario -->
                      <div class="alert alert-secondary" role="alert" style="text-align:center">
                        Medico <strong id="nroMedico"></strong>
                      </div>

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

                        <div class="form-group">
                            <label for="estadoMedico" class="col-form-label">Estado</label>
                            <asp:DropDownList ID="estadoMedico" runat="server" class="custom-select custom-select-sm">
                                <asp:ListItem Value="0"> Activo </asp:ListItem>
                                <asp:ListItem Value="1"> Baja </asp:ListItem>
                            </asp:DropDownList>
                            <div class="invalid-feedback"></div>
                        </div>

                    <!-- Fin Formulario -->

                </div>

                <div class="modal-footer">
                    <button type="button" id="btnCerrarModEdMed" data-dismiss="modal" class="btn btn-sm btn-secondary">Cancelar</button>
                    <button type="button" id="btnModMedico" class="btn btn-sm btn-success"> <i class='fas fa-save'></i> Modificar</button>
                </div>

            </div>
        </div>
    </div>

    <script src="../../Js/medicos.js"></script>

</asp:Content>
