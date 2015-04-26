using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DJimmy.GUI
{
    public partial class Progress : Form
    {
        public Progress()
        {
            InitializeComponent();
        }

        public void SetText(string status)
        {
            progressBar1.Visible = false;
            lblStatus.Text = status;
        }

        public void SetProgress(int progress, string status)
        {
            progressBar1.Visible = true;
            progressBar1.Value = progress;
            lblStatus.Text = status;
        }

        private void Progress_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }
    }
}
