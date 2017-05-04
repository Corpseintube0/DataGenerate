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
        private Control[] _connectionStringControls;
        private Control[] _autorizationControls;

        public FormConnection()
        {
            InitializeComponent();

            _connectionStringControls = new Control[] { txtBoxConnectionStr };
            _autorizationControls = new Control[] { label1, label2, label3, label4, textBoxUserName, textBoxDbName, textBoxPassword, textBoxServerName };
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
            //MessageBox.Show("Подключение к базе успешно.");
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

        private void txtBoxConnectionStr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnConnect_Click(sender, e);
        }

        private void rdoBtnConnectionType0_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in _connectionStringControls)
                c.Enabled = rdoBtnConnectionType0.Checked;
            foreach (Control c in _autorizationControls)
                c.Enabled = !rdoBtnConnectionType0.Checked;
        }

        private void FormConnection_Load(object sender, EventArgs e)
        {
            rdoBtnConnectionType0_CheckedChanged(sender, e);
        }

        private void txtBoxConnectionStr_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
