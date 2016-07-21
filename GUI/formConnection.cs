using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGen;

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
            Program.DB = new DBLogic();
            Program.DB.Connect(txtBoxConnectionStr.Text);

            MessageBox.Show("Подключение к базе успешно.");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void formConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
