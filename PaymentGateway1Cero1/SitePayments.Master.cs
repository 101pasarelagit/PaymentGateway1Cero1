using System;

namespace PaymentGateway1Cero1
{
    public partial class SitePayments : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    AddPropertiesForm();
                }
            }
            catch
            {

            }
        }

        public void Timer1_Tick(object sender, System.EventArgs e)
        {
            DateTime MyDateTime = DateTime.Now;
            LblFecha.Text = MyDateTime.ToString("MMMM dd, yyyy h:mm:ss tt").ToLower();
            LblFecha.Text = LblFecha.Text.Substring(0, 1).ToUpper() + LblFecha.Text.Substring(1, LblFecha.Text.Length - 1);
        }

        private void AddPropertiesForm()
        {

            System.Web.HttpContext current = System.Web.HttpContext.Current;
            string text = current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string text2;
            if (string.IsNullOrEmpty(text) == false)
            {
                string[] array = text.Split(new char[]
                {
                        ','
                });
                if (array.Length != 0)
                {
                    text2 = array[0];
                }
            }
            text2 = current.Request.ServerVariables["REMOTE_ADDR"];
            lblIP.Text = text2;

        }
    }
}