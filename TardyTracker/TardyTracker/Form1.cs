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
            int tardy = 0;

            getDataConnection dataConnection = new getDataConnection();

            dataConnection.dataCmdString("UPDATE [SHEET1$] SET Tardys = Tardys + 1 WHERE Id = " + idNumber);
            dataTable = dataConnection.dataTableFill(idNumber);

            foreach(DataRow row in dataTable.Rows) //pulls the information from dataTable so that i can use in methods. 
            {
                firstName = row["FirstName"].ToString();
                lastName = row["LastName"].ToString();
                tardys = row["Tardys"].ToString();
            }

            if (Convert.ToInt32(tardys) >= 3) //this is to cause roll-over whenever a child hits more than 3 tardys. 
            {
                dataConnection.dataCmdString("UPDATE [SHEET1$] SET Tardys = 0 WHERE Id = " + idNumber);
            }
            tardy = Convert.ToInt32(tardys);
            

            switch(tardy) //gives a specific message depending on how many tardys the child has recived. 
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

            }


            tbIdNumber.Clear(); //clears the student ID input box so that another can't troll. 
            

            dataView.DataSource = dataTable; //using this for testing processes right now, probably won't keep in the final product. 
        }
    }
}
