using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;

namespace OfficelinerMobileWebService
{
    public class ODBConnection
    {
        public String ConnectionString {get;set;}
        public String DataSource { get; set; }
        public String UserID { get; set; }
        public String Password { get; set; }


        private OracleConnection _connection;
        public OracleConnection Connection
        {
            get
            {
                return this._connection;
            }
            set
            {
                this._connection = value;
            }
        }

        public ODBConnection(string dataSource, string userID, string password) {
            
            //init tasks
            ConnectionString = String.Empty;
            DataSource = dataSource;
            UserID = userID;
            Password = password;

            if (DataSource.Length > 0 && UserID.Length > 0 && Password.Length > 0)
            {
                //build the connectionString for the Database connection
                ConnectionString = String.Format("Data Source={0};Persist Security Info=True; Password={1}; User ID={2}", DataSource, Password, UserID);
            }
            
            //connection
            _connection = new OracleConnection(ConnectionString);
        }
    }
}