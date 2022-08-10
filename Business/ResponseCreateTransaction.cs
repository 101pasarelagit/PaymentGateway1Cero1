using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ResponseCreateTransaction
    {
        public string ReturnCode { get; set; }
        public bool StateTransaction { get; set; }
        public string TransactionNumberPSE { get; set; }
        public string Bankurl { get; set; }

        public string Message { get; set; }
    }
}

