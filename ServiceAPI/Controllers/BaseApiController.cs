using DataAccessLayer.Repository;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace ServiceAPI.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        public IEasyBuyRepository EasyBuyRepo { get; }

        public IEprocRepository EprocRepo { get; }

        protected BaseApiController(IEprocRepository eprocRepository, IEasyBuyRepository easyBuyRepository)
        {
            EprocRepo = eprocRepository;
            EasyBuyRepo = easyBuyRepository;
        }
        
        private (string dataSource, string userId, string password) GetTargetSource()
        {
            var userId = ConfigurationManager.AppSettings.Get("USERID");
            var password = ConfigurationManager.AppSettings.Get("PASSWORD");
            var dataSource = ConfigurationManager.AppSettings.Get("DATASOURCE");

            return (dataSource: dataSource, userId: userId, password: password);

        }

        protected byte[] ParseDataUrl(string dataUrl)
        {
            try
            {
                var base64Data = Regex.Match(dataUrl, @"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)").Groups["data"].Value;
                var binData = Convert.FromBase64String(base64Data);
                return binData;
            }
            catch (Exception EX_NAME)
            {
                Debug.WriteLine(EX_NAME.Message);
                throw EX_NAME;
            }
        }

        protected String[] GetExcelSheetNames(string path)
        {
            OleDbConnection objConn = null;
            DataTable dt = null;

            try
            {
                string connString = string.Empty;

                string importFileName = path;
                string fileExtension = Path.GetExtension(importFileName);

                if (fileExtension == ".xls")
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + importFileName + ";" + "Extended Properties='Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text'";
                if (fileExtension == ".xlsx")
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + importFileName + ";" + "Extended Properties='Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text'";


                // Create connection object by using the preceding connection string.

                objConn = new OleDbConnection(connString);
                // Open connection with the database.

                objConn.Open();
                // Get the data table containg the schema guid.

                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.

                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                // Loop through all of the sheets if you want too...

                for (int j = 0; j < excelSheets.Length; j++)
                {
                    // Query each excel sheet.

                }

                return excelSheets;
            }
            catch (Exception ex)
            {
                WriteLog("Error in GetExcelSheetNames method ");
                throw ex;
            }
            finally
            {
                // Clean up.

                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        protected DataTable ReadExcelFile(string sheetName, string path)
        {

            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string Import_FileName = path;
                string fileExtension = Path.GetExtension(Import_FileName);
                if (fileExtension == ".xls")
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text'";
                if (fileExtension == ".xlsx")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text'";
                //conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=NO;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";



                using (OleDbCommand comm = new OleDbCommand())
                {

                    comm.CommandText = "Select * from [" + sheetName + "$]";

                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        return dt;
                    }

                }
            }
        }

        protected void WriteLog(string message)
        {

            string filePath = HttpContext.Current.Server.MapPath(@"~/Logs");

            filePath = Path.Combine(filePath, "Error.txt");
            using (StreamWriter sw = (File.Exists(filePath)) ? File.AppendText(filePath) : File.CreateText(filePath))
            {
                sw.WriteLine("Message :" + message + Environment.NewLine);
                sw.WriteLine(Environment.NewLine +
                                 "-----------------------------------------------------------------------------" +
                                 Environment.NewLine);
            }
        }


    }
}