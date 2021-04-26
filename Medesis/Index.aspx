<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Medesis._Default" %>

<asp:Content ContentPlaceHolderID="tituloPagina" runat="server">
    <i class="fas fa-home-alt"></i> Inicio 
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="contenidoPagina" runat="server">

    <div class="jumbotron" style="text-align: center">
        <h1>Medisis</h1>
        <p class="lead">Sistema para la gestion de consultas medicas.</p>
    </div>

</asp:Content>
