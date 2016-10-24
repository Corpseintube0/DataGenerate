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

        //private string[] _genTypes = Enum.GetNames(typeof(GeneratorTypes));

        /// <summary>
        /// Значения, сгенерированные в текущей сессии работы.
        /// </summary>
        private string[][] _currentGeneratedValues;

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
                lblDbName.Text = String.Format("Версия сервера СУБД = {0}\nБаза данных = {1}", ProgramData.DB.ServerName, ProgramData.DB.DBName);
                cmbBoxTableNames.DataSource = ProgramData.DB.GetTableNames();
                FillDataGridView(cmbBoxTableNames.Text);
                toolStripComboBoxGens.ComboBox.DataSource = Enum.GetNames(typeof(GeneratorTypes)); //GeneratorTypes.cs
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

        }

        private void InsertDataIntoTable(string[][] arg)
        {
            var data = ProgramData.DB.GetColumnNames(cmbBoxTableNames.Text);
            int colCount = data.Length; //число столбцов и генераторов
            int rowCount = arg[0].Length;
            string[] temp = new string[1];
            for (int i = 0; i < rowCount; ++i)
            {
                temp = new string[colCount];
                for (int j = 0; j < colCount; ++j)
                    temp[j] = arg[j][i];
                try
                {
                    ProgramData.DB.InsertIntoTable(cmbBoxTableNames.Text, temp); //вставка одной строки в таблицу
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(String.Format("При вставке данных СУБД сгенерировала исключение:\n{0} {1}", ex.Message, ex.Errors[0].Message));
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Исключение уровня программной оболочки: {0}", ex.Message));
                    return;
                }
            }
        }

        /// <summary>
        /// Инициализирует генератор в соответствии с переданным типом.
        /// </summary>
        /// <param name="type">Желаемый тип генератора.</param>
        /// <returns>Объект, представляющий конкретную реализацию абстрактного класса TestDataGenerator.</returns>
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
                    return new DateTimeGenerator(new DateTime(2015, 12, 15), new DateTime(2015, 12, 20));
                case GeneratorTypes.Text:
                    return new RandomTextGenerator(100);
                default:
                    throw new Exception("Нет такого генератора.");
            }
        }

        /// <summary>
        /// Срабатывает при изменении выбранной из списка таблицы.
        /// </summary>
        private void cmbBoxTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDataGridView(cmbBoxTableNames.Text);
        }

        private void toolStripBtnGenerate_Click(object sender, EventArgs e)
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
                    while (true)
                    {
                        try
                        {
                            fi = new FileInfo(String.Format("Export\\GeneratedSQLQuery {0}.sql", date.ToString().Remove(10) + String.Format("({0})", filesTotal)));
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
                sw.WriteLine("INSERT INTO [{0}].[dbo].[{1}]", ProgramData.DB.DBName, cmbBoxTableNames.Text);
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

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Начало генерации TODO: вставить логику для генерации через наборы данных с учетом уникальности, пустоты и т.п.
            int rowCount = (int)numGenCount.Value; //число строк для генерации
            //string[][] values = new string[colCount][];
            _currentGeneratedValues = new string[colCount][];
            for (int i = 0; i < colCount; ++i)
                _currentGeneratedValues[i] = _generators[i].NextSet(rowCount);

            dataGridViewPreview.Columns.Clear();
            for (int i = 0; i < data.Length; ++i)
                dataGridViewPreview.Columns.Add(string.Format("column{0}", i), data[i]);

            if (rowCount > 100)
                groupBoxPreview.Text = String.Format("Предварительный просмотр сгенерированных данных (первые 100 строк из {0})", rowCount);
            else
                groupBoxPreview.Text = String.Format("Предварительный просмотр сгенерированных данных ({0} строк)", rowCount);
            for (int i = 0; i < rowCount; ++i)
            {
                if (i < 100)
                {
                    var preview = new string[colCount];
                    for (int j = 0; j < colCount; ++j)
                        preview[j] = _currentGeneratedValues[j][i];
                    dataGridViewPreview.Rows.Add(preview); //Вывод для предварительного просмотра
                }
                if (sqlExport)
                {
                    file = fi.Open(FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(file);
                    sw.Write("(");
                    for (int j = 0; j < colCount; ++j)
                    {
                        sw.Write("'{0}'", _currentGeneratedValues[j]);
                        if (j + 1 != colCount)
                            sw.Write(", ");
                        else
                            sw.Write(")");
                    }
                    if (i + 1 != rowCount)
                        sw.Write(",");
                    sw.WriteLine("\n");
                    sw.Close();
                    file.Dispose();
                }
            }
            toolStripBtnInsert.Enabled = true;
        }

        private void toolStripBtnInsert_Click(object sender, EventArgs e)
        {
            InsertDataIntoTable(_currentGeneratedValues);
        }

        //обработчик события: клик по строке в gridview со столбцами и генераторами
        private void dataGridViewColumns_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewColumns.CurrentRow == null)
                return;
            toolStripComboBoxGens.Text = (string)dataGridViewColumns.CurrentRow.Cells[3].Value;
        }

        //обработчик события
        private void toolStripComboBoxGens_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewColumns.CurrentRow == null)
                return;
             dataGridViewColumns.CurrentRow.Cells[3].Value = toolStripComboBoxGens.Text;
        }
    }
}
