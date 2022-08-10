using Business;
using Encryption;
using Newtonsoft.Json;
using PaymentGatewayEntities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;

namespace PaymentGateway1Cero1.Pages
{
    public partial class Payments : System.Web.UI.Page
    {
        #region Instances
        Propiedades objPropiedades = new Propiedades();
        DetalleTransaccion objDatosTransaccion = new DetalleTransaccion();
        CLPSEPayment objPSE = new CLPSEPayment();
        CLPaymentBussines objPasarela = new CLPaymentBussines();
        CLEncryption objEncryption = new CLEncryption();
        Persona objPersona = new Persona();
        ExceptionBusiness oExceptionBusiness = new ExceptionBusiness();
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PageLoad();
                }
            }
            catch (Exception ex)
            {
                catchException(ex);
            }
        }

        private void PageLoad()
        {
            if (ValidarParametros())
            {
                objDatosTransaccion = objPasarela.DetalleTransaccionPago(objDatosTransaccion.IDTransaccion);
                if (objDatosTransaccion != null)
                {
                    if (string.IsNullOrEmpty(objDatosTransaccion.Factura) == false)
                    {
                        AsignarPropiedadesDatosPago();
                        ConsultarListaBancos();
                        ValidarEstadoFactura();
                    }
                    else
                    {
                        ErrorLogTransaccion("NO SE ENCONTRO INFORMACIÓN DE LA TRANSACCIÓN");
                    }
                }
                else
                {
                    ErrorLogTransaccion("NO SE ENCONTRO INFORMACIÓN DE LA TRANSACCIÓN");
                }
            }
            else
            {
                ErrorLogTransaccion("ERROR EN EL METODO ValidarParametros()");
            }
        }

        private bool ValidarParametros()
        {
            try
            {
                PropiedadesPRM objDatosTransaccionPRM = new PropiedadesPRM();
                string param = Request.QueryString["params"].ToString();
                if (string.IsNullOrEmpty(param) == false)
                {
                    string paramsDecrypt = objEncryption.Decrypt(param);
                    NameValueCollection ParameterList = HttpUtility.ParseQueryString(paramsDecrypt);

                    foreach (String QueryString in ParameterList.AllKeys)
                    {
                        objDatosTransaccionPRM.count = objDatosTransaccionPRM.count + 1;
                        objDatosTransaccionPRM.ParameterValue = HttpUtility.UrlDecode((ParameterList[QueryString]));
                        if (PropiedadesPRM.Parameters.IsDefined(typeof(PropiedadesPRM.Parameters), QueryString))
                        {
                            if (string.IsNullOrEmpty(objDatosTransaccionPRM.ParameterValue) == false)
                            {
                                PropiedadesPRM.Parameters parameter = (PropiedadesPRM.Parameters)Enum.Parse(typeof(PropiedadesPRM.Parameters), QueryString);

                                if (PropiedadesPRM.Parameters.TransactionId == parameter)
                                {
                                    long IDTransaccion;
                                    if (Int64.TryParse(objDatosTransaccionPRM.ParameterValue, out IDTransaccion))
                                    {
                                        if (IDTransaccion > 0)
                                        {
                                            objDatosTransaccion.IDTransaccion = IDTransaccion;
                                            TransationID.Text = objDatosTransaccion.IDTransaccion.ToString();
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                catchException(ex);
                return false;
            }
            return true;
        }

        private void AsignarPropiedadesDatosPago()
        {
            LblNombreEntidad.Text = objDatosTransaccion.NombreEntidad;
            LblTramite.Text = objDatosTransaccion.Tramite;
            lblNombres.Text = string.Format("{0} {1} {2} {3}", objDatosTransaccion.PrimerNombre, objDatosTransaccion.SegundoNombre, objDatosTransaccion.PrimerApellido, objDatosTransaccion.SegundoApellido);
            lblIdentificacion.Text = objDatosTransaccion.Identificacion;
            lblValorTotalPagar.Text = string.Format("{0:N}", objDatosTransaccion.Total);
            lblFactura.Text = objDatosTransaccion.Factura;
            TxtNumeroDocumento.Text = objDatosTransaccion.Identificacion;
            TxtEmail.Text = objDatosTransaccion.EmailPagador;
            txtPhone.Text = objDatosTransaccion.TelefonoPagador;
            txtPrimerNombre.Text = objDatosTransaccion.PrimerNombre;
            txtSegundoNombre.Text = objDatosTransaccion.SegundoNombre;
            txtPrimerApellido.Text =objDatosTransaccion.PrimerApellido;
            txtSegundoApellido.Text = objDatosTransaccion.SegundoApellido;
            dllTipoDocumento.SelectedIndex = -1;
            dllTipoDocumento.Items.FindByValue(objDatosTransaccion.IDTipoDocumento.ToString()).Selected = true;
            URLRetorno.Text = objDatosTransaccion.URLRetorno;
            BtnPagarPSE.Enabled = true;
        }

        private void ConsultarListaBancos()
        {
            Dictionary<string, string> banks = objPSE.GetBankList(objDatosTransaccion.CodigoEntidad);
            if (banks.Count > 0)
            {
                ddlBancos.DataSource = banks;
                ddlBancos.DataValueField = "key";
                ddlBancos.DataTextField = "value";
                ddlBancos.DataBind();
            }
            else
            {
                ErrorLogTransaccion("ERROR NO SE OBTUVO LA LISTA DE BANCOS");
                ModalInfo(objPropiedades.Advertencia, objPropiedades.MensajeBanco, 2);
            }
        }

        public void BtnPagarPSE_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (ddlBancos.SelectedIndex > 0)
                {
                    Mostrarprogreso();
                    objDatosTransaccion = objPasarela.DetalleTransaccionPago(long.Parse(TransationID.Text));
                    if (objDatosTransaccion.IDTransaccion > 0)
                    {
                        if (ValidarEstadoFactura())
                        {
                            AsignarDatosTransaccion();
                            CrearTransaccion();
                        }
                    }
                    else
                    {
                        ModalInfo(objPropiedades.Advertencia, objPropiedades.MensajeTrx, 2);
                        objPasarela.TransactionLog(objDatosTransaccion.IDTransaccion, "NO SE OBTUVO RESPUESTA POR PARTE DE LA BASE DE DATOS");
                    }
                }
                else
                {
                    ConsultarListaBancos();
                }
            }
            catch (Exception ex)
            {
                catchException(ex);
            }
        }

        private bool ValidarEstadoFactura()
        {
            StringBuilder sb = new StringBuilder();
            string EstadoFactura = string.Empty;

            GetTransactionInformation oGetTransactionInformation = objPasarela.EstadoTransaccion(objDatosTransaccion.Factura, objDatosTransaccion.IDTramite, objDatosTransaccion.CodigoEntidad);
            if (oGetTransactionInformation != null)
            {
                if (oGetTransactionInformation.Estado.ToUpper() == DetalleTransaccion.EstadoTransaccion.APROBADA.ToString())
                {
                    EstadoFactura = DetalleTransaccion.EstadoTransaccion.APROBADA.ToString();
                }
                else if (oGetTransactionInformation.Estado.ToUpper() == DetalleTransaccion.EstadoTransaccion.PENDIENTE.ToString())
                {
                    EstadoFactura = DetalleTransaccion.EstadoTransaccion.PENDIENTE.ToString();
                }
                if (string.IsNullOrEmpty(EstadoFactura) == false)
                {
                    sb.Append("En este momento su número de Referencia o Factura ")
                    .Append(oGetTransactionInformation.Factura)
                    .Append(" ha finalizado su proceso de pago y cuya transacción se encuentra " + EstadoFactura + " en su entidad financiera. Si desea mayor")
                    .Append(" información sobre el estado de su operación puede comunicarse a nuestra lineas de atención al cliente ")
                    .Append(objDatosTransaccion.TelefonoEntidad)
                    .Append(", Fax ")
                    .Append(objDatosTransaccion.TelefonoEntidad)
                    .Append(" o enviar un correo electrónico a ")
                    .Append(objDatosTransaccion.EmailEntidad)
                    .Append(" y preguntar por el estado de la transacción: ")
                    .Append(oGetTransactionInformation.CUS);
                    objPropiedades.EstadoFactura = sb.ToString();
                    ModalInfo(objPropiedades.Advertencia, objPropiedades.EstadoFactura, 2);
                    objPasarela.TransactionLog(objDatosTransaccion.IDTransaccion, string.Format("Mensaje={0}Factura={1}EstadoFactura={2}CUS={3}", "RETURN FALSE ValidarEstadoFactura", oGetTransactionInformation.Factura, EstadoFactura, oGetTransactionInformation.CUS));
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void AsignarDatosTransaccion()
        {

            objDatosTransaccion.TypePerson = int.Parse(DllTipoPersona.SelectedValue);
            objDatosTransaccion.TipoDocumento = dllTipoDocumento.SelectedValue;
            objDatosTransaccion.Identificacion = TxtNumeroDocumento.Text;
            objDatosTransaccion.PrimerNombre = txtPrimerNombre.Text;
            objDatosTransaccion.SegundoNombre = txtSegundoNombre.Text;
            objDatosTransaccion.PrimerApellido = txtPrimerApellido.Text;
            objDatosTransaccion.SegundoApellido = txtSegundoApellido.Text;
            objDatosTransaccion.EmailPagador = TxtEmail.Text;
            objDatosTransaccion.TelefonoPagador = txtPhone.Text;
            objDatosTransaccion.CodigoBanco = int.Parse(ddlBancos.SelectedItem.Value.ToString());
            objDatosTransaccion.FechaExpedicion = DateTime.Now;
            objDatosTransaccion.IP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();

            objPersona.Identificacion = objDatosTransaccion.Identificacion;
            objPersona.TipoDocumento = int.Parse(objDatosTransaccion.TipoDocumento);
            objPersona.TipoPersona = Persona.ETipoPersona.Natural;
            objPersona.PrimerNombre = objDatosTransaccion.PrimerNombre;
            objPersona.SegundoNombre = objDatosTransaccion.SegundoNombre;
            objPersona.PrimerApellido = objDatosTransaccion.PrimerApellido;
            objPersona.SegundoApellido = objDatosTransaccion.SegundoApellido;
            objPersona.Email = objDatosTransaccion.EmailPagador;
            objPersona.Telefono = objDatosTransaccion.TelefonoPagador;
        }

        private void CrearTransaccion()
        {
            if (objPasarela.InsertPayPerson(objDatosTransaccion.IDTransaccion, objPersona))
            {
                if(objDatosTransaccion.CodigoEntidad == "8920992166")//casanare
                {
                    CreateTransactionPSE(true);
                }
                else
                {
                    CreateTransactionPSE(false);
                }
                
            }
            else
            {
                ModalInfo(objPropiedades.Advertencia, objPropiedades.MensajeTrx, 2);
                objPasarela.TransactionLog(objDatosTransaccion.IDTransaccion, string.Concat("ERROR EN EL METODO InsertPayPerson() ", "DATA ", JsonConvert.SerializeObject(objPersona)));
            }
        }

        private void CreateTransactionPSE(bool multicredit)
        {
            ResponseCreateTransaction objResponseTransaction = objPSE.CreateTransaction(objDatosTransaccion, multicredit);
            objPasarela.TransactionLog(objDatosTransaccion.IDTransaccion, string.Concat(" ERROR EN EL METODO CreateTransaction()  ", "DATA ", JsonConvert.SerializeObject(objDatosTransaccion), " RESPUESTA ACH ", JsonConvert.SerializeObject(objResponseTransaction)));
            //lblNombres.Text = objResponseTransaction.Message;
            if (objResponseTransaction.StateTransaction == true)
            {
                objDatosTransaccion.TransactionStatus = DetalleTransaccion.EstadoTransaccion.PENDIENTE;
                objDatosTransaccion.CUS = objResponseTransaction.TransactionNumberPSE;
                if (objPasarela.ActualizarEstadoTransaccionPendiente(objDatosTransaccion))
                {
                    Session["IDTrans"] = 1;
                    RedirectBanco(objResponseTransaction.Bankurl);
                }
                else
                {
                    ModalInfo(objPropiedades.Advertencia, objPropiedades.MensajeTrx, 2);
                    objPasarela.TransactionLog(objDatosTransaccion.IDTransaccion, string.Concat("ERROR EN EL METODO ActualizarInicioTransaccion() ", "DATA ", JsonConvert.SerializeObject(objDatosTransaccion)));
                }
            }
            else
            {
                ModalInfo(objPropiedades.Advertencia, string.Format(objResponseTransaction.Message,
                    objDatosTransaccion.NombreEntidad,
                    objDatosTransaccion.TelefonoEntidad,
                    objDatosTransaccion.EmailEntidad), 2);
                objPasarela.TransactionLog(objDatosTransaccion.IDTransaccion, string.Concat(" ERROR EN EL METODO CreateTransaction()  ", "DATA ", JsonConvert.SerializeObject(objDatosTransaccion), " RESPUESTA ACH ", JsonConvert.SerializeObject(objResponseTransaction)));
            }
        }

     

        #region Methods


        private void ModalInfo(string Titulo, string Mensaje, int Tipo)
        {

            Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "MostrarMensaje", string.Format("<script type='text/javascript'>MostrarMensaje('{0}','{1}','{2}');</script>", Titulo, Mensaje, Tipo));
        }

        private void Mostrarprogreso()
        {
            Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "Mostrarprogreso", string.Format("<script type='text/javascript'>Mostrarprogreso();</script>", new object[0]));
        }

        private void RedirectBanco(string Url)
        {
            BtnPagarPSE.Visible = false;
            BtnPagarPSE.Enabled = false;
            Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "HideModal", string.Format("<script type='text/javascript'>HideModal('{0}');</script>", Url));
        }

        private void catchException(Exception ex)
        {
            if (objPropiedades.IDTransaccion > 0)
            {
                objPasarela.TransactionLog(objPropiedades.IDTransaccion, JsonConvert.SerializeObject(ex));
            }
            oExceptionBusiness.InsetLogException(ex);
            ModalInfo(objPropiedades.Advertencia, objPropiedades.MensajeTrx, 2);
        }

        private void ErrorLogTransaccion(string Mensaje)
        {
            objPasarela.TransactionLog(objDatosTransaccion.IDTransaccion, Mensaje);
            Redirect404();
        }
        private void Redirect404()
        {
            Response.Redirect("~/ErrorPages/Oops.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        #endregion

        protected void btnHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DetailTransactions.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                catchException(ex);
            }
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(URLRetorno.Text, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                catchException(ex);
            }
        }
    }
}