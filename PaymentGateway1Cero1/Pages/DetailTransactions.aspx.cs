using Business;
using PaymentGatewayEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PaymentGateway1Cero1.Pages
{
    public partial class DetailTransactions : System.Web.UI.Page
    {
        DetalleTransaccion objPropiedades = new DetalleTransaccion();
        Propiedades objPropiedadesTax = new Propiedades();
        ExceptionBusiness oExceptionBusiness = new ExceptionBusiness();
        public string CodigoMunicipio { get; set; }

        protected void Page_Load(object sender, System.EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            {
                string Identificacion = txtIdentificacion.Text;
                if (string.IsNullOrEmpty(Identificacion) == false)
                {
                    ListTransacciones(Identificacion);
                }
            }
            catch (Exception ex)
            {
                catchException(ex);
            }
        }

        private void ListTransacciones(string Identificacion)
        {
            CLPaymentBussines objPasarela = new CLPaymentBussines();

            List<TransaccionesCiudadano> List = objPasarela.TransaccionesCiudadano(Identificacion);

            if (List.Count > 0)
            {
                dvContent.Visible = true;
                GridTransac.DataSource = List;
                GridTransac.DataBind();
                GridTransac.HeaderRow.TableSection = System.Web.UI.WebControls.TableRowSection.TableHeader;
            }
            else
            {
                dvContent.Visible = false;
                GridTransac.DataSource = null;
                GridTransac.DataBind();
                ModalInfo(objPropiedadesTax.Advertencia, "El número de identificación ingresado no tiene transacciones en línea", 2);
            }
        }

        public void catchException(Exception ex)
        {
            Propiedades objpropiedades = new Propiedades();
            oExceptionBusiness.InsetLogException(ex);
            ModalInfo(objPropiedadesTax.Advertencia, objPropiedadesTax.MensajeTrx, 2);
        }

        #region Metodos

        protected void ModalInfo(string Titulo, string Mensaje, int Tipo)
        {
            Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "MostrarMensaje", string.Format("<script type='text/javascript'>MostrarMensaje('{0}','{1}','{2}');</script>", Titulo, Mensaje, Tipo));
        }

        #endregion

    }
}
