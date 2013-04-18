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

        public ODBConnection(string dataSource ="olxsrv", string userID ="fabian", string password = "fabian") {
            
            //init tasks
            ConnectionString = String.Empty;
            DataSource = dataSource;
            UserID = userID;
            Password = password;

            //build the connectionString for the Database connection
            ConnectionString = String.Format("Data Source={0};Persist Security Info=True; Password={1}; User ID={2}", DataSource, Password, UserID);
            
            //connection
            _connection = new OracleConnection(ConnectionString);
        }

        //If the connection could be open, the function returns true, else the function returns false
        public bool openConnection()
        {
            try
            {
                _connection.Open();
                return true;
            }

            catch (Exception e)
            {
                return false;
            }

        }

        //Return true, if the connection was closed without a exception
        public bool closeConnection()
        {
            try
            {
                _connection.Close();
                _connection.Dispose();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

       
    }
}