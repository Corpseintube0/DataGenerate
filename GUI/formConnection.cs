using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGenerator;

namespace GUI
{
    public partial class FormConnection : Form
    {
        public FormConnection()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Program.TargetDB.ConnectionString = txtBoxConnectionStr.Text;
            try
            {
                Program.TargetDB.Connect(Program.TargetDB.ConnectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Подключение к базе успешно.");
            DialogResult = DialogResult.OK;
            Program.TargetDB.ConnectionClose();
            Close();
        }

        private void formConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
