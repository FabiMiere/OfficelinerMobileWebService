using System;
using System.Data;
using Funq;
using Oracle.DataAccess.Client;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace OfficelinerMobileWebService
{

    /// <summary>
    /// Create your ServiceStack web service implementation.
    /// </summary>
    public class HelloService : IService
    {
        public object Any(Hello request)
        {
            //Looks strange when the name is null so we replace with a generic name.
            var name = request.Name ?? "John Doe";
            return new HelloResponse { Result = "Hello, " + name };
        }

        public object Any(Login request)
        {

            //Insert a delegate here, to a other class maybe some actions with the request String here or delegate these action to a helper
            //return a value here

            /* Fragen Boris
             * 
             * Wie erstelle ich den Pfad? ServiceStack.Hello ersetzen
             * Wie komme ich an die verschiedene Rückgabewerte? Also die returns und wie greife ich auf die verschiedene Formate zu?
             * Best practice für ein Login, wie gestalte ich Klassen etc...
             * 
             * Jochen:
             * OfficelinerWS verwenden und eine SessionID bekommen
             * 
             * */
            

            String value = "";

            if (request.DataSource.Length > 0 && request.UserID.Length > 0 && request.Password.Length > 0)
            {
                ODBConnection odbcon = new ODBConnection(request.DataSource, request.UserID, request.Password);
                odbcon.openConnection();
                return "erfolgreich angemeldet";

                //ToDo This function need a seperate class with the functions for the webservice and the mapping vom the db results to a c# object
                //Checks if the connection could be open or a connection already exists
                if (odbcon.openConnection() || odbcon.Connection != null)
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = odbcon.Connection;
                    cmd.CommandText = "select * from ol_fibu_skr04";
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        value = String.Format("<p>" + value + dr.GetOracleString(0).ToString() + ", " + dr.GetOracleString(1).ToString() + "</p>");
                    }
                    odbcon.closeConnection();
                    return value;
                }
                else
                {
                    return value = "connection could not open";
                }


            }
            else { 
            return "Login nicht erfolgreich";
            }
            


        }
    }

    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Create your ServiceStack web service application with a singleton AppHost.
        /// </summary>        
        public class HelloAppHost : AppHostBase
        {
            /// <summary>
            /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
            /// </summary>
            public HelloAppHost() : base("Hello Web Services", typeof(HelloService).Assembly) { }

            /// <summary>
            /// Configure the container with the necessary routes for your ServiceStack application.
            /// </summary>
            /// <param name="container">The built-in IoC used with ServiceStack.</param>
            public override void Configure(Container container)
            {
                //Register user-defined REST-ful urls. You can access the service at the url similar to the following.
                //http://localhost/ServiceStack.Hello/servicestack/hello or http://localhost/ServiceStack.Hello/servicestack/hello/John%20Doe
                //You can change /servicestack/ to a custom path in the web.config.
                Routes
                    //Server/Hello
                  .Add<Hello>("/hello")
                    //Server/Hello inklusive attribut
                  .Add<Hello>("/hello/{Name}")

                  //Ruft Login Klasse und übergibt die Werte an die Klasse! Die Attribute müssen mit den der Klassen übereinstimmen
                  .Add<Login>("/Login/{DataSource}/{UserID}/{Password}")

                  //Andere Möglichkeit Routing -> Hier wird alles was nach Login kommt in das Attribut DataSource geschrieben, der String kann dann selber geteilt werden
                  //Mit dieser Möglichkeit landet der gesamte String im Attribut DataSource, dieser kann anschließend selbst geteilt werden

                  /*
                   * Beispielanwendung, die diesen Service verwendet:
                   * OfficelinerMobile LoginScreen
                   * Benutzer trägt seine Daten ein, diese Daten in Kombination mit dem Connectionstring stellen eine Verbindung zur Datenbank her!
                   * Die Datenbank liefert eine SessionId zurück, diese Id wird Global gespeichert und für alle weitere WS aufrufe verwendet.
                   * 
                   * */
                 .Add<Login>("/Login/{DataSource*}");
                  
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            //Initialize your application
            (new HelloAppHost()).Init();
        }
    }
}
