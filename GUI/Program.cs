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
        public static DBLogic TargetDB = new DBLogic(); //целевая БД
        public static StagedDB StagedDB = new StagedDB(); //промежуточная БД

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
