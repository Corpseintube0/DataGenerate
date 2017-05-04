using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    /// <summary>
    /// Задает пару: имя таблицы - имя столбца таблицы.
    /// </summary>
    public struct TableColumnPair
    {
        /// <summary>
        /// Имя таблицы.
        /// </summary>
        string TableName;

        /// <summary>
        /// Имя столбца.
        /// </summary>
        string ColumnName;

        public TableColumnPair(string tableName, string columnName)
        {
            TableName = tableName;
            ColumnName = columnName;
        }
    }
}
