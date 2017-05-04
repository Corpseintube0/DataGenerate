using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGenerator;

namespace GUI
{
    static class Program
    {
        private static DBLogic _targetDB = new DBLogic(); //целевая БД
        private static StagedDB _stagedDB = new StagedDB("StagedDB.db"); //промежуточная БД

        public static DBLogic TargetDB
        {
            get { return _targetDB; }
        }

        public static StagedDB StagedDB
        {
            get { return _stagedDB; }
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
