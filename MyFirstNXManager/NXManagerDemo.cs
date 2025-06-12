using NXOpen;
using NXOpen.PDM;
using NXOpen.UF;
using NXOpen.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstNXManager
{
    public partial class NXManagerDemo : Form
    {
        PdmSession thePdmSession = Program.theSession.PdmSession;
        Session theSession = Program.theSession;
        UFSession theUfSession = Program.theUfSession;

        public NXManagerDemo()
        {
            InitializeComponent();
        }

        private void CreateItemButton_Click(object sender, EventArgs e)
        {
            CreateNewPart();
        }

        private void OpenItemButton_Click(object sender, EventArgs e)
        {
            OpenItem();
        }

        private void NewPartNoButton_Click(object sender, EventArgs e)
        {
            GenerateNewId();
        }

        private void CheckTCInfoButton_Click(object sender, EventArgs e)
        {
            GetTCInfo();
        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            QueryItem();
        }

   
    }
}
