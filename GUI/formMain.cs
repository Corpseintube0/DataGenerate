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
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace GUI
{
    public partial class FormMain : Form
    {

        //private string[] _genTypes = Enum.GetNames(typeof(GeneratorTypes));

        /// <summary>
        /// Значения, сгенерированные в текущей сессии работы.
        /// </summary>
        private string[][] _currentGeneratedValues;
        /// <summary>
        /// список таблиц
        /// </summary>
        private string[] _tableNames;
        /// <summary>
        /// текущая таблица
        /// </summary>
        private string _currentTable;
        //список доступных генераторов
        private TestDataGenerator[] _generators;

        

        public FormMain()
        {
            InitializeComponent();
            //поток с консолью для отладки
            Task.Factory.StartNew(DebugConsole); //TODO: delete
        }

        /// <summary>
        /// Для отладки.
        /// </summary>
        private void DebugConsole()
        {
            // Запускаем консоль.
            if (AllocConsole())
            {
                System.Console.WriteLine("Для выхода наберите exit.");
                while (true)
                {
                    // Считываем данные.
                    string output = System.Console.ReadLine();
                    if (output == "exit")
                        break;
                    // Выводим данные в textBox
                    //Action action = () => textBox.Text += output + Environment.NewLine;
                    //if (InvokeRequired)
                    //    Invoke(action);
                    //else
                    //    action();
                }
                // Закрываем консоль.
                FreeConsole();
            }
        }
        //-==============================================- для консоли TODO: del
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeConsole();
        //-==============================================-

        private void formMain_Load(object sender, EventArgs e)
        {
            FormConnection formConnect = new FormConnection();
            if (formConnect.ShowDialog() == DialogResult.OK)
            {
                this.Text = String.Format("Генератор данных для SQL Server. Версия сервера СУБД: {0}. База данных: {1}", Program.TargetDB.ServerName, Program.TargetDB.DBName);
                cmbBoxColType.SelectedIndex = 0;
                _tableNames = Program.TargetDB.GetTableNames();
                foreach (var item in _tableNames)
                {
                    string[] row = { "false", item };
                    dataGridViewTables.Rows.Add(row);
                }
                //FillDataGridViewColumns(dataGridViewTables.Rows[0].Cells[1].Value.ToString());
                //FillDataGridViewColumns(_currentTable);
                toolStripComboBoxGens.ComboBox.DataSource = Enum.GetNames(typeof(GeneratorTypes)); //GeneratorTypes.cs

                string query =  "SELECT " +
                                "OBJECT_NAME(constraint_object_id) AS FKName, " +
                                "OBJECT_NAME(parent_object_id) AS TableName, " +
                                "COL_NAME(parent_object_id, parent_column_id) AS ColumnName, " +
                                "OBJECT_NAME(referenced_object_id) AS RefTableName, " +
                                "COL_NAME(referenced_object_id, referenced_column_id) AS RefColumnName " +
                                "FROM " +
                                "sys.foreign_key_columns " +
                                "ORDER BY " +
                                "FKName, " +
                                "constraint_column_id ";
                var ds = Program.TargetDB.SelectRows(query);
                Program.StagedDB.InsertDataset("FKList", ds); //заполняем таблицу промежуточной БД данными о внешних ключах
                DataGridViewSuggestTypes();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string name = row["TableName"].ToString() + row["ColumnName"].ToString();
                    Program.StagedDB.CreateFKTable(name, "TEXT");
                    string queryToTargetDB = String.Format("SELECT {0} FROM {1}", row["RefColumnName"], row["RefTableName"]);
                    var valuesFormTargetDB = Program.TargetDB.SelectRows(queryToTargetDB);
                    List<string> temp = new List<string>();
                    foreach (DataRow row2 in valuesFormTargetDB.Tables[0].Rows)
                        temp.Add(row2[0].ToString());
                    Program.StagedDB.FillFKTable(name, temp.ToArray());
                }
            }
            else
            {
                this.Close();
                Application.Exit();
            }
        }

        /// <summary>
        /// Заполняет DataGridView метаданными из таблицы.
        /// </summary>
        /// <param name="tableName">Имя таблицы из БД для отображения данных.</param>
        private void FillDataGridViewColumns(string tableName)
        {
            dataGridViewColumns.Rows.Clear();
            var names = Program.TargetDB.GetColumnNames(tableName);
            var types = Program.TargetDB.GetColumnTypes(tableName);
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
            var types = Program.TargetDB.GetColumnTypes(_currentTable);
            var names = Program.TargetDB.GetColumnNames(_currentTable);

            //проверяем есть ли в текущей таблице внешние ключи
            string sql = String.Format("SELECT TableName, ColumnName FROM FKList WHERE TableName = '{0}'", _currentTable);
            DataSet ds = Program.StagedDB.SelectRows(sql);
            List<string> fkColumns = new List<string>();
            if (ds.Tables != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                    fkColumns.Add(row.ItemArray[1].ToString());
            }

            for (int i=0; i<types.Length; ++i)
            {
                if (fkColumns.Contains(names[i]))
                {
                    dataGridViewColumns.Rows[i].Cells[3].Value = GeneratorTypes.ForeinKey.ToString();
                    continue;
                }
                switch (types[i][0])
                {
                    case "uniqueidentifier":
                        (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.GUID.ToString();
                        //TODO: нужно заблокировать имена, не относящиеся к данному типу
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
                    case "float": //TODO: исправить
                        (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.Int.ToString();
                        break;
                    default:
                        throw new Exception("No such a type!");
                }
            }
        }            

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.TargetDB != null)
                Program.TargetDB.ConnectionClose(); //закрываем подключение
            Application.Exit();
        }

        private void InsertDataIntoTable(string[][] arg)
        {
            var data = Program.TargetDB.GetColumnNames(_currentTable);
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
                    Program.TargetDB.InsertIntoTable(_currentTable, temp); //вставка одной строки в таблицу
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(String.Format("При вставке данных СУБД сгенерировала исключение:\n{0} {1}", ex.Message, ex.Errors[0].Message));
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Исключение: {0}", ex.Message));
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
                    var tg = new TextGenerator("RussianMaleNames.txt");
                    //tg.UniqueValues = true;
                    //tg.EmptyValues = 20;
                    return tg;
                case GeneratorTypes.Surnames:
                    return new TextGenerator("RussianMaleSurnames.txt");
                case GeneratorTypes.GUID:
                    return new GuidGenerator();
                case GeneratorTypes.Int:
                    return new IntegerGenerator();
                case GeneratorTypes.Date:
                    return new DateTimeGenerator(new DateTime(1960, 12, 15), new DateTime(2000, 12, 20));
                case GeneratorTypes.Text:
                    return new RandomTextGenerator(100);
                default:
                    throw new Exception("Нет такого генератора.");
            }
        }

        /// <summary>
        /// Срабатывает при изменении режима просмотра столбцов.
        /// </summary>
        private void cmbColType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillDataGridViewColumns(_currentTable);
        }

        private void toolStripBtnGenerate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var progressForm = new FormProgress();
            
            progressForm.Show();
            progressForm.Location = new Point(Location.X + Width / 2 - progressForm.Width / 2, Location.Y + Height/2 - progressForm.Height / 2);

            var colNames = Program.TargetDB.GetColumnNames(_currentTable);
            int colCount = colNames.Length; //число столбцов и генераторов
            _generators = new TestDataGenerator[colCount];
            for (int i = 0; i < colCount; ++i)
            {
                object gen = Enum.Parse(typeof(GeneratorTypes), (string)dataGridViewColumns.Rows[i].Cells[3].Value);
                switch ((GeneratorTypes)gen)
                {
                    case GeneratorTypes.Int:
                        _generators[i] = new IntegerGenerator((int)UpDownMin.Value, (int)UpDownMax.Value);
                        break;
                    case GeneratorTypes.ForeinKey:
                        //TODO: Выполнить назначение генератора с соответствующим DataSet
                        DataSet ds = Program.StagedDB.SelectRows(string.Format("SELECT * FROM {0}{1};", _currentTable, colNames[i]));
                        _generators[i] = new ForeignKeyGenerator(ds);
                        break;
                    default:
                        _generators[i] = GetGeneratorFromType((GeneratorTypes)gen);
                        break;
                }
                //if ((GeneratorTypes)gen == GeneratorTypes.Int)
                //    _generators[i] = new IntegerGenerator((int)UpDownMin.Value, (int)UpDownMax.Value);
                //else
                //    _generators[i] = GetGeneratorFromType((GeneratorTypes)gen);
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
                sw.Close();
                file.Dispose();
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Начало генерации TODO: связать UI с параметрами генераторов (чтобы пользователь сам мог настраивать генерацию)
            int rowCount = (int)numGenCount.Value; //число строк для генерации
            progressForm.SetProgressBarSteps(rowCount);
            _currentGeneratedValues = new string[colCount][];
            for (int i = 0; i < colCount; ++i)
                _currentGeneratedValues[i] = _generators[i].NextSet(rowCount);

            dataGridViewPreview.Columns.Clear();
            for (int i = 0; i < colNames.Length; ++i)
                dataGridViewPreview.Columns.Add(string.Format("column{0}", i), colNames[i]);

            if (rowCount > 100)
                groupBoxPreview.Text = String.Format("Предварительный просмотр сгенерированных данных (первые 100 строк из {0})", rowCount);
            else
                groupBoxPreview.Text = String.Format("Предварительный просмотр сгенерированных данных ({0} строк)", rowCount);
            for (int i = 0; i < rowCount; ++i)
            {
                progressForm.NextProgressBarStep();
                try
                {
                    if (i < 100)
                    {
                        var preview = new string[colCount];
                        for (int j = 0; j < colCount; ++j)
                            preview[j] = _currentGeneratedValues[j][i];
                        dataGridViewPreview.Rows.Add(preview); //Вывод для предварительного просмотра
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show(String.Format("Сгенерировано {0} значений из {1}, т.к. достигнут максимальный размер множества уникальных значений.", i, rowCount), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
                if (sqlExport)
                {
                    file = fi.Open(FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(file);
                    sw.WriteLine("INSERT INTO [{0}].[dbo].[{1}]", Program.TargetDB.DBName, _currentTable);
                    sw.Write("(");
                    for (int k = 0; k < colCount; ++k)
                    {
                        sw.Write("[{0}]", colNames[k]);
                        if (k + 1 != colCount)
                            sw.Write(", ");
                        else
                            sw.Write(")");
                    }
                    sw.WriteLine();
                    sw.WriteLine("VALUES ");
                    sw.Write("(");
                    for (int j = 0; j < colCount; ++j)
                    {
                        sw.Write("'{0}'", _currentGeneratedValues[j][i]);
                        if (j + 1 != colCount)
                            sw.Write(", ");
                        else
                            sw.Write(")");
                    }
                    sw.WriteLine("\n");
                    sw.Close();
                    file.Dispose();
                }
            }
            toolStripBtnInsert.Enabled = true;
            progressForm.Close(); 
            Cursor.Current = Cursors.Arrow;
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
            if ((string)dataGridViewColumns.CurrentRow.Cells[3].Value == GeneratorTypes.ForeinKey.ToString())
                toolStripComboBoxGens.Enabled = false;
            else
                toolStripComboBoxGens.Enabled = true;
            dataGridViewColumns.CurrentRow.Selected = true;
            grpBoxOptions.Text = String.Format("Настройки генерации для {0}", dataGridViewColumns.CurrentRow.Cells[3].Value);
            if (dataGridViewPreview.Columns.Count > 0)
                dataGridViewPreview.Columns[dataGridViewColumns.CurrentRow.Index].Selected = true;
        }

        //обработчик события
        private void toolStripComboBoxGens_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewColumns.CurrentRow == null)
                return;
            if (toolStripComboBoxGens.Text == "ForeinKey")
            {
                toolStripComboBoxGens.Text = dataGridViewColumns.CurrentRow.Cells[3].Value.ToString();
                return;
            }
            dataGridViewColumns.CurrentRow.Cells[3].Value = toolStripComboBoxGens.Text;
            grpBoxOptions.Text = String.Format("Настройки генерации для {0}", dataGridViewColumns.CurrentRow.Cells[3].Value);
        }

        //Обработка нажатия горячих клавиш
        private void formMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5) //нажата F5
            {
                toolStripBtnGenerate.PerformClick();
            }
            else
            if (e.KeyData == Keys.F9) //нажата F9
            {
                toolStripBtnInsert.PerformClick();
            }
        }

        private void buttonShowHideTables_Click(object sender, EventArgs e)
        {
            if (splitContainerTables.Panel1Collapsed)
            {
                splitContainerTables.Panel1Collapsed = false;
                this.Left -= 200;
                this.Width += 200;
            }

            else
            {
                splitContainerTables.Panel1Collapsed = true;
                this.Left += 200;
                this.Width -= 200;
            }
        }

        private void dataGridViewTables_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewTables.CurrentRow.Cells[1].Value == null)
                return;
            _currentTable = dataGridViewTables.CurrentRow.Cells[1].Value.ToString();
            FillDataGridViewColumns(_currentTable);
            if (dataGridViewColumns.Rows.Count != 0)
            {
                dataGridViewColumns.Rows[0].Selected = true;
                dataGridViewColumns_SelectionChanged(sender, new EventArgs());
            }
            grpBoxTables.Text = "Столбцы таблицы " + dataGridViewTables.CurrentRow.Cells[1].Value.ToString();
        }

        /// <summary>
        /// Обработчик события: смена состояния столбца CheckBox в строке таблицы.
        /// </summary>
        private void dataGridViewColumns_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0)
                return;

        }

        //отладочный метод TODO: delete
        private void PrintTable(DataTable dt)
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.BufferWidth = 1000;

            foreach (System.Collections.DictionaryEntry de in dt.ExtendedProperties)
                Console.WriteLine("Ключ = {0}, Значение = {1}", de.Key, de.Value);
            Console.WriteLine();

            // Создание объекта DataTableReader
            DataTableReader dtReader = dt.CreateDataReader();
            while (dtReader.Read())
            {
                for (int i = 0; i < dtReader.FieldCount; i++)
                    Console.Write("{0,-20}", dtReader.GetValue(i).ToString().Trim());
                Console.WriteLine();
            }
            dtReader.Close();
        }

        //отладочный метод TODO: delete
        private void PrintDataSet(DataSet ds)
        {
            Console.BufferWidth = 1000;
            // Вывод имени и расширенных свойств
            Console.WriteLine("*** Объекты DataSet ***\n");
            Console.WriteLine("Имя DataSet: {0}", ds.DataSetName);
            foreach (System.Collections.DictionaryEntry de in ds.ExtendedProperties)
                Console.WriteLine("Ключ = {0}, Значение = {1}", de.Key, de.Value);
            Console.WriteLine();

            // Вывод каждой таблицы
            foreach (DataTable dt in ds.Tables)
            {
                //Console.WriteLine("=> Таблица: {0}", dt.TableName);

                // Вывод имени столбцов
                for (int curCol = 0; curCol < dt.Columns.Count; ++curCol)
                    Console.Write("{0,-20}", dt.Columns[curCol].ColumnName);
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------");

                // Выводим содержимое таблицы
                PrintTable(dt);
            }
        }
    }
}
