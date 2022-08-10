using PaymentGatewayEntities;
using PSEGatewayBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CLPSEPayment
    {
        PSEPaymentBussines objPSEPaymentBussines = new PSEPaymentBussines();

        public Dictionary<string, string> GetBankList(string CodigoMunicipio)
        {
            return objPSEPaymentBussines.GetBankList(CodigoMunicipio);
        }

        public ResponseCreateTransaction CreateTransaction(DetalleTransaccion objDetalleTransaccion, bool multicredit)
        {
            ResponseCreateTransaction objresponse = new ResponseCreateTransaction();
            ResponseTransaction objResponseTransaction = new ResponseTransaction();
            TransactionInformation objTransactionInformation = AsignarPropiedades(objDetalleTransaccion);
            if (multicredit == true)
            {
                objResponseTransaction = objPSEPaymentBussines.CreateTransactionMultiCredit(objTransactionInformation);
            }
            else
            {
                objResponseTransaction = objPSEPaymentBussines.CreateTransaction(objTransactionInformation);
            }
            objresponse.ReturnCode = objResponseTransaction.ReturnCode;
            objresponse.StateTransaction = objResponseTransaction.StateTransaction;
            objresponse.TransactionNumberPSE = objResponseTransaction.TransactionNumberPSE;
            objresponse.Bankurl = objResponseTransaction.Bankurl;
            objresponse.Message = objResponseTransaction.Message;

            return objresponse;

        }

        TransactionInformation AsignarPropiedades(DetalleTransaccion objDetalleTransaccion)
        {
            TransactionInformation objTransactionInformation = new TransactionInformation();

            objTransactionInformation.CodigoMunicipio = objDetalleTransaccion.CodigoEntidad;
            objTransactionInformation.ServiceCode = objDetalleTransaccion.CodigoServicio;
            objTransactionInformation.CodigoPadre = objDetalleTransaccion.CodigoPadre;
            objTransactionInformation.CuentaBancoMun = string.Empty;
            objTransactionInformation.CodBanco = objDetalleTransaccion.CodigoBanco.ToString();
            objTransactionInformation.TransactionValue = objDetalleTransaccion.Total;
            objTransactionInformation.Factura = objDetalleTransaccion.Factura;
            objTransactionInformation.TransactionID = objDetalleTransaccion.IDTransaccion;
            objTransactionInformation.TypePerson = objDetalleTransaccion.TypePerson;
            objTransactionInformation.IdentificacionPagador = objDetalleTransaccion.Identificacion;
            objTransactionInformation.Referencia = objDetalleTransaccion.Referencia;
            objTransactionInformation.NombreTramite = objDetalleTransaccion.Tramite;
            objTransactionInformation.ListImpuestosMultiCredito = ListImpuestosMultiCredito(objDetalleTransaccion);
            return objTransactionInformation;
        }

        private List<ImpuestosMultiCredito> ListImpuestosMultiCredito(DetalleTransaccion objDetalleTransaccion)
        {
            List<ImpuestosMultiCredito> ListData = new List<ImpuestosMultiCredito>();
            ListData.Add(new ImpuestosMultiCredito() { serviceCode = objDetalleTransaccion.CodigoServicio1, companyIdentification = objDetalleTransaccion.CodigoEntidad, transactionValue = objDetalleTransaccion.TotalCuenta1, vatValue = "0" });
            ListData.Add(new ImpuestosMultiCredito() { serviceCode = objDetalleTransaccion.CodigoServicio2, companyIdentification = objDetalleTransaccion.CodigoEntidad, transactionValue = objDetalleTransaccion.TotalCuenta2, vatValue = "0" });
            ListData.Add(new ImpuestosMultiCredito() { serviceCode = objDetalleTransaccion.CodigoServicio3, companyIdentification = objDetalleTransaccion.CodigoEntidad, transactionValue = objDetalleTransaccion.TotalCuenta3, vatValue = "0" });
            ListData.Add(new ImpuestosMultiCredito() { serviceCode = objDetalleTransaccion.CodigoServicio4, companyIdentification = objDetalleTransaccion.CodigoEntidad, transactionValue = objDetalleTransaccion.TotalCuenta4, vatValue = "0" });

            return ListData;

        }

        public ResponseTransactionInformacion GetTransactionInformation(string CodigoMunicipio, string CUS, string ServiceCode)
        {
            ResponseTransactionInformacion objResponseTransaction = new ResponseTransactionInformacion();
            ResponseGetTransactionInformation objresponse = objPSEPaymentBussines.GetTransactionInformation(CodigoMunicipio, CUS, ServiceCode, "123456789");

            if (objresponse.Status > 0)
            {
                objResponseTransaction.TransactionStatus = objresponse.Status.ToString();
                objResponseTransaction.returnCode = objresponse.returnCode;
                objResponseTransaction.transactionState = objresponse.transactionState;
                objResponseTransaction.transactionCycle = int.Parse(objresponse.transactionCycle);
                objResponseTransaction.trazabilityCode = objresponse.trazabilityCode;
                objResponseTransaction.transactionValue = objresponse.transactionValue;
                objResponseTransaction.ticketId = objresponse.ticketId;
                objResponseTransaction.soliciteDate = objresponse.soliciteDate;
                objResponseTransaction.bankProcessDate = objresponse.bankProcessDate;
            }

            return objResponseTransaction;
        }
    }
}
