using Newtonsoft.Json;
using PaymentLogException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ExceptionBusiness
    {
        public void InsetLogException(Exception ex)
        {
            LogException oLogException = new LogException();
            oLogException.InsertLogException(JsonConvert.SerializeObject(ex), LogException.Project.PaymentGateway1Cero1);
        }
    }
}
