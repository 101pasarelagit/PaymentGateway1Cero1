<%@ Page Title="" Language="C#" MasterPageFile="~/SitePayments.Master" AutoEventWireup="true" CodeBehind="ResponsePayment.aspx.cs" Inherits="PaymentGateway1Cero1.Pages.ResponsePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="LblCodigoMunicipio" Visible="false" runat="server"></asp:Label>
     <asp:Label ID="URLRetorno" Visible="false" runat="server"></asp:Label>
    <div class="container">
        <br />
        <div class="section-title text-center">
            <h2>
                <asp:Label ID="lblNombreEntidad" runat="server"></asp:Label></h2>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div>
                    <div class="widget-area">
                        <div class="widget">
                            <h2 class="widget-title">Detalle Transacción:</h2>
                            <div class="row">
                                <div class="col-md-4 process">
                                    <div class="process-step">
                                        <figure class="process-icon">
                                            <img src="../Content/img/process-icons/1.png" />
                                        </figure>
                                        <h4>Datos Transaccion</h4>
                                        <ul>
                                            <li>IP:
                                    <asp:Label ID="LblIP" runat="server"></asp:Label></li>
                                            <li>Estado :
                                    <asp:Label ID="LblTransactionState" runat="server"></asp:Label></li>
                                            <li>CUS:
                                    <asp:Label ID="LbltrazabilityCode" runat="server"></asp:Label></li>
                                            <li>Total:
                                    <asp:Label ID="LblTransactionValue" runat="server"></asp:Label></li>
                                            <li>Factura:
                                    <asp:Label ID="LblTickedID" runat="server"></asp:Label></li>
                                            <li>Fecha Expedicion:
                                    <asp:Label ID="LblSolicitDate" runat="server"></asp:Label></li>
                                            <li>Fecha Respuesta Banco:
                                    <asp:Label ID="LblBankProcessingDate" runat="server"></asp:Label></li>
                                            <li>CicloTransaccion:
                                    <asp:Label ID="LblTransactionCycle" runat="server"></asp:Label></li>
                                            <li>IVA:
                                    <asp:Label ID="LblVATAmount" runat="server"></asp:Label></li>
                                            <li>Banco:
                                    <asp:Label ID="lblBanco" runat="server"></asp:Label></li>
                                            <li>Trámite:
                                    <asp:Label ID="lbldescripcion" runat="server"></asp:Label></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-md-4 process">
                                    <div class="process-step">
                                        <figure class="process-icon">
                                            <img src="../Content/img/process-icons/2.png" alt="#" />
                                        </figure>
                                        <h4>Datos Pagador</h4>
                                        <ul>
                                            <li>Nombre:
                                    <asp:Label ID="LblNombre" runat="server"></asp:Label></li>
                                            <li>Identificación :
                                    <asp:Label ID="LblIdent" runat="server"></asp:Label></li>
                                            <li>Email:
                                    <asp:Label ID="LblEmail" runat="server"></asp:Label></li>
                                            <li>Telefono:
                                    <asp:Label ID="LblPhone" runat="server"></asp:Label></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-md-4 process">
                                    <div class="process-step">
                                        <figure class="process-icon">
                                            <img src="../Content/img/process-icons/3.png" alt="#">
                                        </figure>
                                        <h4>Datos Entidad</h4>
                                        <ul>
                                            <li>Razón Social:
                                    <asp:Label ID="lblEntidad" runat="server"></asp:Label></li>
                                            <li>Nit :
                                    <asp:Label ID="LblNit" runat="server"></asp:Label></li>
                                            <li>Telefono:
                                    <asp:Label ID="LblTelefono" runat="server"></asp:Label></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="section-title text-center">
                                    <asp:Button ID="btnFinalizar" runat="server" CssClass="site-btn sb-gradients" OnClick="btnFinalizar_Click" Text="Finalizar Transacción" />
                                    <asp:Button ID="btnComprobante" runat="server" CssClass="site-btn sb-gradients" OnClick="btnComprobante_Click" Text="Imprimir Comprobante" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
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
