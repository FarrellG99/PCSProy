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
    public partial class MasterJurusan : Form
    {
        OracleDataAdapter ADAP;
        DataTable DT, DTKAJUR;
        OracleConnection conn = Form1.conn;
        String searchtxt = "", status;
        public MasterJurusan()
        {
            InitializeComponent();
        }
        public void rename_header()
        {
            DG.Columns[0].HeaderText = "KODE";
            DG.Columns[1].HeaderText = "JURUSAN";
            DG.Columns[2].HeaderText = "NIP";
            DG.Columns[3].HeaderText = "DOSEN";
        }
        private void buka_grid()
        {
            DT = new DataTable();
            ADAP = new OracleDataAdapter("select jurusan.kode_jurusan,nama_jurusan,jurusan.nip,nama_dosen"+
                " from jurusan,dosen where jurusan.nip=dosen.nip " + searchtxt, conn);
            ADAP.Fill(DT);
            DG.DataSource = DT;
            rename_header();
        }
        private void buka_kajur()
        {
            DTKAJUR = new DataTable();
            ADAP = new OracleDataAdapter("select * from dosen", conn);
            ADAP.Fill(DTKAJUR);
            cmbKepalaJur.Items.Clear();
            foreach (DataRow item in DTKAJUR.Rows)
            {
                cmbKepalaJur.Items.Add(item[1].ToString());
            }
        }
        private int cari_idx_kajur(String kd)
        {
            int pos = -1;
            for (int i = 0; i < DTKAJUR.Rows.Count; i++)
            {
                if (DTKAJUR.Rows[i][0].ToString() == kd) pos = i;
            }
            return pos;
        }
        public void siapkan_form_mode(Boolean bol) //patokan dari button insert
        {
            btnInsert.Enabled = bol;
            btnUpdate.Enabled = !bol;
            btnDelete.Enabled = !bol;
            txtKodeJur.Enabled = bol;
        }
        public void bersihkan_form()
        {
            txtKodeJur.Text = ""; txtNamaJur.Text = ""; cmbKepalaJur.SelectedIndex = -1;
            rbAktif.Checked = true;
        }
        private void MasterJurusan_Load(object sender, EventArgs e)
        {
            buka_grid(); buka_kajur(); siapkan_form_mode(true);
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            searchtxt = " and (upper(nama_jurusan) like '%" + txtSearch.Text.ToUpper() + "%' " +
                " or upper(nama_dosen) like '%" + txtSearch.Text.ToUpper() + "%' or jurusan.kode_jurusan like '%"+ 
                txtSearch.Text.ToUpper()+ "%') order by 1";
            buka_grid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (rbTidakAktif.Checked) status = "N";
            else status = "Y";
            if (txtKodeJur.Text == "") MessageBox.Show("Isi Kode Jurusan");
            else if (txtNamaJur.Text == "") MessageBox.Show("Isi Nama Jurusan");
            else if (cmbKepalaJur.SelectedIndex == -1) MessageBox.Show("Pilih Kepala Jurusan");
            else
            {
                try
                {
                    OracleCommand oupd = new OracleCommand("update jurusan set " +
                     "nip='" + DTKAJUR.Rows[cmbKepalaJur.SelectedIndex].ItemArray[0].ToString() + "'," +
                     "nama_jurusan='" + txtNamaJur.Text + "'" +
                     " where " +
                     "kode_jurusan='" + txtKodeJur.Text + "'"
                     , conn);
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    oupd.ExecuteNonQuery();
                    MessageBox.Show("Data jurusan terubah");
                    siapkan_form_mode(true);
                    buka_grid(); bersihkan_form();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Update : " + ex.Message);
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            /*try
            {
                OracleCommand oupd = new OracleCommand("update jurusan set " +                   
                    "statusjurusan='N' where " +
                    "kodejurusan='" + txtKodeJur.Text + "'"
                    , conn);
                if (conn.State == ConnectionState.Closed) conn.Open();
                oupd.ExecuteNonQuery();
                MessageBox.Show("Data jurusan terhapus");
                siapkan_form_mode(true);
                buka_grid(); bersihkan_form();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Delete : " + ex.Message);
            }*/
            try
            {
                OracleCommand oupd = new OracleCommand("delete jurusan where " +
                    "kode_jurusan='" + txtKodeJur.Text + "'"
                    , conn);
                if (conn.State == ConnectionState.Closed) conn.Open();
                oupd.ExecuteNonQuery();
                MessageBox.Show("Data jurusan terhapus");
                siapkan_form_mode(true);
                buka_grid(); bersihkan_form();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Delete : " + ex.Message);
            }
            
        }

        private void DG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKodeJur.Text = DT.Rows[e.RowIndex].ItemArray[0].ToString();
            txtNamaJur.Text = DT.Rows[e.RowIndex].ItemArray[1].ToString();
            cmbKepalaJur.SelectedIndex = cari_idx_kajur(DT.Rows[e.RowIndex].ItemArray[2].ToString());
            siapkan_form_mode(false);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (rbTidakAktif.Checked) status = "N";
            else status = "Y";
            if (txtKodeJur.Text == "") MessageBox.Show("Isi Kode Jurusan");
            else if (txtNamaJur.Text == "") MessageBox.Show("Isi Nama Jurusan");
            else if (cmbKepalaJur.SelectedIndex == -1) MessageBox.Show("Pilih Kepala Jurusan");
            else
            {
                try
                {
                    OracleCommand oins = new OracleCommand("insert into jurusan values(" +
                  "'" + txtKodeJur.Text + "'," +
                  "'" + DTKAJUR.Rows[cmbKepalaJur.SelectedIndex].ItemArray[0].ToString() + "'," +
                  "'" + txtNamaJur.Text + "'" +
                  ")"
                  , conn);
                    oins.ExecuteNonQuery();
                    MessageBox.Show("1 data jurusan terbuat");
                    siapkan_form_mode(true);
                    buka_grid(); bersihkan_form();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Insert : " + ex.Message);
                }
                
            }
        }

    }
}
