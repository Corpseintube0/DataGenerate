using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGeneration;

namespace GUI
{
    public partial class formConnection : Form
    {
        public formConnection()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ProgramData.DB.ConnectionString = txtBoxConnectionStr.Text;
            ProgramData.DB.Connect(ProgramData.DB.ConnectionString);

            MessageBox.Show("Подключение к базе успешно.");
            DialogResult = DialogResult.OK;

            ProgramData.DB.ConnectionClose();
            Close();
        }

        private void formConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
