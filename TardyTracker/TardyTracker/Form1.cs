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

            string idNumber = tbIdNumber.Text;
            getDataConnection dataConnection = new getDataConnection();

            dataConnection.dataCmdString("UPDATE [SHEET1$] SET Tardys = Tardys + 1 WHERE Id = " + idNumber);

            dataTable = dataConnection.dataTableFill(idNumber);

            dataView.DataSource = dataTable;
        }
    }
}
