using PaymentGatewayEntities;
using PaymentTransactionNotification.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class NotificarTransaccion
    {
        public void Notificar(DetalleTransaccion oDetalleTransaccion)
        {
            ResponseState objresponse = new ResponseState();
            Notification objnotification = new Notification(oDetalleTransaccion);
            objresponse = objnotification.Notificar();
        }
    }
}
