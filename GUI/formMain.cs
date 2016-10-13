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
        private string[] _genTypes;

        public formMain()
        {
            _genTypes = new string[] {
                GeneratorTypes.Date.ToString(),
                GeneratorTypes.GUID.ToString(),
                GeneratorTypes.Int.ToString(),
                GeneratorTypes.Text.ToString(),
            };
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            formConnection formConnect = new formConnection();
            if (formConnect.ShowDialog() == DialogResult.OK)
            {
                lblDbName.Text = ProgramData.DB.DBName;
                cmbBoxTableNames.DataSource = ProgramData.DB.GetTableNames();
                FillDataGridView(cmbBoxTableNames.Text);
            }
            else
                this.Close();
        }

        /// <summary>
        /// Заполняет DataGridView метаданными из таблицы.
        /// </summary>
        /// <param name="tableName">Имя таблицы из БД для отображения данных.</param>
        private void FillDataGridView(string tableName)
        {
            dataGridViewColumns.Rows.Clear();
            var names = ProgramData.DB.GetColumnNames(tableName);
            var types = ProgramData.DB.GetColumnTypes(tableName);
            for (int i = 0; i < names.Length; ++i)
            {
                string[] row = { "false", names[i], types[i] };
                dataGridViewColumns.Rows.Add(row);
            }
            foreach (DataGridViewRow row in dataGridViewColumns.Rows)
            {
                ((DataGridViewComboBoxCell)(row.Cells[3])).DataSource = _genTypes;
                ((DataGridViewComboBoxCell)(row.Cells[3])).Value = _genTypes[0];
                //if (row.Cells["ColName"].Value == "")
                //{
                //    dataGridViewColumns.Rows.RemoveAt(row.Index); //TODO: удалить последнюю строку
                //}
            }
        }
            //dataGridViewColumns.Rows.RemoveAt(names.Length);
            

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ProgramData.DB != null)
                ProgramData.DB.ConnectionClose(); //закрываем подключение
            Application.Exit();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var tdg = new TextGenerator("RussianMaleNames.txt");
            var guid = Guid.NewGuid();

            ProgramData.DB.InsertIntoTable("Person", new[] { guid.ToString(), "Имя1","Фамилия1","2000-12-20" });


            //TODO: Добавить вывод сгенерированных данных в отчёт
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var guid = Guid.NewGuid();
            var date = DateTime.Today;

            string[] values = { guid.ToString(), "jfdjfd", "hhfdhfd", date.ToString() };
            //var adapter = Program.DB.InsertInto("Person", values);
            //adapter.Update();
            //TODO: сделать DataRow
        }

        /// <summary>
        /// Срабатывает при изменении выбранной из списка таблицы.
        /// </summary>
        private void cmbBoxTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDataGridView(cmbBoxTableNames.Text);
        }
    }
}
