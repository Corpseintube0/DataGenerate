using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
        }

        public void NextProgressBarStep()
        {
            progressBar.PerformStep();
            //this.Refresh();
        }

        public void SetProgressBarSteps(int value)
        {
            progressBar.Maximum = value;
        }

        /// <summary>
        /// Для отладки.
        /// </summary>
        public void ShowProgressValue()
        {
            MessageBox.Show(progressBar.Value.ToString() + " Max:" + progressBar.Maximum.ToString());
        }
    }
}
