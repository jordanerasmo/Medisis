<%@ Page Title="Listado Consultas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoConsultas.aspx.cs" Inherits="Medesis.ListadoConsultas" %>

<asp:Content ContentPlaceHolderID="tituloPagina" runat="server">
     <i class="fas fa-book-medical"></i> Listado Consultas
</asp:Content>

<asp:Content ContentPlaceHolderID="contenidoPagina" runat="server">

    <div id="tablaListadoConsultas">
        <table id="TablaListadoConsultas" class="tablaListCons table table-striped table-bordered">
            <thead></thead>
            <tbody></tbody>
        </table>
    </div>

    <!-- Modal modificar datos consulta -->
    <div class="modal fade" id="modalModificarConsulta" tabindex="-1" aria-hidden="true">
      <div class="modal-dialog modal-lg modal-dialog-scrollable">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Modificar consulta</h5>
          </div>
          <div class="modal-body">

          <!-- Formulario -->

            <form class="row g-3">
                <div class="col-auto">
                    <label for="inputPassword2" class="visually-hidden">Password</label>
                    <input type="password" class="form-control" id="inputPassword2" placeholder="Password">
                </div>
                <div class="col-auto">
                    <button type="submit" class="btn btn-primary mb-3">Confirm identity</button>
                </div>
            </form>
          
          <!-- Fin Formulario -->

          </div>

          <div class="modal-footer">
            <button type="button" id="btnCerrarModalModificarConsulta" class="btn btn-secondary">Cancelar</button>
            <button type="button" id="btnActualizarConsulta" class="btn btn-primary">Modificar</button>
          </div>
        </div>
      </div>
    </div>

    <script src="../../Js/consultas.js"></script>
</asp:Content>

