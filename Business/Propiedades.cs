using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Propiedades
    {
        public long IDTransaccion { get; set; }

        public string Http { get; set; }

        public string EstadoFactura { get; set; }

        public string Informacion
        {
            get { return "Información"; }
        }

        public string Advertencia
        {
            get
            {
                return "Advertencia";
            }
        }

        public string MensajeBanco
        {
            get
            {
                return "No se pudo obtener la lista de bancos, por favor intente más tarde";
            }
        }

        public string MensajeTrx
        {
            get
            {
                return "No se pudo crear la transacción, por favor intente más tarde o comuníquese con la entidad.";
            }
        }

        public string MensajeR
        {
            get
            {
                return "En este momento no se puede visualizar el detalle de la transacción, por favor intente más tarde o comuníquese con la entidad.";
            }
        }


        string[] _msg = {
            "No se pudo crear la transacción, por favor intente ",
            "más tarde o comuníquese con la entidad. Nuestras líneas de atención al cliente al teléfono ",
            " o enviarnos sus inquietudes o sugerencias al correo " };


        public string[] messages
        {
            get { return _msg; }
        }

        public static string GetConfiguration(string key)
        {
            AppSettingsReader appSettingsReader = new AppSettingsReader();
            return appSettingsReader.GetValue(key, typeof(string)).ToString();
        }
    }
}
