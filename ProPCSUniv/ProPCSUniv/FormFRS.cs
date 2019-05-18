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
    public partial class FormFRS : Form
    {
        public FormFRS()
        {
            InitializeComponent();
        }

        OracleDataAdapter ADAP, daH, daD;
        DataTable DT, DTMataKuliah, DTAutogen, dtH, dtD;
        OracleTransaction oTrans;
        OracleCommandBuilder ocombobuilderH, ocombobuilderD;

        private void cmbMatkul_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        OracleConnection conn = Form1.conn;

        private void isi_builder()
        {
            daH = new OracleDataAdapter("select * from perwalian where kode_frs=''", conn);
            dtH = new DataTable();
            ocombobuilderH = new OracleCommandBuilder(daH);
            daH.Fill(dtH);
            daD = new OracleDataAdapter("select * from d_transaksi where kode_frs=''", conn);
            dtD = new DataTable();
            ocombobuilderD = new OracleCommandBuilder(daD);
            daD.Fill(dtD);
        }

        private void isi_semua()
        {
            //Jurusan
            OracleCommand ocjur = new OracleCommand("select kode_jurusan from mahasiswa where NRP ='" + Form1.usernamelogin + "'",conn);
            String kodejurusan = ocjur.ExecuteScalar().ToString();
            OracleCommand ocnamjur = new OracleCommand("select nama_jurusan from jurusan where kode_jurusan ='" + kodejurusan + "'", conn);
            String jurusan = ocnamjur.ExecuteScalar().ToString();
            label4.Text = jurusan;

            //Nama Mahasiswa
            OracleCommand namamhs = new OracleCommand("select nama_mahasiswa from mahasiswa where NRP ='" + Form1.usernamelogin + "'", conn);
            String nama = namamhs.ExecuteScalar().ToString();
            label7.Text = nama;

            //Dosen Wali
            OracleCommand nipdos = new OracleCommand("select nip from mahasiswa where NRP ='" + Form1.usernamelogin + "'", conn);
            String nipmhs = nipdos.ExecuteScalar().ToString();
            OracleCommand dosnip = new OracleCommand("select nama_dosen from dosen where NIP ='" + nipmhs + "'", conn);
            String nip = dosnip.ExecuteScalar().ToString();
            label9.Text = nip;
        }

        private void autogenfrs()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            OracleCommand oauto = new OracleCommand("select autogenfrs1('" +Form1.usernamelogin +"') from dual", conn);
            String hasil = oauto.ExecuteScalar().ToString();
            txtID.Text = hasil;
        }

        private void FormFRS_Load(object sender, EventArgs e)
        {
            bukacombo(); autogenfrs(); isi_semua();
        }

        private void bukacombo()
        {
            DTMataKuliah = new DataTable();
            ADAP = new OracleDataAdapter("select * from matakuliah", conn);
            ADAP.Fill(DTMataKuliah);
            cmbMatkul.Items.Clear();
            cmbMatkul.ValueMember = "kode_mk";
            cmbMatkul.DisplayMember = "nama_mk";
            cmbMatkul.DataSource = DTMataKuliah;
        }

    }
}
