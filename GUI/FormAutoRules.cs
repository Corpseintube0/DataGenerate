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
    public partial class FormAutoRules : Form
    {
        //TODO: Возможно, стоит ограничить набор правил 10-ю правилами (производительность)
        private string[] defaultTypes = new string[] {
                                                         GeneratorTypes.GUID.ToString(),
                                                         GeneratorTypes.Names.ToString(),
                                                         GeneratorTypes.Integer.ToString(),
                                                         GeneratorTypes.Date.ToString(),
                                                         GeneratorTypes.Lastnames.ToString(),
                                                         GeneratorTypes.Gender.ToString(),
                                                         GeneratorTypes.Country.ToString(),
                                                         GeneratorTypes.Department.ToString(),
                                                         GeneratorTypes.Emails.ToString(),
                                                         GeneratorTypes.Hash.ToString(),
                                                         GeneratorTypes.WorkingPosition.ToString(),
                                                         GeneratorTypes.Text.ToString()
                                                      };
        private string[] defaultRules = new string[] {
                                                         "DATA_TYPE = 'uniqueidentifier'" ,
                                                         "(DATA_TYPE = 'varchar' OR DATA_TYPE = 'char' OR DATA_TYPE = 'nchar' OR DATA_TYPE = 'nvarchar' OR DATA_TYPE = 'text') AND COLUMN_NAME LIKE '%name%'" ,
                                                         "DATA_TYPE = 'int' OR DATA_TYPE = 'tinyint' OR DATA_TYPE = 'smallint'" ,
                                                         "DATA_TYPE = 'date' OR DATA_TYPE = 'datetime'" ,
                                                         "(DATA_TYPE = 'varchar' OR DATA_TYPE = 'char' OR DATA_TYPE = 'nchar' OR DATA_TYPE = 'nvarchar' OR DATA_TYPE = 'text') AND COLUMN_NAME LIKE '%lastname%'",
                                                         "CHARACTER_MAXIMUM_LENGTH = 1 AND (DATA_TYPE = 'char' OR DATA_TYPE = 'nchar') AND COLUMN_NAME LIKE '%gender%'",
                                                         "(DATA_TYPE LIKE '%char%' OR DATA_TYPE = 'text') AND COLUMN_NAME LIKE '%country%'",
                                                         "DATA_TYPE NOT LIKE '%int%' AND (COLUMN_NAME LIKE 'Depart' OR COLUMN_NAME LIKE 'Group')",
                                                         "DATA_TYPE LIKE '%char%' AND COLUMN_NAME LIKE 'email'",
                                                         "DATA_TYPE LIKE '%varchar%' AND (COLUMN_NAME = 'hash' OR COLUMN_NAME = 'passwordhash')",
                                                         "DATA_TYPE LIKE '%varchar%' AND (COLUMN_NAME LIKE '%job%' OR COLUMN_NAME LIKE '%post%' OR COLUMN_NAME LIKE '%position%')",
                                                         ""
                                                      };

        private ToolTip toolTip = new ToolTip();
        private Button _buttonEditRule = new Button() { Size = new Size(30, 30), Text = "..." };
        private RichTextBox _textBoxEditRule = new RichTextBox() {Multiline = true, Size = new Size(450, 200) };
        private string[] _comboDataSource = Enum.GetNames(typeof(GeneratorTypes));

        public FormAutoRules()
        {
            InitializeComponent();
            toolTip.SetToolTip(_buttonEditRule, "Редактировать правило");
            var list = _comboDataSource.ToList();
            list.Remove("ForeignKey");
            _comboDataSource = list.ToArray();      
            (dataGridViewRules.Columns[2] as DataGridViewComboBoxColumn).DataSource = _comboDataSource;

            FillDataGridViewFromDb();
            //buttonRestoreDefault_Click(null, null);
        }

        private void FillDataGridViewFromDb()
        {
            dataGridViewRules.Rows.Clear();
            var ds = Program.StagedDB.SelectRows("SELECT * FROM AutoTypeAssignRules ORDER BY Priority");
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.Cells.Add(new DataGridViewCheckBoxCell() { Value = true });
                newRow.Cells.Add(new DataGridViewTextBoxCell() { Value = dataRow.ItemArray[0].ToString() });
                newRow.Cells.Add(new DataGridViewComboBoxCell() { DataSource = _comboDataSource, Value = dataRow.ItemArray[1].ToString() });
                string rule = dataRow.ItemArray[2].ToString();
                //if (rule.Length != 0) //убираем лишний AND и скобки
                //    rule = rule.Substring(5, rule.Length - 6);
                newRow.Cells.Add(new DataGridViewTextBoxCell() { Value = rule });
                newRow.Resizable = DataGridViewTriState.False;
                dataGridViewRules.Rows.Add(newRow);
            }
        }

        private void buttonRestoreDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Это действие вернет правила автоназначения по-умолчанию. Продолжить?", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            dataGridViewRules.Rows.Clear();
            for (int i = 0; i < defaultRules.Count(); ++i)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewCheckBoxCell() { Value = true });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = i.ToString() });
                row.Cells.Add(new DataGridViewComboBoxCell() { DataSource = _comboDataSource, Value = defaultTypes[i] });
                row.Cells.Add(new DataGridViewTextBoxCell() {Value = defaultRules[i]});
                row.Resizable = DataGridViewTriState.False;

                dataGridViewRules.Rows.Add(row);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Priority", typeof(Int32)); //
            dt.Columns.Add("GenType", typeof(String)); //(Priority INTEGER PRIMARY KEY, GenType TEXT, Rule TEXT)
            dt.Columns.Add("Rule", typeof(String));    //
            foreach (DataGridViewRow row in dataGridViewRules.Rows)
            {
                string newCell3Value = row.Cells[3].Value.ToString();
                if (row.Cells[3].Value.ToString() != "")
                    newCell3Value = String.Format("{0}", row.Cells[3].Value.ToString());
                object[] temp = new object[] { row.Cells[1].Value, row.Cells[2].Value, newCell3Value };
                dt.Rows.Add(temp);
            }
            ds.Tables.Add(dt);
            Program.StagedDB.DeleteValues("AutoTypeAssignRules");
            Program.StagedDB.InsertDataset("AutoTypeAssignRules", ds);

            buttonApply.Enabled = false;
        }

        private void dataGridViewRules_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            buttonApply.Enabled = true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (buttonApply.Enabled)
                buttonApply_Click(sender, e);
            Close();
        }

        private void новоеПравило_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            int priority = dataGridViewRules.Rows.Count;
            dataGridViewRules.Rows.Add(new object[] { true, priority, null, "" });
        }
    }
}
