using Business;
using Newtonsoft.Json;
using PaymentGatewayEntities;
using SelectPdf;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.UI;

namespace PaymentGateway1Cero1.Pages
{
    public partial class ResponsePayment : System.Web.UI.Page
    {
        Propiedades objPropiedades = new Propiedades();
        DetalleTransaccion objDetalleTransaccion = new DetalleTransaccion();
        CLPaymentBussines objPasarela = new CLPaymentBussines();
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        ExceptionBusiness oExceptionBusiness = new ExceptionBusiness();
        protected void Page_Load(object sender, EventArgs e)
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
                if (ValidarSession())
                {
                    objDetalleTransaccion = objPasarela.DetalleTransaccionPago(objPropiedades.IDTransaccion);
                    objDetalleTransaccion.Project = "PaymentGateway1Cero1";
                    if (objDetalleTransaccion.IDTransaccion > 0)
                    {
                        if (GetTransactionInformationPSE())
                        {
                            if(ActualizarEstadoTransaccionDefinitiva())
                            {
                                NotificarTransaccion();
                                PropiedadesDetallePago();
                            }
                            else
                            {
                                catchException(new Exception(string.Concat(objDetalleTransaccion.IDTransaccion + " - EROR AL ACTUALIZAR LA TRANSACCION EN ESTADO DEFINITIVO " +  JsonConvert.SerializeObject(objDetalleTransaccion))));
                            }
                        }
                        else
                        {
                            catchException(new Exception(string.Concat(objDetalleTransaccion.IDTransaccion + " - NO SE OBTUVO RESPUESTA POR PARTE DE ACH ")));
                        }                  
                    }
                    else
                    {
                        oExceptionBusiness.InsetLogException(new Exception("objDetalleTransaccion.IDTransaccion is null"));
                    }
                }
                else
                {
                    oExceptionBusiness.InsetLogException(new Exception("ValidarSession is false"));
                    Redirect404();
                }
            }
            else
            {
                oExceptionBusiness.InsetLogException(new Exception("ValidarParametros is false"));
                Redirect404();
            }
        }

        private bool ValidarSession()
        {
            //if (Session["IDTrans"] == null)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            return true;
        }

        private bool ValidarParametros()
        {
            if (Request.QueryString["ticketID"] != null)
            {
                string param = Request.QueryString["ticketID"].ToString();

                if (string.IsNullOrEmpty(param))
                {
                    return false;
                }
                else
                {
                    long IDTransaccion;
                    if (Int64.TryParse(param, out IDTransaccion))
                    {
                        objPropiedades.IDTransaccion = IDTransaccion;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        protected bool GetTransactionInformationPSE()
        {

            CLPSEPayment objPSEPaymentBussines = new CLPSEPayment();
            ResponseTransactionInformacion objResponseTransaction = objPSEPaymentBussines.GetTransactionInformation(objDetalleTransaccion.CodigoEntidad, objDetalleTransaccion.CUS, objDetalleTransaccion.CodigoServicio);
            objDetalleTransaccion.TransactionStatus = (DetalleTransaccion.EstadoTransaccion)Enum.Parse(typeof(DetalleTransaccion.EstadoTransaccion), objResponseTransaction.TransactionStatus.ToString());
            objDetalleTransaccion.ResponseReturnCode = objResponseTransaction.returnCode;
            objDetalleTransaccion.ResponseTransactionStateCode = objResponseTransaction.transactionState;
            objDetalleTransaccion.CicloTransaccion = objResponseTransaction.transactionCycle;
            objDetalleTransaccion.CUS = objResponseTransaction.trazabilityCode;
            objDetalleTransaccion.Total = objResponseTransaction.transactionValue;
            objDetalleTransaccion.Factura = objResponseTransaction.ticketId;
            objDetalleTransaccion.FechaTransaccion = objResponseTransaction.soliciteDate;
            objDetalleTransaccion.FechaRespuestaBanco = objResponseTransaction.bankProcessDate;

            if (objDetalleTransaccion.TransactionStatus > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void PropiedadesDetallePago()
        {
            if (objDetalleTransaccion.TransactionStatus == DetalleTransaccion.EstadoTransaccion.PENDIENTE)
            {
                ModalInfo(objPropiedades.Advertencia, "Por favor verificar si el débito fue realizado en el Banco", 2);
            }
            lblNombreEntidad.Text = objDetalleTransaccion.NombreEntidad;
            lblEntidad.Text = objDetalleTransaccion.NombreEntidad;
            LblCodigoMunicipio.Text = objDetalleTransaccion.CodigoEntidad;
            LblNit.Text = objDetalleTransaccion.CodigoEntidad;
            LblTelefono.Text = objDetalleTransaccion.TelefonoEntidad;
            LblNombre.Text = string.Format("{0} {1} {2} {3}", objDetalleTransaccion.PrimerNombre, objDetalleTransaccion.SegundoNombre, objDetalleTransaccion.PrimerApellido, objDetalleTransaccion.SegundoApellido);
            LblIdent.Text = objDetalleTransaccion.Identificacion;
            LblPhone.Text = objDetalleTransaccion.TelefonoPagador;
            LblEmail.Text = objDetalleTransaccion.EmailPagador;
            LblIP.Text = objDetalleTransaccion.IP;
            LblTransactionState.Text = objDetalleTransaccion.TransactionStatus.ToString();
            LbltrazabilityCode.Text = objDetalleTransaccion.CUS;
            LblTransactionValue.Text = objDetalleTransaccion.Total.ToString("C", nfi);
            LblTickedID.Text = objDetalleTransaccion.Factura.PadLeft(8, '0');
            LblSolicitDate.Text = objDetalleTransaccion.FechaExpedicion.ToString();
            LblBankProcessingDate.Text = objDetalleTransaccion.FechaRespuestaBanco.ToString();
            LblTransactionCycle.Text = objDetalleTransaccion.CicloTransaccion.ToString();
            LblVATAmount.Text = "0";
            lblBanco.Text = objDetalleTransaccion.Banco;
            lbldescripcion.Text = objDetalleTransaccion.Tramite;
            URLRetorno.Text = objDetalleTransaccion.URLRetorno;
            Session["IDTrans"] = null;
        }

        protected bool ActualizarEstadoTransaccionDefinitiva()
        {

            return objPasarela.ActualizarEstadoTransaccionDefinitiva(objDetalleTransaccion);
        }
        protected void NotificarTransaccion()
        {

            NotificarTransaccion oNotificarTransaccion = new NotificarTransaccion();
            oNotificarTransaccion.Notificar(objDetalleTransaccion);
        }

        private void ComprobanteTransaccionPDF()
        {
            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), "A4", true);
            PdfPageOrientation pdfOrientation = (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation), "Portrait", true);
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
            PdfDocument doc = converter.ConvertHtmlString(CuerpoCorreoDetalleTransaccion(), "");
            doc.Save(Response, true, "DetalleTransaccion.pdf");
            doc.Close();
        }

        protected string CuerpoCorreoDetalleTransaccion()
        {

            string body = string.Empty;
            string archivo = Server.MapPath("/") + "\\PaymentGateway\\Content\\template\\ComprobanteTransaccion.html";
            FileStream Archivo = System.IO.File.Open(archivo, FileMode.Open);
            using (StreamReader reader = new StreamReader(Archivo))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{NombreEntidad}", lblNombreEntidad.Text.ToUpper());
            body = body.Replace("{EstadoTransaccion}", LblTransactionState.Text);
            body = body.Replace("{CUS}", LbltrazabilityCode.Text);
            body = body.Replace("{Banco}", lblBanco.Text);
            body = body.Replace("{Descripcion}", lbldescripcion.Text);
            body = body.Replace("{ValorTransaccion}", LblTransactionValue.Text);
            body = body.Replace("{NumeroFactura}", LblTickedID.Text);
            body = body.Replace("{FechaSolicitud}", LblSolicitDate.Text);
            body = body.Replace("{FechaRespuesta}", LblBankProcessingDate.Text);
            body = body.Replace("{CicloTransaccion}", LblTransactionCycle.Text);
            body = body.Replace("{ValorIva}", LblVATAmount.Text);
            body = body.Replace("{Nombre}", LblNombre.Text);
            body = body.Replace("{Identificacion}", LblIdent.Text);
            body = body.Replace("{Telefono}", LblPhone.Text);
            body = body.Replace("{Email}", LblEmail.Text);
            body = body.Replace("{IP}", LblIP.Text);
            body = body.Replace("{Nit}", LblNit.Text);
            body = body.Replace("{TelefonoMunicipio}", LblTelefono.Text);

            return body;

        }

        #region Methods

        protected void ModalInfo(string Titulo, string Mensaje, int Tipo)
        {
            Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "MostrarMensaje",
            string.Format("<script type='text/javascript'>MostrarMensaje('{0}','{1}','{2}');</script>",
            Titulo, Mensaje, Tipo));
        }

        private void catchException(Exception ex)
        {
            if (objPropiedades.IDTransaccion > 0)
            {
                objPasarela.TransactionLog(objPropiedades.IDTransaccion, JsonConvert.SerializeObject(ex));
            }
            oExceptionBusiness.InsetLogException(ex);
        }


        protected void btnComprobante_Click(object sender, EventArgs e)
        {
            try
            {
                ComprobanteTransaccionPDF();
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
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

        protected void btnHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DetailTransactions.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                catchException(ex); ;
            }
        }

        private void Redirect404()
        {
            Response.Redirect("~/ErrorPages/Oops.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        #endregion
    }
}