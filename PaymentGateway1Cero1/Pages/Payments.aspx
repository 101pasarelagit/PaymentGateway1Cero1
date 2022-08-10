<%@ Page Title="" Language="C#" MasterPageFile="~/SitePayments.Master" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="PaymentGateway1Cero1.Pages.Payments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="TransationID" Visible="false" runat="server"></asp:Label>
    <asp:Label ID="URLRetorno" Visible="false" runat="server"></asp:Label>
    <div class="container">
        <br />
        <div class="section-title text-center">
            <h2>
                <asp:Label ID="LblNombreEntidad" runat="server"></asp:Label></h2>
        </div>
        <div class="row">
            <div class="col-md-5">
                <div>
                    <div class="widget-area">
                        <div class="widget">
                            <h2 class="widget-title">Datos Transacción:</h2>
                            <ul>
                                <li>
                                    <h2>
                                        <a href="#" style="font-size: 30px;">Factura:
                                           <asp:Label ID="lblFactura" runat="server"></asp:Label></a>
                                    </h2>
                                </li>
                                <li>
                                    <h2>
                                        <a href="#" style="font-size: 30px;">Tramite:
                                           <asp:Label ID="LblTramite" runat="server"></asp:Label></a>
                                    </h2>
                                </li>
                                <li>
                                    <h2>
                                        <a href="#" style="font-size: 30px;">Nombres:
                                           <asp:Label ID="lblNombres" runat="server"></asp:Label></a>
                                    </h2>
                                </li>
                                <li>
                                    <h2>
                                        <a href="#" style="font-size: 30px;">Identificación:
                                           <asp:Label ID="lblIdentificacion" runat="server"></asp:Label></a>
                                    </h2>
                                </li>


                                <li>
                                    <h2>
                                        <a href="#" style="font-size: 40px;">Total:
                                            <asp:Label ID="lblValorTotalPagar" runat="server"></asp:Label></a>
                                    </h2>
                                </li>
                            </ul>
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnFinalizar" CssClass="site-btn sb-gradients" OnClick="btnFinalizar_Click" runat="server" Text="Finalizar Transacción" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-7">
                <div>
                    <div class="widget-area">
                        <div class="widget">
                            <h2 class="widget-title">Datos Pagador:</h2>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>* Tipo de Persona</label>
                                        <asp:DropDownList ID="DllTipoPersona" class="form-control" TabIndex="1" runat="server">
                                            <asp:ListItem Value="0">Natural</asp:ListItem>
                                            <asp:ListItem Value="1">Jurídica</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class='form-group'>
                                        <label>* Tipo de Identificación</label><br />
                                        <div class="btn-group">
                                            <asp:DropDownList ID="dllTipoDocumento" class="form-control" runat="server" TabIndex="2">
                                                <asp:ListItem Value="1">Cédula de Ciudadanía</asp:ListItem>
                                                <asp:ListItem Value="2">Cédula de Extranjería</asp:ListItem>
                                                <asp:ListItem Value="3">Nit</asp:ListItem>
                                                <asp:ListItem Value="4">Tarjeta de Identidad</asp:ListItem>
                                                <asp:ListItem Value="5">Pasaporte</asp:ListItem>
                                                <asp:ListItem Value="6">ID’s</asp:ListItem>
                                                <asp:ListItem Value="7">CEL</asp:ListItem>
                                                <asp:ListItem Value="8">Registro Civil de Nacimiento</asp:ListItem>
                                                <asp:ListItem Value="9">Documento de Identificación Extranjero</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class='form-group'>
                                        <label>* Número de Documento</label>
                                        <asp:TextBox ID="TxtNumeroDocumento" runat="server"
                                            CssClass="form-control" placeholder="Recuerde ingresar sólo números" MaxLength="12" TabIndex="3"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class='form-group'>
                                        <label>* Primer Nombre</label>
                                        <asp:TextBox ID="txtPrimerNombre" runat="server"
                                            CssClass="form-control" placeholder="Recuerde ingresar sólo letras" MaxLength="12" TabIndex="4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class='form-group'>
                                        <label>Segundo Nombre</label>
                                        <asp:TextBox ID="txtSegundoNombre" runat="server"
                                            CssClass="form-control" placeholder="Sólo letras, no se puede ingresar números ni símbolos."
                                            MaxLength="100" TabIndex="5"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class='form-group'>
                                        <label>* Primer Apellido</label>
                                        <asp:TextBox ID="txtPrimerApellido" runat="server"
                                            CssClass="form-control" placeholder="Sólo letras, no se puede ingresar números ni símbolos." MaxLength="12" TabIndex="6"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class='form-group'>
                                        <label>Segundo Apellido</label>
                                        <asp:TextBox ID="txtSegundoApellido" runat="server"
                                            CssClass="form-control" placeholder="Sólo letras, no se puede ingresar números ni símbolos."
                                            MaxLength="100" onfocus="OcultarDivErrores(txtNameError);" TabIndex="7"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class='form-group'>
                                        <label>* E-mail</label>
                                        <asp:TextBox ID="TxtEmail" runat="server"
                                            CssClass="form-control" placeholder="Sólo letras, no se puede ingresar números ni símbolos."
                                            TabIndex="8"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class='form-group'>
                                        <label>* Teléfono (Celular)</label>
                                        <asp:TextBox ID="txtPhone" runat="server"
                                            CssClass="form-control" placeholder="Sólo letras, no se puede ingresar números ni símbolos."
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>* Seleccionar Banco</label>
                                        <asp:DropDownList ID="ddlBancos" class="form-control" TabIndex="10" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:ImageButton ID="BtnPagarPSE" Style="height: 60%;"
                                        ValidationGroup="ValidacionFormulario" TabIndex="11" OnClientClick="ValidarFormulario('ValidacionFormulario');" OnClick="BtnPagarPSE_Click" runat="server" ImageUrl="../Content/img/logo-pse.png" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal modal-static fade" id="processing-modal" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="text-center">
                        <img src="../Content/img/loading.gif" class="icon" />
                        <h5><span class="modal-text">Transacción en proceso, Espere por favor... </span></h5>
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
