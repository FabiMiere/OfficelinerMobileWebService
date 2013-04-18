using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using ServiceStack.ServiceInterface.ServiceModel;

namespace OfficelinerMobileWebService
{
    public class Login
    {
        public String DataSource { get; set; }
        public String UserID { get; set; }
        public String Password { get; set; }

        public LoginResponse getConnection(Login request)
        {
            ODBConnection odbcon = new ODBConnection(request.DataSource, request.UserID, request.Password);
            try
            {
                odbcon.Connection.Open();
                return new LoginResponse {
                                            Result = "Verbindung erfolgreich",
                                            OraConnection = odbcon.Connection
                                         };
            }
            catch (Exception e)
            {
                return new LoginResponse { Result = e.ToString() };
            }

        }
    }

    public class LoginResponse
    {
        public String Result { get; set; }
        public OracleConnection OraConnection { get; set; }
    }
}