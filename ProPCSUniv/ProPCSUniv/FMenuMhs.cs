using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProPCSUniv
{
    public partial class FMenuMhs : Form
    {
        FormFRS ffrs;
        public FMenuMhs()
        {
            InitializeComponent();
        }

        private void fRSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ffrs = new FormFRS();
            ffrs.MdiParent = this;
            ffrs.Show();
        }
    }
}
