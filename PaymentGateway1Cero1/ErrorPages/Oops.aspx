<%@ Page Title="" Language="C#" MasterPageFile="~/SitePayments.Master" AutoEventWireup="true" CodeBehind="Oops.aspx.cs" Inherits="PaymentGateway1Cero1.ErrorPages.Oops" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="about-section spad">
        <div class="container">
            <div class="row">
                <div class="col-lg-6 offset-lg-6 about-text">
                    <h2>Oops</h2>
                    <h5>Mensaje del sistema.</h5>
                    <p>Señor ciudadano (a) no se puede abrir la pagina solicitada. Por favor realizar nuevamente la transacción.</p>
                </div>
            </div>
            <div class="about-img">
                <img src="../Content/img/about-img.png" />
            </div>
        </div>
    </section>
    <section class="newsletter-section gradient-bg">
        <div class="container text-white">
            <div class="row">
                <div class="col-lg-7 newsletter-text">
                    <h2>Consultar Historial transaccional</h2>
                    <p>Señor (a) si deseas consultar el historial transaccional ingresa dando click en el siguiente botón.</p>
                </div>
                <div class="col-lg-5 col-md-8 offset-lg-0 offset-md-2">
                    <asp:Button ID="btnHistorial" CssClass="site-btn sb-gradients" OnClick="btnHistorial_Click" runat="server" Text="Consultar Historial transaccional" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
