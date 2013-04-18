using System;
using System.Collections.Generic;
using ServiceStack.ServiceHost;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;

namespace OfficelinerMobileWebService
{
    /// <summary>
    /// Create your ServiceStack web service implementation.
    /// </summary>
    public class OfficelinerMobileService : IService
    {

        public OracleConnection OraConnection { get; set; }


        public object Any(Hello request)
        {
            //Looks strange when the name is null so we replace with a generic name.
            var name = request.Name ?? "John Doe";
            return new HelloResponse { Result = "Hello, " + name };
        }


        //Login only build a connection to the server and get a Session ID
        public object Any(Login request)
        {
            //Insert a delegate here, to a other class maybe some actions with the request String here or delegate these action to a helper
            //return a value here

            /* Fragen Boris
             * 
             * Wie komme ich an die verschiedene Rückgabewerte? Also die returns und wie greife ich auf die verschiedene Formate zu?
             * Best practice für ein Login, wie gestalte ich Klassen etc...
             * Publish des WebService
             * 
             * Jochen:
             * OfficelinerWS verwenden und eine SessionID bekommen
             * 
             * */
            LoginResponse response = new LoginResponse();
            response = request.getConnection(request);
            OraConnection = response.OraConnection;

            //ToDo raus hier!
            SKR04 skr04request = new SKR04();

            //ToDO store the connection in a value and use this value after the login
            SKR04Response SKR04response = new SKR04Response();
            if (OraConnection != null)
            {
                SKR04response.Result = skr04request.GetSkr04(OraConnection);

                String output = "";

                foreach (var i in SKR04response.Result)
                {
                    output = output + i.ToString();
                }

                return output;
                //return SKR04response.Result;
                //Wie gehts weiter?! Wieso gibt er mir ein leeres Array zurück?!

            }
            else
            {
                return "Please Login before you request";
            }



            //return response.Result;
            

        }

        //Get the SKR04
        public object Any(SKR04 request)
        {
            SKR04Response response = new SKR04Response();
            if (OraConnection != null)
            {
                response.Result = request.GetSkr04(OraConnection);
                return response.Result;
            }
            else
            {
                return "Please Login before you request";
            }
        }
    }
}