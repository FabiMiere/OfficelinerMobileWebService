using System;
using System.Data;
using Funq;
using Oracle.DataAccess.Client;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace OfficelinerMobileWebService
{

    

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize your application
            (new AppHost()).Init();
        }
    }
}
