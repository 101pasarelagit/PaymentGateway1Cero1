using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PropiedadesPRM
    {
        public long IDTransaccion { get; set; }
        public string Http { get; set; }
        public string ParameterValue { get; set; }
        public int count { get; set; }
        public enum Parameters { TransactionId };
    }

}
