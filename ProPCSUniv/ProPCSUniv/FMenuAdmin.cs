using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ProPCSUniv
{
    public partial class FMenuAdmin : Form
    {
        MasterJurusan m_jurusan;
        MasterDosen m_dosen;
        MasterMahasiswa m_mhs;
        MasterRuangan m_ruang;
        MasterPeriode m_periode;
        public FMenuAdmin()
        {
            InitializeComponent();
        }

        private void masterJurusanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_jurusan  = new MasterJurusan();
            m_jurusan.MdiParent = this;
            m_jurusan.Show();
        }

        private void masterMahasiswaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_mhs = new MasterMahasiswa();
            m_mhs.MdiParent = this;
            m_mhs.Show();
        }

        private void masterDosenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_dosen = new MasterDosen();
            m_dosen.MdiParent = this;
            m_dosen.Show();
        }

        private void masterRuanganToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ruang = new MasterRuangan();
            m_ruang.MdiParent = this;
            m_ruang.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_periode = new MasterPeriode();
            m_periode.MdiParent = this;
            m_periode.Show();
        }
    }
}
