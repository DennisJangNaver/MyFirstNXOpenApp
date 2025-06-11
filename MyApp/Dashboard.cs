using NXOpen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UI.GetUI().NXMessageBox.Show("Main Thread", NXMessageBox.DialogType.Information, Thread.CurrentThread.ManagedThreadId.ToString());

            this.Invoke(new Action( () =>
            {
                UI.GetUI().NXMessageBox.Show("Main Thread", NXMessageBox.DialogType.Information, Thread.CurrentThread.ManagedThreadId.ToString());
                UI.GetUI().NXMessageBox.Show("Winform", NXMessageBox.DialogType.Information, "Hello, NX WinForm!!!");
            }));

           /* this.BeginInvoke(new Action(() =>
            {
                UI.GetUI().NXMessageBox.Show("Main Thread", NXMessageBox.DialogType.Information, Thread.CurrentThread.ManagedThreadId.ToString());
                UI.GetUI().NXMessageBox.Show("Winform", NXMessageBox.DialogType.Information, "Hello, NX WinForm!!!");
            }));*/

            //this.backgroundWorker1.RunWorkerAsync();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.backgroundWorker1 == null)
                return;

            UI.GetUI().NXMessageBox.Show("Main Thread", NXMessageBox.DialogType.Information, Thread.CurrentThread.ManagedThreadId.ToString());
            UI.GetUI().NXMessageBox.Show("Winform", NXMessageBox.DialogType.Information, "Hello, NX WinForm!!!");

        }
    }
}
