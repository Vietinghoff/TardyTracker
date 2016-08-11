using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TardyTracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            string tardys = "null";
            string idNumber = tbIdNumber.Text;
            string firstName = "null";
            string lastName = "null";
            string offenseDate1 = "null";
            string offenseDate2 = "null";
            string offenseDate3 = "null";
            int tardy = 0;
            string date = DateTime.Now.ToString("MMddyyyy");

            getDataConnection dataConnection = new getDataConnection();

            dataConnection.dataCmdString("UPDATE [SHEET1$] SET Tardys = Tardys + 1 WHERE Id = " + idNumber);
            
            dataTable = dataConnection.dataTableFill(idNumber); //fills the data table with info so you can look at it in the program


            foreach (DataRow row in dataTable.Rows) //pulls the information from dataTable so that i can use in methods. 
            {
                firstName = row["FirstName"].ToString();
                lastName = row["LastName"].ToString();
                tardys = row["Tardys"].ToString();
            }

            if (Convert.ToInt32(tardys) > 3) //this is to cause roll-over whenever a child hits more than 3 tardys. not working as intended yet, need to add e-mail and then clear everything to defaults with email is sent.
            {
                dataConnection.dataCmdString("UPDATE [SHEET1$] SET Tardys = 0 WHERE Id = " + idNumber);
                dataConnection.dataCmdString("UPDATE [SHEET1$] SET OffenseDate1 = NULL WHERE Id = " + idNumber);
                dataConnection.dataCmdString("UPDATE [SHEET1$] SET OffenseDate2 = NULL WHERE Id = " + idNumber);
                dataConnection.dataCmdString("UPDATE [SHEET1$] SET OffenseDate3 = NULL WHERE Id = " + idNumber);
            }

            
            tardy = Convert.ToInt32(tardys);
            switch (tardy) //gives a specific message depending on how many tardys the child has recived. 
            {
                case 1:
                    MessageBox.Show(firstName + " " + lastName + " " + "You have " + tardys + " " + "tardy, if you get 3 you will be sent to after school detention", "Tardy Tracker");
                    break;

                case 2:
                    MessageBox.Show(firstName + " " + lastName + " " + "You have " + tardys + " " + "tardys, if you recieve one more you will be sent to after school detention", "Tardy Tracker");
                    break;

                case 3:
                    MessageBox.Show(firstName + " " + lastName + " " + "You have " + tardys + " " + "tardys, you will now have to serve afterschool detention", "Tardy Tracker");
                    break;

                default:
                    break;
            }

            dataTable = dataConnection.dataTableFill(idNumber); //fills the data table with info so you can look at it in the program
            foreach (DataRow row in dataTable.Rows) //pulls the information from dataTable so that i can use in methods. 
            {
                firstName = row["FirstName"].ToString();
                lastName = row["LastName"].ToString();
                tardys = row["Tardys"].ToString();
                offenseDate1 = row["OffenseDate1"].ToString();
                offenseDate2 = row["OffenseDate2"].ToString();
                offenseDate3 = row["OffenseDate3"].ToString();
            }

            tardy = Convert.ToInt32(tardys);
            switch (tardy) //fills in the appropriate offense date with the current date that the tardy was logged.
            {
                case 1:
                    dataConnection.dataCmdString("UPDATE [SHEET1$] SET OffenseDate1 = (" + date + ") WHERE Id = " + idNumber);
                    break;

                case 2:
                    dataConnection.dataCmdString("UPDATE [SHEET1$] SET OffenseDate2 = (" + date + ") WHERE Id = " + idNumber);
                    break;

                case 3:
                    dataConnection.dataCmdString("UPDATE [SHEET1$] SET OffenseDate3 = (" + date + ") WHERE Id = " + idNumber);
                    break;
            }

            dataTable = dataConnection.dataTableFill(idNumber); //fills the data table with info so you can look at it in the program
            foreach (DataRow row in dataTable.Rows) //pulls the information from dataTable so that i can use in methods. 
               {
                   firstName = row["FirstName"].ToString();
                   lastName = row["LastName"].ToString();
                   tardys = row["Tardys"].ToString();
                   offenseDate1 = row["OffenseDate1"].ToString();
                   offenseDate2 = row["OffenseDate2"].ToString();
                   offenseDate3 = row["OffenseDate3"].ToString();
               } 

            tbIdNumber.Clear(); //clears the student ID input box so that another can't troll. 
            

            dataView.DataSource = dataTable; //using this for testing processes right now, probably won't keep in the final product. 
        }
    }
}
