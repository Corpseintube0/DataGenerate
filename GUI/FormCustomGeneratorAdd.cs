using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GUI
{
    public partial class FormCustomGeneratorAdd : Form
    {
        private string[] _baseGenerators;
        private List<string> _customGenerators;

        public FormCustomGeneratorAdd(string[] baseGenerators)
        {
            InitializeComponent();

            _baseGenerators = (string[])baseGenerators.Clone();
            _customGenerators = new List<string>();
            var ds = Program.StagedDB.SelectRows("SELECT * FROM CustomGeneratorsList ORDER BY CGName");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                _customGenerators.Add(row.ItemArray[0].ToString());
                string[] dgvrow = new string[] { row.ItemArray[0].ToString() };
                dataGridViewCustomGens.Rows.Add(dgvrow);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Program.StagedDB.DeleteValues("CustomGeneratorsList");

            foreach (DataGridViewRow row in dataGridViewCustomGens.Rows)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("CGName");
                dt.Columns.Add("Path");
                ds.Tables.Add(dt);

                string path = row.Cells[0].Value.ToString() + ".txt";
                string cgname = row.Cells[0].Value.ToString();

                ds.Tables[0].Rows.Add(cgname, path);
                Program.StagedDB.InsertDataset("CustomGeneratorsList", ds);
            }
            Close();
        }

        private void FormCustomGeneratorAdd_Load(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
          
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridViewCustomGens_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewCustomGens.Rows == null || dataGridViewCustomGens.CurrentRow == null)
                return;
            string fileName = dataGridViewCustomGens.CurrentRow.Cells[0].Value.ToString() + ".txt";
            textBox1.Text = "";
            try
            {
                List<string> list = new List<string>();
                using (StreamReader fs = new StreamReader(fileName))
                {
                    while (true)
                    {
                        string temp = fs.ReadLine();
                        if (temp == null)
                            break;
                        list.Add(temp);
                    }
                    textBox1.Lines = list.ToArray();
                }
            }
            catch  (FileNotFoundException ex)
            {
                File.WriteAllText(dataGridViewCustomGens.CurrentRow.Cells[0].Value.ToString() + ".txt", textBox1.Text, Encoding.Unicode);
            }
        }

        private void WriteTextBoxToFile()
        {
            StreamWriter streamWriter = new StreamWriter(dataGridViewCustomGens.CurrentRow.Cells[0].Value.ToString() + ".txt");
            foreach (var line in textBox1.Lines)
                streamWriter.WriteLine(line);
            streamWriter.Close();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (dataGridViewCustomGens.CurrentRow == null)
                return;
            WriteTextBoxToFile();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var addForm = new FormAddOrRenameGenerator(true);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                string newGenName = addForm.GeneratorName;
                if (_baseGenerators.Contains(newGenName) || _customGenerators.Contains(newGenName))
                {
                    MessageBox.Show("Уже существует генератор с таким именем. Введите другое имя.");
                    return;
                }
                //DataGridViewRow r = new DataGridViewRow();

                string[] row = new string[] { newGenName };

                dataGridViewCustomGens.Rows.Add(row);
                _customGenerators.Add(newGenName);

                textBox1.Text = "";
                File.WriteAllText(newGenName + ".txt", textBox1.Text, Encoding.Unicode);
                //StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                //streamWriter.WriteLine(textBox1.Text);
                //streamWriter.Close();

                //CustomGeneratorsConfigSection section = (CustomGeneratorsConfigSection)ConfigurationManager.GetSection("CustomGenerators");

            }
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            string newName = dataGridViewCustomGens.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удаленные данные нельзя будет вернуть! Продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            try
            {
                File.Delete(dataGridViewCustomGens.CurrentRow.Cells[0].Value.ToString() + ".txt");
            }
            catch (NullReferenceException ex)
            {

            }
            _customGenerators.Remove(dataGridViewCustomGens.CurrentRow.Cells[0].Value.ToString());
            dataGridViewCustomGens.Rows.RemoveAt(dataGridViewCustomGens.CurrentRow.Index);
            Program.StagedDB.DeleteValues("CustomGeneratorsList");
            foreach (DataGridViewRow row in dataGridViewCustomGens.Rows)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("CGName");
                dt.Columns.Add("Path");
                ds.Tables.Add(dt);

                string path = row.Cells[0].Value.ToString() + ".txt";
                ds.Tables[0].Rows.Add(row.Cells[0].Value.ToString(), path);
                Program.StagedDB.InsertDataset("CustomGeneratorsList", ds);
            }
        }
    }
}
