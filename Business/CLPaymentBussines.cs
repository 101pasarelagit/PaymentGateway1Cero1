using PaymentGatewayBusiness;
using PaymentGatewayEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business
{

    public class CLPaymentBussines
    {

        TransaccionBusiness objTransaccionBusiness = new TransaccionBusiness();

        public DetalleTransaccion DetalleTransaccionPago(long IDTransation)
        {
            return objTransaccionBusiness.ConsultaDetalleTransaccion(IDTransation);
        }

        public bool InsertPayPerson(long IDTransaccion, Persona objPersona)
        {
            return objTransaccionBusiness.InsertPayPerson(IDTransaccion, objPersona);
        }

        public bool ActualizarEstadoTransaccionPendiente(DetalleTransaccion objDatosTransaccion)
        {

            return objTransaccionBusiness.ActualizarEstadoTransaccionPendiente(
                objDatosTransaccion.IDTransaccion,
                objDatosTransaccion.CodigoBanco,
                objDatosTransaccion.CUS,
                objDatosTransaccion.IP
                );
        }

        public bool ActualizarEstadoTransaccionDefinitiva(DetalleTransaccion objDatosTransaccion)
        {
            return objTransaccionBusiness.ActualizarEstadoTransaccionDefinitiva(
                objDatosTransaccion.IDTransaccion,
                objDatosTransaccion.CicloTransaccion,
                objDatosTransaccion.TransactionStatus,
                objDatosTransaccion.FechaRespuestaBanco
                );
        }

        public GetTransactionInformation EstadoTransaccion(string Factura, int IDTramite, string CodigoEntidad)
        {
            return objTransaccionBusiness.EstadoTransaccion(Factura, IDTramite, CodigoEntidad);
        }

        public List<TransaccionesCiudadano> TransaccionesCiudadano(string Identificacion)
        {
            return objTransaccionBusiness.TransaccionesCiudadano(Identificacion);
        }

        public void TransactionLog(long IDTransaccion, string Log)
        {
            objTransaccionBusiness.TransactionLog(IDTransaccion, Log);
        } 
    }
}
