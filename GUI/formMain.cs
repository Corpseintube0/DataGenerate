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
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            if (Program.DB != null)
                return;
            formConnection formConnect = new formConnection();
            if (formConnect.ShowDialog() == DialogResult.OK)
            {
                lblDbName.Text = Program.DB.DBName;
                cmbBoxTableNames.DataSource = Program.DB.GetTableNames();
            }
                
            //Refresh();
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.DB != null)
                Program.DB.ConnectionClose(); //закрываем подключение
            Application.Exit();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var tdg = new TextGenerator("RussianMaleNames.txt");
        }
    }
}
