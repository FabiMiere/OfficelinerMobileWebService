using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Funq;
using ServiceStack.Api.Swagger;
using ServiceStack.WebHost.Endpoints;

namespace OfficelinerMobileWebService
{

    /// <summary>
    /// Create your ServiceStack web service application with a singleton AppHost.
    /// </summary>        
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
        /// </summary>
        public AppHost() : base("Hello Web Services", typeof(OfficelinerMobileService).Assembly) { }

        /// <summary>
        /// Configure the container with the necessary routes for your ServiceStack application.
        /// </summary>
        /// <param name="container">The built-in IoC used with ServiceStack.</param>
        public override void Configure(Container container)
        {

            //For Swagger Plugin
            Plugins.Add(new SwaggerFeature());

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

              .Add<BusinessPartnerMethods>("/BP/GetAll")

              //Andere Möglichkeit Routing -> Hier wird alles was nach Login kommt in das Attribut DataSource geschrieben, der String kann dann selber geteilt werden
                //Mit dieser Möglichkeit landet der gesamte String im Attribut DataSource, dieser kann anschließend selbst geteilt werden

              /*
               * Beispielanwendung, die diesen Service verwendet:
               * OfficelinerMobile LoginScreen
               * Benutzer trägt seine Daten ein, diese Daten in Kombination mit dem Connectionstring stellen eine Verbindung zur Datenbank her!
               * Die Datenbank liefert eine SessionId zurück, diese Id wird Global gespeichert und für alle weitere WS aufrufe verwendet.
               * 
               * */
             //.Add<Login>("/Login/{DataSource*}")
            .Add<SKR04>("/SKR04");

            //For Swagger API
            //Routes.IgnoreRoute("api/{*pathInfo}"); 

        }
    }


}