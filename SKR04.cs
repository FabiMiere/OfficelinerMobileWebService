using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace OfficelinerMobileWebService
{
    public class SKR04
    {
        String ACCNO;
        String Description;


        public List<SKR04> GetSkr04(OracleConnection connection)
        {
        OracleCommand cmd = new OracleCommand();
        SKR04Response skr04response = new SKR04Response();
        skr04response.Result = new List<SKR04>();
        

        cmd.Connection = connection;
        cmd.CommandText = "select * from ol_fibu_skr04";
        cmd.CommandType = CommandType.Text;
        OracleDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            SKR04 value = new SKR04();
            value.ACCNO = dr.GetOracleString(0).ToString();
            value.Description = dr.GetOracleString(1).ToString();
            skr04response.Result.Add(value);
        }

        return skr04response.Result;
        }
    }

    public class SKR04Response
    {
        public List<SKR04> Result;
    }

    
}