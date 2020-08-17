using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.IO;

namespace TRAVELMART.Common
{
    public class ConnectionSetting
    {

        public static Database GetConnection()
        {
            Database SFDatebase = DatabaseFactory.CreateDatabase(); // //null;
            try
            {

                string path = ConfigurationSettings.AppSettings["FileLocation"].ToString() + ConfigurationSettings.AppSettings["FileName"].ToString();

                //string[] lines = File.ReadAllLines(HttpContext.Current.Server.MapPath(path), Encoding.UTF8);
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                if (lines.Length > 0)
                {
                    string databaseString = DatabaseFactory.CreateDatabase().ConnectionString;
                    string connectionString = "";
                    foreach (string res in lines)
                    {
                        connectionString = connectionString + res.ToString();
                    }
                    connectionString = "User " + connectionString + databaseString;
                    SFDatebase = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return SFDatebase;
            }
            return SFDatebase;
        }

        public static Database GetConnectionSecurity()
        {
            Database SFDatebase = DatabaseFactory.CreateDatabase();  
            try
            {

                string path = ConfigurationSettings.AppSettings["FileLocation"].ToString() + ConfigurationSettings.AppSettings["FileName"].ToString();

                //string[] lines = File.ReadAllLines(HttpContext.Current.Server.MapPath(path), Encoding.UTF8);
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                if (lines.Length > 0)
                {

                    string databaseString = DatabaseFactory.CreateDatabase("APPSERVICESConnectionString").ConnectionString;
                    string connectionString = "";
                    foreach (string res in lines)
                    {
                        connectionString = connectionString + res.ToString();
                    }
                    connectionString = "User " + connectionString + databaseString;
                    SFDatebase = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return SFDatebase;
            }
            return SFDatebase;
        }


        public static string GetConnectionSecuritySetting() {

            string connectionString = ConfigurationManager.ConnectionStrings["APPSERVICESConnectionString"].ConnectionString;
            try
            {

                string path = ConfigurationSettings.AppSettings["FileLocation"].ToString() + ConfigurationSettings.AppSettings["FileName"].ToString();
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                if (lines.Length > 0)
                {
                    string credentialString = "";
                    foreach (string res in lines)
                    {
                        credentialString = credentialString + res.ToString();
                    }
                    connectionString = "User " + credentialString + connectionString;
                }
            }
            catch (Exception ex)
            {
              throw ex;
            }
            return connectionString;
        }
    }
}
