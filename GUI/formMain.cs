using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGenerator;
using System.IO;
using System.Runtime.InteropServices;

namespace GUI
{
    public partial class FormMain : Form
    {
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
        private string _currentTableName;

        private string _currentTableSchema;
        /// <summary>
        /// назначенные генераторы
        /// </summary>
        //private TestDataGenerator[] _generators;
        /// <summary>
        /// Задаёт соответствие между столбцами и назначенными генераторами.
        /// </summary>
        private Dictionary<TableColumnPair, TestDataGenerator> _colGenDictionary = new Dictionary<TableColumnPair, TestDataGenerator>();
        //private FormAutoRules _formAutoRules;
        //список доступных генераторов данных
        private List<string> generatorsList;
        /// <summary>
        /// Заданные пользователем генераторы данных.
        /// </summary>
        private List<string> customGens;
        public FormMain()
        {
            InitializeComponent();
            //поток с консолью для отладки
            Task.Factory.StartNew(DebugConsole); //TODO: delete
            //_formAutoRules = new FormAutoRules();

            tabControlGeneratorProperties.ItemSize = new Size(0, 1);
        }

        /// <summary>
        /// Для отладки. TODO: Delete
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
        //-==============================================- 
        //TODO: delete
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
                comboBoxSort.SelectedIndex = 2;
                //comboBoxSort_SelectedIndexChanged(null, null);
                //Составление словаря из пар "имя_таблицы-имя_столбца"
                
                dataGridViewTables.Sort(dataGridViewTables.Columns[1], ListSortDirection.Ascending);
                generatorsList = new List<string>(Enum.GetNames(typeof(GeneratorTypes)));
                customGens = new List<string>();
                var ds = Program.StagedDB.SelectRows("SELECT CGName FROM CustomGeneratorsList");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    generatorsList.Add(row.ItemArray[0].ToString());
                    customGens.Add(row.ItemArray[0].ToString());
                }
                toolStripComboBoxGens.ComboBox.DataSource = generatorsList.ToArray();//GeneratorTypes.cs
                //заполнение данных о внешних ключах
                string query =  "SELECT " +
                                "OBJECT_NAME(parent_object_id) + COL_NAME(parent_object_id, parent_column_id) AS FKName, " +
                                "OBJECT_SCHEMA_NAME(parent_object_id) + '.' + OBJECT_NAME(parent_object_id) AS TableName," +
                                "COL_NAME(parent_object_id, parent_column_id) AS ColumnName, " +
                                "OBJECT_SCHEMA_NAME(referenced_object_id) + '.' + OBJECT_NAME(referenced_object_id) AS RefTableName," +
                                "COL_NAME(referenced_object_id, referenced_column_id) AS RefColumnName " +
                                "FROM " +
                                "sys.foreign_key_columns " +
                                "ORDER BY " +
                                "FKName, " +
                                "constraint_column_id ";
                ds = Program.TargetDB.SelectRows(query);
                Program.StagedDB.InsertDataset("FKList", ds);

                query = "SELECT TABLE_SCHEMA + '.' + TABLE_NAME as TABLE_NAME, COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH FROM information_schema.columns";
                ds = Program.TargetDB.SelectRows(query);
                Program.StagedDB.InsertDataset("TableColumnAttributes", ds);

                //назначение "ключей" и "значений" словаря
                if (MessageBox.Show("База открывается впервые. Выполнить автоназначение генераторов?", "?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    //назначение по-умолчанию
                    foreach (DataGridViewRow row in dataGridViewTables.Rows)
                    {
                        string tableName = row.Cells[1].Value.ToString();
                        string[] columnNames = Program.TargetDB.GetColumnNames(tableName);
                        for (int i = 0; i < columnNames.Count(); ++i)
                        {
                            TableColumnPair tcp = new TableColumnPair(tableName, columnNames[i]);
                            _colGenDictionary.Add(tcp, new RandomTextGenerator());
                        }
                    }
                }
                else  //назначение в соответствии с правилами
                    GeneratorTypesAutoAssign();
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
            if (_colGenDictionary.Count == 0)
                return;
            dataGridViewColumns.Rows.Clear();
            var names = Program.TargetDB.GetColumnNames(tableName);
            var dataTypes = Program.TargetDB.GetColumnTypes(tableName);
            for (int i = 0; i < names.Length; ++i)
            {
                string dataType = dataTypes[i][0];
                if (dataTypes[i][1] != "")
                    dataType += String.Format("({0})", dataTypes[i][1]);
                TableColumnPair tcp = new TableColumnPair(tableName, names[i]);
                Type type = _colGenDictionary[tcp].GetType();
                string generatorName;
                if (type == typeof(TextGenerator))
                    generatorName = (_colGenDictionary[tcp] as TextGenerator).AssignedName;
                else 
                    generatorName = GetGeneratorNameByType(type);
                string[] row = { "false", names[i], dataType, generatorName};
                dataGridViewColumns.Rows.Add(row);
            }
        }

        private string GetGeneratorNameByType(Type arg)
        {
            switch (arg.Name)
            {
                case "IntegerGenerator":
                    return GeneratorTypes.Integer.ToString();
                //case "TextGenerator":
                //    return GeneratorTypes.Text.ToString();
                case "GuidGenerator":
                    return GeneratorTypes.GUID.ToString();
                case "DateTimeGenerator":
                    return GeneratorTypes.DateTime.ToString();
                case "RandomTextGenerator":
                    return GeneratorTypes.Text.ToString();
                case "ForeignKeyGenerator":
                    return GeneratorTypes.ForeignKey.ToString();
                default:
                    throw new Exception("неизвестный тип генератора данных!");
            }
        }

        /// <summary>
        /// Заполняет словарь (таблицы, столбцы) значениями (тип генератора).
        /// </summary>
        private void GeneratorTypesAutoAssign()
        {
            string[] tableNames = Program.TargetDB.GetTableNames(true);
            for (int i = 0; i < tableNames.Count(); ++i)
            {
                string[] columnNames = Program.TargetDB.GetColumnNames(tableNames[i]);
                for (int j = 0; j < columnNames.Count(); ++j)
                {
                    TableColumnPair tcp = new TableColumnPair(tableNames[i], columnNames[j]);
                    //назначение генератора из AutoAssignRules
                    string genName = AutoAssignGeneratorName(tableNames[i], columnNames[j]);
                    object genEnum = Enum.Parse(typeof(GeneratorTypes), genName);
                    var generator = GetGeneratorFromType((GeneratorTypes)genEnum);
                    _colGenDictionary.Add(tcp, generator);
                }
            }
        }

        /// <summary>
        /// Выполняет автоматическое назначение генератора в соответствии с правилами назначения генераторов и возвращает имя генератора, которое можно преобразовать в тип.
        /// </summary>
        /// <param name="tableName">Имя таблицы.</param>
        /// <param name="columnName">Имя столбца.</param>
        /// <returns>Имя назначенного генератора.</returns>
        private string AutoAssignGeneratorName(string tableName, string columnName)
        {
            //проверка на внешний ключ
            string fkQuery = String.Format("SELECT * FROM FKList WHERE TableName = '{0}' AND ColumnName = '{1}'", tableName, columnName);
            DataSet dsFk = Program.StagedDB.SelectRows(fkQuery);
            if (dsFk.Tables[0].Rows.Count != 0)
                return GeneratorTypes.ForeignKey.ToString();

            //проверка на соответствие правилу
            var dsRules = Program.StagedDB.SelectRows("SELECT * FROM AutoTypeAssignRules ORDER BY Priority");
            foreach (DataRow rowRule in dsRules.Tables[0].Rows)
            {
                string addRule = rowRule["Rule"].ToString() == "" ? "" : " AND (" + rowRule["Rule"] + ")";
                string ruleQuery = String.Format("SELECT * FROM TableColumnAttributes WHERE TABLE_NAME='{0}' AND COLUMN_NAME='{1}'{2}", tableName, columnName, addRule);
                var ds = Program.StagedDB.SelectRows(ruleQuery);
                if (ds.Tables[0].Rows.Count != 0)
                    return rowRule["GenType"].ToString();
            }
            return GeneratorTypes.Text.ToString();
        }

        /// <summary>
        /// Выставляет столбцам типы генераторов в соответствии с заданными правилами.
        /// </summary>
        private void DataGridViewSuggestTypes() //заменить на GeneratorsAutoAssign
        {
            string sqlFK = String.Format("SELECT TableName, ColumnName FROM FKList WHERE TableName = '{0}'", _currentTableName);
            DataSet dsFK = Program.StagedDB.SelectRows(sqlFK);
            List<string> fkColumns = new List<string>();
            if (dsFK.Tables != null)
            {
                foreach (DataRow row in dsFK.Tables[0].Rows)
                    fkColumns.Add(row.ItemArray[1].ToString());
            }
            if (_currentTableName == null)
                throw new Exception("Не задано имя текущей таблицы!");
            var types = Program.TargetDB.GetColumnTypes(_currentTableName);
            var names = Program.TargetDB.GetColumnNames(_currentTableName);
            List<int> foreignKeyIndexes = new List<int>();
            for (int i = 0; i < dataGridViewColumns.Rows.Count; ++i)
            {
                //проверяем есть ли в текущей таблице внешние ключи
                if (fkColumns.Contains(names[i]))
                {
                    dataGridViewColumns.Rows[i].Cells[3].Value = GeneratorTypes.ForeignKey.ToString();
                    foreignKeyIndexes.Add(i);
                    continue;
                }
            }

            var dsRules = Program.StagedDB.SelectRows("SELECT * FROM AutoTypeAssignRules ORDER BY Priority DESC;");
            foreach (DataRow rowRule in dsRules.Tables[0].Rows)
            {
                string sql = String.Format("SELECT COLUMN_NAME FROM TableColumnAttributes WHERE TABLE_NAME='{0}' {1}", _currentTableName, rowRule["Rule"]);
                //получили имена столбцов, соответствующие правилу
                var ds = Program.StagedDB.SelectRows(sql);
                List<string> appropriateColumns = new List<string>();
                foreach (DataRow r in ds.Tables[0].Rows)
                    appropriateColumns.Add(r.ItemArray[0].ToString());
                for(int i = 0; i<dataGridViewColumns.Rows.Count; ++i)
                {
                    //проверяем на внешние ключи
                    if (foreignKeyIndexes.Contains(i))
                        continue;
                    //проверяем на соответствие
                    if (appropriateColumns.Contains(dataGridViewColumns.Rows[i].Cells[1].Value))
                        dataGridViewColumns.Rows[i].Cells[3].Value = rowRule["GenType"];
                }
            }

            // устар.
            //var types = Program.TargetDB.GetColumnTypes(_currentTable);
            //var names = Program.TargetDB.GetColumnNames(_currentTable);

            ////проверяем есть ли в текущей таблице внешние ключи
            //string sql = String.Format("SELECT TableName, ColumnName FROM FKList WHERE TableName = '{0}'", _currentTable);
            //DataSet ds = Program.StagedDB.SelectRows(sql);
            ////PrintDataSet(ds);
            //List<string> fkColumns = new List<string>();
            //if (ds.Tables != null)
            //{
            //    foreach (DataRow row in ds.Tables[0].Rows)
            //        fkColumns.Add(row.ItemArray[1].ToString());
            //}

            //for (int i=0; i<types.Length; ++i)
            //{
            //    if (fkColumns.Contains(names[i]))
            //    {
            //        dataGridViewColumns.Rows[i].Cells[3].Value = GeneratorTypes.ForeinKey.ToString();
            //        continue;
            //    }
            //    switch (types[i][0])
            //    {
            //        case "uniqueidentifier":
            //            (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.GUID.ToString();
            //            //TODO: нужно заблокировать имена, не относящиеся к данному типу
            //            break;
            //        case "nchar":
            //        case "nvarchar":
            //        case "char":
            //        case "varchar":
            //        case "text":
            //            if (names[i].ToLower().Contains("name"))
            //                (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.Names.ToString();
            //            else
            //                (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.Text.ToString();
            //            break;
            //        case "date":
            //        case "datetime":
            //            (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.Date.ToString();
            //            break;
            //        case "smallint":
            //        case "tinyint":
            //        case "int":
            //        case "float": //TODO: исправить
            //            (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.Integer.ToString();
            //            break;
            //        default:
            //            (dataGridViewColumns.Rows[i].Cells[3]).Value = GeneratorTypes.Text.ToString(); //для пользовательских типов (возможно, потом заменится)
            //            break;
            //            //throw new Exception("No such a type!");
            //    }
            //}
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.TargetDB != null)
                Program.TargetDB.ConnectionClose(); //закрываем подключение
            Application.Exit();
        }

        private void InsertDataIntoTable(string[][] arg)
        {
            var data = Program.TargetDB.GetColumnNames(_currentTableName);
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
                    Program.TargetDB.InsertIntoTable(_currentTableName, temp); //вставка одной строки в таблицу
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
                    var tg = new TextGenerator("RussianMaleNames.txt", "Names");
                    return tg;
                case GeneratorTypes.Lastnames:
                    return new TextGenerator("RussianMaleSurnames.txt", "Surnames");
                case GeneratorTypes.GUID:
                    return new GuidGenerator();
                case GeneratorTypes.Integer:
                    return new IntegerGenerator(1, 200);
                case GeneratorTypes.Date:
                case GeneratorTypes.DateTime:
                    return new DateTimeGenerator(new DateTime(1960, 12, 15), new DateTime(2000, 12, 20));
                case GeneratorTypes.Text:
                    return new RandomTextGenerator(25);
                case GeneratorTypes.ForeignKey:
                    return new ForeignKeyGenerator();
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

            var colNames = Program.TargetDB.GetColumnNames(_currentTableName);
            int colCount = colNames.Length; //число столбцов и генераторов
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
                _currentGeneratedValues[i] = _colGenDictionary[new TableColumnPair(_currentTableName, colNames[i])].NextSet(rowCount);
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
                    sw.WriteLine("INSERT INTO [{0}].[dbo].[{1}]", Program.TargetDB.DBName, _currentTableName);
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
            string generatorName = (string)dataGridViewColumns.CurrentRow.Cells[3].Value;
            toolStripComboBoxGens.Text = generatorName;
            if (generatorName == GeneratorTypes.ForeignKey.ToString())
                toolStripComboBoxGens.Enabled = false;
            else
                toolStripComboBoxGens.Enabled = true;
            dataGridViewColumns.CurrentRow.Selected = true;
            grpBoxOptions.Text = String.Format("Настройки генерации для {0}", generatorName);
            //if (dataGridViewPreview.Columns.Count > 0)
            //    dataGridViewPreview.Columns[dataGridViewColumns.CurrentRow.Index].Selected = true;
            object genType;
            if (customGens.Contains(toolStripComboBoxGens.Text))
            {
                genType = null;
                ShowGenerationPanel(GeneratorTypes.Names);
            }
                
            else
            {
                genType = Enum.Parse(typeof(GeneratorTypes), generatorName);
                ShowGenerationPanel((GeneratorTypes)genType);
            }
        }

        //обработчик события: смена индекса в списке генераторов
        private void toolStripComboBoxGens_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewColumns.CurrentRow == null)
                return;
            string genName = dataGridViewColumns.CurrentRow.Cells[3].Value.ToString();
            if (toolStripComboBoxGens.Text == "ForeignKey")
            {
                toolStripComboBoxGens.Text = genName;
                return;
            }
            dataGridViewColumns.CurrentRow.Cells[3].Value = toolStripComboBoxGens.Text;
            grpBoxOptions.Text = String.Format("Настройки генерации для {0}", genName);
            object genType;
            if (customGens.Contains(toolStripComboBoxGens.Text))
                genType = null;
            else
                genType = Enum.Parse(typeof(GeneratorTypes), toolStripComboBoxGens.Text);
            string colName = dataGridViewColumns.CurrentRow.Cells[1].Value.ToString();
            string tableName = dataGridViewTables.CurrentRow.Cells[1].Value.ToString();
            
            if (genType == null)
            {
                ShowGenerationPanel(GeneratorTypes.Names); 
                var ds = Program.StagedDB.SelectRows(String.Format("SELECT Path FROM CustomGeneratorsList WHERE CGName='{0}'", toolStripComboBoxGens.Text));
                _colGenDictionary[new TableColumnPair(tableName, colName)] = new TextGenerator(ds.Tables[0].Rows[0].ItemArray[0].ToString());
            }
            
            else
            {
                ShowGenerationPanel((GeneratorTypes)genType);
                _colGenDictionary[new TableColumnPair(tableName, colName)] = GetGeneratorFromType((GeneratorTypes)genType);
            }
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
            _currentTableName = dataGridViewTables.CurrentRow.Cells[1].Value.ToString();
            FillDataGridViewColumns(_currentTableName);
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

        /// <summary>
        /// Фильтр по имени таблицы.
        /// </summary>
        private void textBoxTableNameFilter_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewTables.Rows)
            {
                string tableName = row.Cells[1].Value.ToString().ToLower();
                if (!tableName.Contains(textBoxTableNameFilter.Text.ToLower()))
                    row.Visible = false;
            }
        }

        private void textBoxTableNameFilter_TextChanged(object sender, EventArgs e)
        {
            if (textBoxTableNameFilter.Text == "")
            {
                foreach (DataGridViewRow row in dataGridViewTables.Rows)
                    row.Visible = true;
            }
        }

        private void txtBoxColumnFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxColumnFilter.Text == "")
            {
                foreach (DataGridViewRow row in dataGridViewColumns.Rows)
                    row.Visible = true;
            }
        }

        /// <summary>
        /// Фильтр по имени столбца.
        /// </summary>
        private void txtBoxColumnFilter_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewColumns.Rows)
            {
                string colName = row.Cells[1].Value.ToString().ToLower();
                if (!colName.Contains(txtBoxColumnFilter.Text.ToLower()))
                    row.Visible = false;
            }
        }

        private void автоназначениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAutoRules().ShowDialog();
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewTables.Rows.Clear();
            string[] namesnSchemas;
            switch (comboBoxSort.SelectedIndex)
            {
                case 2:
                    namesnSchemas = Program.TargetDB.GetTableNames(true);
                    for (int i = 0; i < namesnSchemas.Count(); ++i)
                    {
                        string[] row = { "false", namesnSchemas[i] };
                        dataGridViewTables.Rows.Add(row);
                    }
                    break;
                case 0:
                    namesnSchemas = Program.TargetDB.GetTableNames(true, "BASE TABLE");
                    for (int i = 0; i < namesnSchemas.Count(); ++i)
                    {
                        string[] row = { "false", namesnSchemas[i] };
                        dataGridViewTables.Rows.Add(row);
                    }
                    break;
                case 1:
                    namesnSchemas = Program.TargetDB.GetTableNames(true, "VIEW");
                    for (int i = 0; i < namesnSchemas.Count(); ++i)
                    {
                        string[] row = { "false", namesnSchemas[i] };
                        dataGridViewTables.Rows.Add(row);
                    }
                    break;
            }
        }

        /// <summary>
        /// Отображает на форме нужную контекстно-зависимую панель с параметрами генератора. 
        /// </summary>
        private void ShowGenerationPanel(GeneratorTypes genType)
        {
            switch (genType)
            {                                                                //0 - Text
                case GeneratorTypes.Names:                                   //1 - Int
                case GeneratorTypes.Lastnames:                               //2 - GUID
                    tabControlGeneratorProperties.SelectTab(0);              //3 - Date
                    break;                                                   //4 - Foreign
                case GeneratorTypes.Integer:                                 //5 - RandomText
                    tabControlGeneratorProperties.SelectTab(1);
                    break;
                case GeneratorTypes.GUID:
                    tabControlGeneratorProperties.SelectTab(2);
                    break;
                case GeneratorTypes.Date:
                case GeneratorTypes.DateTime:
                    tabControlGeneratorProperties.SelectTab(3);
                    break;
                case GeneratorTypes.ForeignKey:
                    tabControlGeneratorProperties.SelectTab(4);
                    break;
                case GeneratorTypes.Text:
                    tabControlGeneratorProperties.SelectTab(5);
                    break;
                default:
                    throw new Exception("Unknown generator type!");
            }
        }
        
        private void RecordGeneratorParameters()
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var baseNames = Enum.GetNames(typeof(GeneratorTypes));
            new FormCustomGeneratorAdd(baseNames).ShowDialog();
            generatorsList = new List<string>(Enum.GetNames(typeof(GeneratorTypes)));
            customGens = new List<string>();
            var ds = Program.StagedDB.SelectRows("SELECT CGName FROM CustomGeneratorsList");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                generatorsList.Add(row.ItemArray[0].ToString());
                customGens.Add(row.ItemArray[0].ToString());
            }
            toolStripComboBoxGens.ComboBox.DataSource = generatorsList.ToArray();
        }
    }
}
