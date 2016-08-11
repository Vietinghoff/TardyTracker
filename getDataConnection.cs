using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows;


namespace TardyTracker
{
    public class getDataConnection
    {
        public DataTable dataTableFill(string idNumber) //fills a data table of one childs information from the xcel file that will be acting as the database
        {

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data source=" + @"C:\GitHubRepos\TardyTracker\TardyTracker\Tardy Tracker Spread Sheet.xls"
                + ";" + "Extended Properties=Excel 8.0;";

            OleDbConnection objConnection = new OleDbConnection(connectionString);

            objConnection.Open();
            string qry = "SELECT * FROM [Sheet1$] WHERE id = " + idNumber;
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(qry, objConnection);
            da.Fill(dt);
            objConnection.Close();
            da.Dispose();
            return dt;
        }

     /*   public void dataTableSave(DataTable dataTable)
        {
            DataTable dt = new DataTable();
            dt = dataTable;
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data source=" + @"C:\GitHubRepos\TardyTracker\TardyTracker\Tardy Tracker Spread Sheet.xls"
                + ";" + "Extended Properties=Excel 8.0;";

            OleDbConnection objConnection = new OleDbConnection(connectionString);

            objConnection.Open();
            string qry = "UPDATE [Sheet1$] FROM " + dt;
            OleDbCommand cmd = new OleDbCommand(qry, objConnection);
            cmd.ExecuteNonQuery();
            objConnection.Close();

        }
        */

        public void dataCmdString(string qry) //allows me to send commands to the database, namely to update it on certain things like the amount of tardys that a child has and what date the offence occured. 
        {
            string command = qry;

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data source=" + @"C:\GitHubRepos\TardyTracker\TardyTracker\Tardy Tracker Spread Sheet.xls"
                + ";" + "Extended Properties=Excel 8.0;";

            OleDbConnection objConnection = new OleDbConnection(connectionString);
            objConnection.Open();

            OleDbCommand cmd = new OleDbCommand(command, objConnection);
            cmd.ExecuteNonQuery();
            objConnection.Close();
        }

       

        
    }


}

