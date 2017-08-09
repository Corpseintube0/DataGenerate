using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormAddOrRenameGenerator : Form
    {
        public string GeneratorName
        {
            get { return textBox1.Text; } 
        }

        public FormAddOrRenameGenerator(bool add, string oldName = "")
        {
            InitializeComponent();
            if (add)
            {
                Text = "Добавление нового генератора";
                label1.Text = "Имя нового генератора:";
            }
            else
            {
                Text = "Переименование генератора";
                label1.Text = "Новое имя генератора:";
                textBox1.Text = oldName;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
        }
    }
}
