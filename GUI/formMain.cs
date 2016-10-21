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
using System.IO;

namespace GUI
{
    public partial class formMain : Form
    {
        private string[] _genTypes = Enum.GetNames(typeof(GeneratorTypes));

        private TestDataGenerator[] _generators;

        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            formConnection formConnect = new formConnection();
            if (formConnect.ShowDialog() == DialogResult.OK)
            {
                lblDbName.ForeColor = Color.Black;
                lblDbName.Text = String.Format("Версия сервера = {0}\nБаза данных = {1}", ProgramData.DB.ServerName, ProgramData.DB.DBName);
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
                string type = types[i][0];
                if (types[i][1] != "")
                    type += String.Format("({0})", types[i][1]);
                string[] row = { "false", names[i], type};
                dataGridViewColumns.Rows.Add(row);
            }
            foreach (DataGridViewRow row in dataGridViewColumns.Rows)
            {
                ((DataGridViewComboBoxCell)(row.Cells[3])).DataSource = _genTypes; //GeneratorTypes.cs
            }
            DataGridViewSuggestTypes();
        }
        
        /// <summary>
        /// В зависимости от типов и имён предлагает типы генераторов из ComboBoxCell.
        /// </summary>
        private void DataGridViewSuggestTypes()
        {
            //пока только по типам
            var types = ProgramData.DB.GetColumnTypes(cmbBoxTableNames.Text);
            var names = ProgramData.DB.GetColumnNames(cmbBoxTableNames.Text);
            for (int i=0; i<types.Length; ++i)
            {
                switch (types[i][0])
                {
                    case "uniqueidentifier":
                        (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.GUID.ToString();
                        //TODO: нужно как-то заблокировать имена, не относящиеся к данному типу
                        break;
                    case "nchar":
                    case "nvarchar":
                    case "char":
                    case "varchar":
                    case "text":
                        if (names[i].ToLower().Contains("name"))
                            (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.Names.ToString();
                        else
                            (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.Text.ToString();
                        break;
                    case "date":
                        (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.Date.ToString();
                        break;
                    case "int":
                        (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.Int.ToString();
                        break;
                    default:
                        throw new Exception("No such a type!");
                }
            }
        }            

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ProgramData.DB != null)
                ProgramData.DB.ConnectionClose(); //закрываем подключение
            Application.Exit();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var data = ProgramData.DB.GetColumnNames(cmbBoxTableNames.Text);
            int colCount = data.Length; //число столбцов и генераторов
            _generators = new TestDataGenerator[colCount];
            for (int i = 0; i < colCount; ++i)
            {
                object gen = Enum.Parse(typeof(GeneratorTypes), (string)dataGridViewColumns.Rows[i].Cells[3].Value);
                if ((GeneratorTypes)gen == GeneratorTypes.Int)
                    _generators[i] = new IntegerGenerator((int)UpDownMin.Value, (int)UpDownMax.Value);
                else
                    _generators[i] = GetGeneratorFromType((GeneratorTypes)gen); 
            }
            bool sqlExport = chkBoxExportToSQL.Checked;
            StreamWriter sw;
            var date = DateTime.Now;
            var fi = new FileInfo(String.Format("Export\\GeneratedSQLQuery {0}.sql", date.ToString().Remove(10)));
            FileStream file;
            if (sqlExport)
            {
                try
                {
                    file = fi.Open(FileMode.CreateNew, FileAccess.Write);
                }
                catch (IOException) //если файл уже существует, создаем новый, добавляя в конец имени порядковый номер
                {
                    int filesTotal = 1;
                    while(true)
                    {
                        try
                        {
                            fi = new FileInfo(String.Format("Export\\GeneratedSQLQuery {0}.sql", date.ToString().Remove(10) + String.Format("({0})", filesTotal), filesTotal));
                            file = fi.Open(FileMode.CreateNew, FileAccess.Write);
                            break;
                        }
                        catch (IOException)
                        {
                            ++filesTotal;
                            continue;
                        }
                    }
                }
                sw = new StreamWriter(file);
                sw.WriteLine("--Данный файл сгенерирован автоматически");
                sw.WriteLine("--Время генерации: {0}", date.ToString());
                sw.WriteLine("INSERT INTO [{0}].[dbo].[{1}]",ProgramData.DB.DBName,  cmbBoxTableNames.Text);
                sw.Write("(");
                for (int i = 0; i < colCount; ++i)
                {
                    sw.Write("[{0}]", data[i]);
                    if (i + 1 != colCount)
                        sw.Write(", ");
                    else
                        sw.Write(")");
                }
                sw.WriteLine();
                sw.WriteLine("VALUES ");
                sw.Close();
                file.Dispose();
            }
            int count = (int)numGenCount.Value; //число строк для генерации
            for (int i = 0; i < count; ++i)
            {
                List<string> values = new List<string>();
                for (int j=0; j<colCount; ++j)
                    values.Add(_generators[j].Next());
                try
                {
                    ProgramData.DB.InsertIntoTable(cmbBoxTableNames.Text, values.ToArray()); //вставка одной строки в таблицу
                }
                catch(System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(String.Format("При вставке данных СУБД сгенерировала исключение:\n{0} {1}", ex.Message, ex.Errors[0].Message));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(String.Format("Исключение уровня программной оболочки: {0}", ex.Message));
                }
                if (sqlExport)
                {
                    //var fi = new FileInfo(String.Format("Export\\GeneratedSQLQuery {0}.sql", date.ToString().Remove(10)));
                    file = fi.Open(FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(file);
                    sw.Write("(");
                    for (int j = 0; j < colCount; ++j)
                    {
                        sw.Write("'{0}'", values[j]);
                        if (j + 1 != colCount)
                            sw.Write(", ");
                        else
                            sw.Write(")");
                    }
                    if (i + 1 != count)
                        sw.Write(",");
                    sw.WriteLine("\n");
                    sw.Close();
                    file.Dispose();
                }
            }
        }

        /// <summary>
        /// Инициализирует генератор в соответствии с переданным типом.
        /// </summary>
        /// <param name="type">Объект, представляющий конкретную реализацию абстрактного класса TestDataGenerator.</param>
        /// <returns></returns>
        private TestDataGenerator GetGeneratorFromType(GeneratorTypes type)
        {
            switch (type)
            {
                case GeneratorTypes.Names:
                    return new TextGenerator("RussianMaleNames.txt");
                case GeneratorTypes.Surnames:
                    return new TextGenerator("RussianMaleSurnames.txt");
                case GeneratorTypes.GUID:
                    return new GuidGenerator();
                case GeneratorTypes.Int:
                    return new IntegerGenerator();
                case GeneratorTypes.Date:
                    return new DateTimeGenerator();
                default:
                    throw new Exception("Нет такого генератора.");
            }
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

        private void txtBoxGenCount_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
